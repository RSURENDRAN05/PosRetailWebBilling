# ============================================================
# Employee Registration Screen
# - Select employee from list
# - Capture face samples via webcam
# - Save to cloud via API
# ============================================================

import tkinter as tk
from tkinter import ttk
import threading
import cv2
from PIL import Image, ImageTk

from config import *
from api_client import APIClient
from face_utils import capture_face_encodings, CameraStream, encode_frame, annotate_frame
from ui_dialogs import dialogs as messagebox


class RegisterScreen(tk.Frame):
    def __init__(self, parent, on_back=None):
        super().__init__(parent, bg=BG_COLOR)
        self.on_back  = on_back
        self.stream   = CameraStream()
        self._running  = False
        self._after_id = None
        self._selected_emp = None        # dict with emp details
        self.encodings_captured = []
        self._build_ui()

    # ---- Layout --------------------------------------------

    @staticmethod
    def _darken_color(hex_color, factor=0.85):
        h = hex_color.lstrip("#")
        r, g, b = int(h[0:2], 16), int(h[2:4], 16), int(h[4:6], 16)
        return f"#{int(r*factor):02x}{int(g*factor):02x}{int(b*factor):02x}"

    def _build_ui(self):
        # ---- Header ------------------------------------------
        hdr = tk.Frame(self, bg=THEME_COLOR)
        hdr.pack(fill="x")

        left_hdr = tk.Frame(hdr, bg=THEME_COLOR)
        left_hdr.pack(side="left", padx=20, pady=14)
        tk.Label(left_hdr, text="👤", font=("Segoe UI", 18),
                 bg=THEME_COLOR, fg="white").pack(side="left", padx=(0, 10))
        col = tk.Frame(left_hdr, bg=THEME_COLOR)
        col.pack(side="left")
        tk.Label(col, text="Register Employee Face",
                 font=("Segoe UI", 15, "bold"),
                 bg=THEME_COLOR, fg="white").pack(anchor="w")
        tk.Label(col, text="Capture face samples for recognition",
                 font=("Segoe UI", 9),
                 bg=THEME_COLOR, fg="#A8BDD0").pack(anchor="w")

        back_btn = tk.Button(hdr, text="← Back", font=FONT_NORMAL,
                             bg=ACCENT_COLOR, fg="white",
                             relief="flat", padx=14, pady=6,
                             cursor="hand2", command=self._go_back)
        back_btn.pack(side="right", padx=20, pady=14)
        back_btn.bind("<Enter>",
            lambda _e: back_btn.configure(bg=self._darken_color(ACCENT_COLOR)))
        back_btn.bind("<Leave>",
            lambda _e: back_btn.configure(bg=ACCENT_COLOR))

        # Body: left = employee list, right = camera + controls
        body = tk.Frame(self, bg=BG_COLOR)
        body.pack(fill="both", expand=True, padx=20, pady=16)

        self._build_emp_list(body)
        self._build_right_panel(body)

    # ---- Left: Employee List -------------------------------

    def _build_emp_list(self, parent):
        card = tk.Frame(parent, bg=CARD_COLOR, bd=0, relief="flat",
                        highlightbackground=BORDER_COLOR, highlightthickness=1)
        card.pack(side="left", fill="y", padx=(0, 12), ipadx=10, ipady=10)

        tk.Label(card, text="Select Employee", font=FONT_MEDIUM,
                 bg=CARD_COLOR, fg=THEME_COLOR).pack(anchor="w", padx=10, pady=(8, 6))

        # Search row
        search_row = tk.Frame(card, bg=CARD_COLOR)
        search_row.pack(fill="x", padx=10, pady=(0, 6))
        self.search_var = tk.StringVar()
        self.search_var.trace_add("write", lambda *_: self._filter_list())
        ttk.Entry(search_row, textvariable=self.search_var,
                  font=FONT_NORMAL, width=20).pack(side="left")
        tk.Button(search_row, text="🔄", font=FONT_SMALL, bg=BG_COLOR, fg=TEXT_COLOR,
                  relief="flat", padx=6, cursor="hand2",
                  command=self._load_employees).pack(side="left", padx=(4, 0))

        # Treeview
        cols = ("ID", "Name", "Department")
        widths = (70, 160, 110)
        frame = tk.Frame(card, bg=CARD_COLOR)
        frame.pack(fill="both", expand=True, padx=10)
        vsb = ttk.Scrollbar(frame, orient="vertical")
        self.emp_tree = ttk.Treeview(frame, columns=cols, show="headings",
                                     height=18, yscrollcommand=vsb.set)
        for col, w in zip(cols, widths):
            self.emp_tree.heading(col, text=col)
            self.emp_tree.column(col, width=w, anchor="w")
        vsb.config(command=self.emp_tree.yview)
        vsb.pack(side="right", fill="y")
        self.emp_tree.pack(fill="both", expand=True)
        self.emp_tree.bind("<<TreeviewSelect>>", self._on_emp_select)

        self._all_employees = []   # full list for filtering
        self.list_status = tk.StringVar(value="Loading…")
        tk.Label(card, textvariable=self.list_status, font=FONT_SMALL,
                 bg=CARD_COLOR, fg=MUTED_COLOR).pack(anchor="w", padx=10, pady=(4, 0))

    # ---- Right: Camera + controls --------------------------

    def _build_right_panel(self, parent):
        right = tk.Frame(parent, bg=BG_COLOR)
        right.pack(side="left", fill="both", expand=True)

        # Selected employee banner
        sel_card = tk.Frame(right, bg=CARD_COLOR,
                            highlightbackground=BORDER_COLOR, highlightthickness=1)
        sel_card.pack(fill="x", pady=(0, 10), ipadx=10, ipady=8)
        tk.Label(sel_card, text="Selected:", font=FONT_SMALL,
                 bg=CARD_COLOR, fg=MUTED_COLOR).pack(side="left", padx=(10, 4))
        self.sel_name_var = tk.StringVar(value="— none —")
        tk.Label(sel_card, textvariable=self.sel_name_var, font=FONT_MEDIUM,
                 bg=CARD_COLOR, fg=THEME_COLOR).pack(side="left")
        self.sel_id_var = tk.StringVar(value="")
        tk.Label(sel_card, textvariable=self.sel_id_var, font=FONT_SMALL,
                 bg=CARD_COLOR, fg=MUTED_COLOR).pack(side="left", padx=(8, 0))

        # Camera + controls row
        cam_row = tk.Frame(right, bg=BG_COLOR)
        cam_row.pack(fill="both", expand=True)

        # Camera card
        cam_card = tk.Frame(cam_row, bg=CARD_COLOR,
                            highlightbackground=BORDER_COLOR, highlightthickness=1)
        cam_card.pack(side="left", fill="both", expand=True, ipadx=10, ipady=10)
        tk.Label(cam_card, text="Live Camera Preview", font=FONT_MEDIUM,
                 bg=CARD_COLOR, fg=THEME_COLOR).pack(pady=(8, 6))
        self.cam_label = tk.Label(cam_card, bg="#1A1A2E",
                                  width=PREVIEW_WIDTH, height=PREVIEW_HEIGHT)
        self.cam_label.pack(padx=10, pady=(0, 6))
        self.status_label = tk.Label(cam_card, text="Camera: Off",
                                     font=FONT_SMALL, bg=CARD_COLOR, fg=MUTED_COLOR)
        self.status_label.pack()

        # Controls card
        ctrl_card = tk.Frame(cam_row, bg=CARD_COLOR,
                             highlightbackground=BORDER_COLOR, highlightthickness=1)
        ctrl_card.pack(side="left", fill="y", padx=(10, 0), ipadx=14, ipady=10)

        tk.Label(ctrl_card, text="Face Capture", font=FONT_MEDIUM,
                 bg=CARD_COLOR, fg=THEME_COLOR).pack(anchor="w", padx=10, pady=(8, 12))

        self.progress_var = tk.StringVar(value="Select an employee, then capture")
        tk.Label(ctrl_card, textvariable=self.progress_var, font=FONT_SMALL,
                 bg=CARD_COLOR, fg=MUTED_COLOR, wraplength=200,
                 justify="left").pack(anchor="w", padx=10, pady=(0, 8))

        self.progress_bar = ttk.Progressbar(ctrl_card, length=200,
                                            mode="determinate", maximum=FACE_SAMPLES)
        self.progress_bar.pack(padx=10, pady=(0, 12))

        self.capture_btn = tk.Button(
            ctrl_card, text="📷  Capture Face", font=FONT_NORMAL,
            bg=ACCENT_COLOR, fg="white", relief="flat", padx=10, pady=8,
            cursor="hand2", state="disabled", command=self._start_capture)
        self.capture_btn.pack(fill="x", padx=10, pady=(0, 6))

        self.save_btn = tk.Button(
            ctrl_card, text="💾  Save Face Data", font=FONT_NORMAL,
            bg=THEME_COLOR, fg="white", relief="flat", padx=10, pady=8,
            cursor="hand2", state="disabled", command=self._save_employee)
        self.save_btn.pack(fill="x", padx=10)

    # ---- Employee list loading / filtering -----------------

    def _load_employees(self):
        self.list_status.set("Loading…")
        self.emp_tree.delete(*self.emp_tree.get_children())
        threading.Thread(target=self._fetch_employees, daemon=True).start()

    def _fetch_employees(self):
        result = APIClient.get_employees(status="active")
        if result.get("success"):
            emps = result.get("data", [])
            self._all_employees = emps
            self.after(0, lambda: self._populate_tree(emps))
        else:
            msg = result.get("message", "Failed to load")
            self.after(0, lambda: self.list_status.set(f"⚠ {msg}"))

    def _populate_tree(self, emps):
        self.emp_tree.delete(*self.emp_tree.get_children())
        for e in emps:
            self.emp_tree.insert("", "end", iid=str(e["emp_id"]), values=(
                e.get("emp_id", ""),
                e.get("emp_printname", e.get("emp_name", "")),
                e.get("emp_designation", e.get("department", "")),
            ))
        self.list_status.set(f"{len(emps)} employees")

    def _filter_list(self):
        q = self.search_var.get().strip().lower()
        filtered = [e for e in self._all_employees
                    if q in str(e.get("emp_id", "")).lower()
                    or q in (e.get("emp_printname") or "").lower()
                    or q in (e.get("emp_designation") or "").lower()] if q else self._all_employees
        self._populate_tree(filtered)

    def _on_emp_select(self, _event=None):
        sel = self.emp_tree.selection()
        if not sel:
            return
        emp_id = sel[0]
        emp = next((e for e in self._all_employees if str(e["emp_id"]) == emp_id), None)
        if not emp:
            return
        self._selected_emp = emp
        name = emp.get("emp_printname", emp.get("emp_name", emp_id))
        self.sel_name_var.set(name)
        self.sel_id_var.set(f"  (ID: {emp_id})")
        self.capture_btn.config(state="normal")
        self.save_btn.config(state="disabled")
        self.progress_bar["value"] = 0
        self.progress_var.set(f"Ready to capture face for {name}")
        self.encodings_captured = []

    # ---- Camera Preview ------------------------------------

    def start_preview(self):
        ok, msg = self.stream.open()
        if not ok:
            self.status_label.config(text=f"⚠ {msg}", fg=DANGER_COLOR)
            return
        self._running = True
        self.status_label.config(text="● Camera on", fg=ACCENT_COLOR)
        self._update_preview()

    def _update_preview(self):
        if not self._running:
            return
        ret, frame = self.stream.read()
        if ret:
            frame = cv2.flip(frame, 1)
            locations, encs = encode_frame(frame)
            if locations:
                identities = [("?", "Position Face", 0.0)] * len(locations)
                frame = annotate_frame(frame, locations, identities)
            self._show_frame(frame)
        self._after_id = self.after(30, self._update_preview)

    def _show_frame(self, frame_bgr):
        rgb  = cv2.cvtColor(frame_bgr, cv2.COLOR_BGR2RGB)
        img  = Image.fromarray(rgb).resize((PREVIEW_WIDTH, PREVIEW_HEIGHT))
        imtk = ImageTk.PhotoImage(image=img)
        self.cam_label.imgtk = imtk
        self.cam_label.config(image=imtk)

    def stop_preview(self):
        self._running = False
        if self._after_id:
            self.after_cancel(self._after_id)
        self.stream.release()

    # ---- Capture -------------------------------------------

    def _start_capture(self):
        if not self._selected_emp:
            messagebox.showwarning("No Employee", "Please select an employee from the list.")
            return

        self.capture_btn.config(state="disabled")
        self.save_btn.config(state="disabled")
        self.progress_bar["value"] = 0
        self.encodings_captured = []
        name = self._selected_emp.get("emp_printname", self._selected_emp.get("emp_id"))
        self.progress_var.set(f"Capturing {name}… look at the camera")

        threading.Thread(target=self._capture_thread, daemon=True).start()

    def _capture_thread(self):
        self.stop_preview()

        def progress(current, total):
            self.after(0, lambda: self._on_progress(current, total))

        encodings, msg = capture_face_encodings(
            num_samples=FACE_SAMPLES,
            progress_callback=progress
        )
        self.after(0, lambda: self._on_capture_done(encodings, msg))

    def _on_progress(self, current, total):
        self.progress_bar["value"] = current
        self.progress_var.set(f"Captured {current}/{total} samples…")

    def _on_capture_done(self, encodings, msg):
        self.encodings_captured = encodings
        self.start_preview()
        self.capture_btn.config(state="normal")
        if len(encodings) >= FACE_SAMPLES:
            self.progress_var.set(f"✅ {len(encodings)} samples ready. Click Save.")
            self.save_btn.config(state="normal")
        else:
            self.progress_var.set(f"⚠ Only {len(encodings)} samples. Try again.")

    # ---- Save ----------------------------------------------

    def _save_employee(self):
        if not self._selected_emp:
            messagebox.showwarning("No Employee", "Please select an employee first.")
            return
        if not self.encodings_captured:
            messagebox.showwarning("No Face", "Please capture face samples first.")
            return

        self.save_btn.config(state="disabled")
        self.progress_var.set("Saving to cloud…")
        threading.Thread(target=self._save_thread, daemon=True).start()

    def _save_thread(self):
        emp_id = self._selected_emp["emp_id"]
        name   = self._selected_emp.get("emp_printname", str(emp_id))
        com_id = self._selected_emp.get("emp_compid")
        loc_id = self._selected_emp.get("emp_locid")

        result = APIClient.save_face_encodings(
            emp_id    = emp_id,
            com_id    = com_id,
            loc_id    = loc_id,
            encodings = self.encodings_captured,
            replace   = True,
        )

        if result.get("success"):
            saved_count = (result.get("data") or {}).get("saved_count", len(self.encodings_captured))
            self.after(0, lambda: self._save_success(name, saved_count))
        else:
            self.after(0, lambda: self._save_error(result.get("message")))

    def _save_success(self, name, saved_count):
        messagebox.showinfo("Success", f"✅ {name} registered successfully!\nSaved samples: {saved_count}")
        self._selected_emp = None
        self.sel_name_var.set("— none —")
        self.sel_id_var.set("")
        self.emp_tree.selection_remove(*self.emp_tree.selection())
        self.progress_bar["value"] = 0
        self.progress_var.set("Saved! Select next employee to register.")
        self.save_btn.config(state="disabled")
        self.capture_btn.config(state="disabled")
        self.encodings_captured = []

    def _save_error(self, msg):
        messagebox.showerror("Error", f"Failed to save: {msg}")
        self.save_btn.config(state="normal")
        self.progress_var.set("Error saving. Please try again.")

    # ---- Navigation ----------------------------------------

    def _go_back(self):
        self.stop_preview()
        if self.on_back:
            self.on_back()

    def on_show(self):
        self._load_employees()
        self.start_preview()

    def on_hide(self):
        self.stop_preview()

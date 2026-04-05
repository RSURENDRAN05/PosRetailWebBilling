# ============================================================
# Employee Registration Screen
# - Fill employee details
# - Capture face samples via webcam
# - Save to cloud via API
# ============================================================

import tkinter as tk
from tkinter import ttk, messagebox
import threading
import cv2
from PIL import Image, ImageTk
import numpy as np

from config import *
from api_client import APIClient
from face_utils import capture_face_encodings, CameraStream, encode_frame, annotate_frame


class RegisterScreen(tk.Frame):
    def __init__(self, parent, on_back=None):
        super().__init__(parent, bg=BG_COLOR)
        self.on_back  = on_back
        self.stream   = CameraStream()
        self._running = False
        self._after_id = None
        self._build_ui()

    # ---- Layout --------------------------------------------

    def _build_ui(self):
        # Header
        hdr = tk.Frame(self, bg=THEME_COLOR, pady=12)
        hdr.pack(fill="x")
        tk.Label(hdr, text="👤  Register New Employee",
                 font=FONT_LARGE, bg=THEME_COLOR, fg="white").pack(side="left", padx=20)
        tk.Button(hdr, text="← Back", font=FONT_NORMAL, bg=ACCENT_COLOR, fg="white",
                  relief="flat", padx=12, pady=4, cursor="hand2",
                  command=self._go_back).pack(side="right", padx=20)

        # Body: left = form, right = camera
        body = tk.Frame(self, bg=BG_COLOR)
        body.pack(fill="both", expand=True, padx=20, pady=16)

        self._build_form(body)
        self._build_camera_panel(body)

    def _build_form(self, parent):
        card = tk.Frame(parent, bg=CARD_COLOR, bd=0, relief="flat",
                        highlightbackground="#DDE1E7", highlightthickness=1)
        card.pack(side="left", fill="y", padx=(0, 12), ipadx=16, ipady=12)

        tk.Label(card, text="Employee Details", font=FONT_MEDIUM,
                 bg=CARD_COLOR, fg=THEME_COLOR).pack(anchor="w", pady=(8, 12), padx=10)

        fields = [
            ("Employee ID *",  "emp_id",     "e.g. EMP001"),
            ("Full Name *",     "name",       "e.g. John Doe"),
            ("Department",      "dept",       "e.g. IT / HR / Sales"),
            ("Position",        "position",   "e.g. Software Engineer"),
            ("Email",           "email",      "e.g. john@company.com"),
            ("Phone",           "phone",      "e.g. +60123456789"),
        ]

        self.vars = {}
        for label, key, hint in fields:
            tk.Label(card, text=label, font=FONT_SMALL, bg=CARD_COLOR,
                     fg=MUTED_COLOR).pack(anchor="w", padx=10)
            var = tk.StringVar()
            entry = ttk.Entry(card, textvariable=var, font=FONT_NORMAL, width=26)
            entry.pack(anchor="w", padx=10, pady=(0, 8), ipady=4)
            self.vars[key] = var

        # Progress label
        self.progress_var = tk.StringVar(value="Fill in details, then click Capture Face")
        tk.Label(card, textvariable=self.progress_var, font=FONT_SMALL,
                 bg=CARD_COLOR, fg=MUTED_COLOR, wraplength=240, justify="left"
                 ).pack(anchor="w", padx=10, pady=(4, 6))

        # Progress bar
        self.progress_bar = ttk.Progressbar(card, length=240, mode="determinate", maximum=FACE_SAMPLES)
        self.progress_bar.pack(padx=10, pady=(0, 8))

        # Buttons
        btn_frame = tk.Frame(card, bg=CARD_COLOR)
        btn_frame.pack(padx=10, fill="x")

        self.capture_btn = tk.Button(
            btn_frame, text="📷  Capture Face", font=FONT_NORMAL,
            bg=ACCENT_COLOR, fg="white", relief="flat", padx=10, pady=6, cursor="hand2",
            command=self._start_capture
        )
        self.capture_btn.pack(fill="x", pady=(0, 6))

        self.save_btn = tk.Button(
            btn_frame, text="💾  Save Employee", font=FONT_NORMAL,
            bg=THEME_COLOR, fg="white", relief="flat", padx=10, pady=6, cursor="hand2",
            state="disabled", command=self._save_employee
        )
        self.save_btn.pack(fill="x")

        self.encodings_captured = []

    def _build_camera_panel(self, parent):
        cam_card = tk.Frame(parent, bg=CARD_COLOR, bd=0, relief="flat",
                            highlightbackground="#DDE1E7", highlightthickness=1)
        cam_card.pack(side="left", fill="both", expand=True, ipadx=10, ipady=10)

        tk.Label(cam_card, text="Live Camera Preview", font=FONT_MEDIUM,
                 bg=CARD_COLOR, fg=THEME_COLOR).pack(pady=(8, 6))

        self.cam_label = tk.Label(cam_card, bg="#1A1A2E",
                                  width=PREVIEW_WIDTH, height=PREVIEW_HEIGHT)
        self.cam_label.pack(padx=10, pady=(0, 10))

        self.status_label = tk.Label(cam_card, text="Camera: Off",
                                     font=FONT_SMALL, bg=CARD_COLOR, fg=MUTED_COLOR)
        self.status_label.pack()

    # ---- Camera Preview (idle) -----------------------------

    def start_preview(self):
        ok, msg = self.stream.open()
        if not ok:
            self.status_label.config(text=f"⚠ {msg}", fg=DANGER_COLOR)
            return
        self._running  = True
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
        emp_id = self.vars["emp_id"].get().strip()
        name   = self.vars["name"].get().strip()
        if not emp_id or not name:
            messagebox.showwarning("Missing Info", "Employee ID and Full Name are required.")
            return

        self.capture_btn.config(state="disabled")
        self.save_btn.config(state="disabled")
        self.progress_bar["value"] = 0
        self.encodings_captured    = []
        self.progress_var.set("Starting camera… look at the camera")

        # Run capture in background thread
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
        self.progress_var.set(f"Captured {current}/{total} samples… keep looking at the camera")

    def _on_capture_done(self, encodings, msg):
        self.encodings_captured = encodings
        self.start_preview()

        if len(encodings) >= FACE_SAMPLES:
            self.progress_var.set(f"✅ {len(encodings)} samples captured! Click Save Employee.")
            self.save_btn.config(state="normal")
        else:
            self.progress_var.set(f"⚠ Only {len(encodings)} samples captured. Try again.")

        self.capture_btn.config(state="normal")

    # ---- Save ----------------------------------------------

    def _save_employee(self):
        data = {k: v.get().strip() for k, v in self.vars.items()}
        if not data["emp_id"] or not data["name"]:
            messagebox.showwarning("Missing Info", "Employee ID and Full Name are required.")
            return
        if not self.encodings_captured:
            messagebox.showwarning("No Face", "Please capture face samples first.")
            return

        self.save_btn.config(state="disabled")
        self.progress_var.set("Saving to cloud…")

        threading.Thread(target=self._save_thread, args=(data,), daemon=True).start()

    def _save_thread(self, data):
        # NOTE: The face system is READ-ONLY for pos_employeeinfo.
        # Step 1: Verify employee exists in pos_employeeinfo
        check = APIClient.get_employee(data["emp_id"])
        if not check.get("success"):
            self.after(0, lambda: self._save_error(
                f"Employee '{data['emp_id']}' not found in employee database.\n"
                "Please register the employee in your POS system first."
            ))
            return

        # Step 2: Save face encodings only
        result = APIClient.save_face_encodings(
            emp_id    = data["emp_id"],
            encodings = self.encodings_captured,
            replace   = True,
        )

        if result.get("success"):
            self.after(0, lambda: self._save_success(data["name"]))
        else:
            self.after(0, lambda: self._save_error(result.get("message")))

    def _save_success(self, name):
        messagebox.showinfo("Success", f"✅ {name} registered successfully!")
        for v in self.vars.values():
            v.set("")
        self.progress_bar["value"] = 0
        self.progress_var.set("Employee saved! Ready for next registration.")
        self.save_btn.config(state="disabled")
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
        self.start_preview()

    def on_hide(self):
        self.stop_preview()

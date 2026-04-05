# ============================================================
# Live Attendance Screen
# - Real-time face detection via webcam
# - Match against cloud-stored encodings
# - Auto check-in / check-out with cooldown
# ============================================================

import tkinter as tk
from tkinter import ttk
import threading
import time
import cv2
from PIL import Image, ImageTk
from collections import defaultdict
from datetime import datetime

from config import *
from api_client import APIClient
from face_utils import face_cache, encode_frame, identify_face, annotate_frame, CameraStream


class AttendanceScreen(tk.Frame):
    def __init__(self, parent, on_back=None):
        super().__init__(parent, bg=BG_COLOR)
        self.on_back      = on_back
        self.stream       = CameraStream()
        self._running     = False
        self._after_id    = None
        self._cooldown    = {}   # employee_id → last_marked timestamp
        self._today_log   = []  # recent records list
        self._build_ui()

    # ---- Layout --------------------------------------------

    def _build_ui(self):
        # Header
        hdr = tk.Frame(self, bg=THEME_COLOR, pady=12)
        hdr.pack(fill="x")
        tk.Label(hdr, text="🎯  Live Face Attendance",
                 font=FONT_LARGE, bg=THEME_COLOR, fg="white").pack(side="left", padx=20)

        self.reload_btn = tk.Button(
            hdr, text="🔄 Reload Faces", font=FONT_SMALL,
            bg="#3D5A70", fg="white", relief="flat", padx=8, pady=4, cursor="hand2",
            command=self._reload_encodings
        )
        self.reload_btn.pack(side="right", padx=8)

        tk.Button(hdr, text="← Back", font=FONT_NORMAL, bg=ACCENT_COLOR, fg="white",
                  relief="flat", padx=12, pady=4, cursor="hand2",
                  command=self._go_back).pack(side="right", padx=(20, 0))

        # Body: camera left, log right
        body = tk.Frame(self, bg=BG_COLOR)
        body.pack(fill="both", expand=True, padx=20, pady=16)

        self._build_camera_panel(body)
        self._build_log_panel(body)

    def _build_camera_panel(self, parent):
        cam_card = tk.Frame(parent, bg=CARD_COLOR, bd=0,
                            highlightbackground="#DDE1E7", highlightthickness=1)
        cam_card.pack(side="left", fill="both", expand=True, padx=(0, 12), ipadx=10, ipady=10)

        # Status bar
        status_bar = tk.Frame(cam_card, bg=CARD_COLOR)
        status_bar.pack(fill="x", padx=10, pady=(8, 4))

        self.cam_status = tk.Label(status_bar, text="● Starting camera…",
                                   font=FONT_SMALL, bg=CARD_COLOR, fg=WARNING_COLOR)
        self.cam_status.pack(side="left")

        self.face_count_lbl = tk.Label(status_bar, text="Faces loaded: 0",
                                       font=FONT_SMALL, bg=CARD_COLOR, fg=MUTED_COLOR)
        self.face_count_lbl.pack(side="right")

        # Live feed
        self.cam_label = tk.Label(cam_card, bg="#1A1A2E",
                                  width=PREVIEW_WIDTH, height=PREVIEW_HEIGHT)
        self.cam_label.pack(padx=10, pady=4)

        # Action indicator
        self.action_var = tk.StringVar(value="Scanning for faces…")
        self.action_lbl = tk.Label(cam_card, textvariable=self.action_var,
                                   font=FONT_MEDIUM, bg=CARD_COLOR, fg=MUTED_COLOR)
        self.action_lbl.pack(pady=(4, 8))

    def _build_log_panel(self, parent):
        log_card = tk.Frame(parent, bg=CARD_COLOR, bd=0,
                            highlightbackground="#DDE1E7", highlightthickness=1)
        log_card.pack(side="left", fill="y", ipadx=10, ipady=10)
        log_card.config(width=300)
        log_card.pack_propagate(False)

        tk.Label(log_card, text="Today's Log", font=FONT_MEDIUM,
                 bg=CARD_COLOR, fg=THEME_COLOR).pack(pady=(8, 6), padx=10, anchor="w")

        # Stats row
        stats = tk.Frame(log_card, bg=CARD_COLOR)
        stats.pack(fill="x", padx=10, pady=(0, 8))

        self.present_var = tk.StringVar(value="0")
        self.late_var    = tk.StringVar(value="0")

        for lbl, var, color in [("Present", self.present_var, ACCENT_COLOR),
                                 ("Late",    self.late_var,    WARNING_COLOR)]:
            box = tk.Frame(stats, bg=color, padx=8, pady=4)
            box.pack(side="left", padx=(0, 6))
            tk.Label(box, textvariable=var, font=FONT_MEDIUM, bg=color, fg="white").pack()
            tk.Label(box, text=lbl, font=FONT_SMALL, bg=color, fg="white").pack()

        # Listbox
        list_frame = tk.Frame(log_card, bg=CARD_COLOR)
        list_frame.pack(fill="both", expand=True, padx=10)

        scrollbar = ttk.Scrollbar(list_frame)
        scrollbar.pack(side="right", fill="y")

        self.log_list = tk.Listbox(
            list_frame, font=FONT_SMALL, bg=CARD_COLOR, fg=TEXT_COLOR,
            selectbackground=ACCENT_COLOR, relief="flat", bd=0,
            yscrollcommand=scrollbar.set
        )
        self.log_list.pack(fill="both", expand=True)
        scrollbar.config(command=self.log_list.yview)

    # ---- Start / Stop -------------------------------------

    def on_show(self):
        self._load_data_and_start()

    def on_hide(self):
        self._stop_camera()

    def _load_data_and_start(self):
        self.cam_status.config(text="⏳ Loading face data…", fg=WARNING_COLOR)
        threading.Thread(target=self._init_thread, daemon=True).start()

    def _init_thread(self):
        # Load encodings
        if face_cache.is_stale():
            ok, msg = face_cache.load_from_api()
        else:
            ok, msg = True, "Cache OK"

        data = face_cache.get_data()
        self.after(0, lambda: self.face_count_lbl.config(
            text=f"Faces loaded: {len(data)}", fg=ACCENT_COLOR if data else DANGER_COLOR
        ))

        # Load today's log
        result = APIClient.get_attendance()
        if result.get("success"):
            records = result["data"]["records"]
            summary = result["data"]["summary"]
            self.after(0, lambda: self._refresh_log(records, summary))

        self.after(0, self._start_camera)

    def _start_camera(self):
        ok, msg = self.stream.open()
        if not ok:
            self.cam_status.config(text=f"⚠ {msg}", fg=DANGER_COLOR)
            return
        self._running = True
        self.cam_status.config(text="● Camera active — scanning", fg=ACCENT_COLOR)
        self._update_frame()

    def _stop_camera(self):
        self._running = False
        if self._after_id:
            self.after_cancel(self._after_id)
        self.stream.release()

    def _update_frame(self):
        if not self._running:
            return

        ret, frame = self.stream.read()
        if ret:
            frame = cv2.flip(frame, 1)
            locations, encs = encode_frame(frame)
            known = face_cache.get_data()
            identities = []

            for enc in encs:
                emp_id, emp_name, conf = identify_face(enc, known)
                identities.append((emp_id, emp_name, conf))

                if emp_id != "Unknown":
                    self._try_mark_attendance(emp_id, emp_name)

            frame = annotate_frame(frame, locations, identities)
            self._update_action_label(identities)
            self._show_frame(frame)

        self._after_id = self.after(33, self._update_frame)  # ~30 FPS

    def _show_frame(self, frame_bgr):
        rgb  = cv2.cvtColor(frame_bgr, cv2.COLOR_BGR2RGB)
        img  = Image.fromarray(rgb).resize((PREVIEW_WIDTH, PREVIEW_HEIGHT))
        imtk = ImageTk.PhotoImage(image=img)
        self.cam_label.imgtk = imtk
        self.cam_label.config(image=imtk)

    def _update_action_label(self, identities):
        if not identities:
            self.action_lbl.config(text="Scanning for faces…", fg=MUTED_COLOR)
        else:
            names = [n for (_, n, _) in identities if n != "Unknown"]
            unk   = sum(1 for (i, _, _) in identities if i == "Unknown")
            parts = []
            if names:  parts.append(f"✅ {', '.join(names)}")
            if unk:    parts.append(f"❓ {unk} unknown face(s)")
            self.action_lbl.config(text="  |  ".join(parts), fg=ACCENT_COLOR)

    # ---- Attendance logic ----------------------------------

    def _try_mark_attendance(self, emp_id: str, emp_name: str):
        now = time.time()
        last = self._cooldown.get(emp_id, 0)
        if now - last < COOLDOWN_SECONDS:
            return  # Still in cooldown

        self._cooldown[emp_id] = now

        # Determine action
        action = "checkout" if CHECK_OUT_MODE else "checkin"
        threading.Thread(target=self._mark_thread,
                         args=(emp_id, emp_name, action), daemon=True).start()

    def _mark_thread(self, emp_id, emp_name, action):
        if action == "checkin":
            result = APIClient.check_in(emp_id)
        else:
            result = APIClient.check_out(emp_id)

        if result.get("success"):
            rec  = result.get("data", {})
            time_str = datetime.now().strftime("%H:%M:%S")
            status   = rec.get("status", "present")
            entry    = f"{time_str}  {emp_name}  ← {action.upper()}"
            if status == "late":
                entry += "  ⚠ LATE"
            self.after(0, lambda: self._add_log_entry(entry, status))
            self.after(0, self._refresh_today_stats)

    def _add_log_entry(self, text: str, status: str):
        color = WARNING_COLOR if status == "late" else ACCENT_COLOR
        self.log_list.insert(0, text)
        self.log_list.itemconfig(0, fg=color)
        if self.log_list.size() > 100:
            self.log_list.delete(100, tk.END)

    def _refresh_today_stats(self):
        result = APIClient.get_attendance()
        if result.get("success"):
            s = result["data"]["summary"]
            self.present_var.set(str(s.get("present", 0)))
            self.late_var.set(str(s.get("late", 0)))

    def _refresh_log(self, records, summary):
        self.log_list.delete(0, tk.END)
        for r in records:
            ci   = r.get("check_in",  "")[-8:] if r.get("check_in")  else "--:--"
            co   = r.get("check_out", "")[-8:] if r.get("check_out") else "  --  "
            name = r.get("employee_name", r.get("emp_id", ""))
            st   = r.get("status", "present")
            text = f"{ci} → {co}  {name}"
            if st == "late": text += " ⚠"
            self.log_list.insert(tk.END, text)
            self.log_list.itemconfig(tk.END, fg=WARNING_COLOR if st == "late" else TEXT_COLOR)

        self.present_var.set(str(summary.get("present", 0)))
        self.late_var.set(str(summary.get("late", 0)))

    # ---- Reload encodings ----------------------------------

    def _reload_encodings(self):
        face_cache.clear()
        self.face_count_lbl.config(text="Reloading…", fg=WARNING_COLOR)
        threading.Thread(target=self._reload_thread, daemon=True).start()

    def _reload_thread(self):
        ok, msg = face_cache.load_from_api()
        data = face_cache.get_data()
        self.after(0, lambda: self.face_count_lbl.config(
            text=f"Faces loaded: {len(data)}",
            fg=ACCENT_COLOR if data else DANGER_COLOR
        ))

    # ---- Navigation ----------------------------------------

    def _go_back(self):
        self._stop_camera()
        if self.on_back:
            self.on_back()

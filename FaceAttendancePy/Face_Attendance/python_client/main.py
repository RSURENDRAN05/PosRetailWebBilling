# ============================================================
# Face Attendance System - Main Application
# Run:  python main.py
# ============================================================

import tkinter as tk
from tkinter import ttk, messagebox
import threading
import sys
import os

# Ensure script directory is on path
sys.path.insert(0, os.path.dirname(os.path.abspath(__file__)))

from config import *
from api_client import APIClient
from face_utils import face_cache
from screens.register_screen   import RegisterScreen
from screens.attendance_screen import AttendanceScreen
from screens.records_screen    import RecordsScreen
from screens.employees_screen  import EmployeesScreen


# ============================================================
# Dashboard (Home) Screen
# ============================================================

class DashboardScreen(tk.Frame):
    def __init__(self, parent, nav_callback):
        super().__init__(parent, bg=BG_COLOR)
        self.nav = nav_callback
        self._build_ui()

    def _build_ui(self):
        # Hero banner
        hero = tk.Frame(self, bg=THEME_COLOR)
        hero.pack(fill="x")

        tk.Label(hero, text="🏢  Face Attendance System",
                 font=("Segoe UI", 24, "bold"),
                 bg=THEME_COLOR, fg="white", pady=24).pack()

        self.conn_lbl = tk.Label(hero, text="Checking connection…",
                                  font=FONT_SMALL, bg=THEME_COLOR, fg="#BDC3C7")
        self.conn_lbl.pack(pady=(0, 16))

        # Stats row
        self.stats_frame = tk.Frame(self, bg=BG_COLOR)
        self.stats_frame.pack(fill="x", padx=30, pady=20)

        self.stat_labels = {}
        for key, title, icon, color in [
            ("total",   "Total Employees", "👤", "#3498DB"),
            ("present", "Present Today",   "✅", ACCENT_COLOR),
            ("absent",  "Absent Today",    "❌", DANGER_COLOR),
            ("late",    "Late Today",      "⚠", WARNING_COLOR),
        ]:
            card = tk.Frame(self.stats_frame, bg=color, padx=24, pady=16,
                            relief="flat", bd=0)
            card.pack(side="left", padx=10, fill="y")
            tk.Label(card, text=icon, font=("Segoe UI", 22), bg=color, fg="white").pack()
            lbl = tk.Label(card, text="—", font=("Segoe UI", 28, "bold"), bg=color, fg="white")
            lbl.pack()
            tk.Label(card, text=title, font=FONT_SMALL, bg=color, fg="white").pack()
            self.stat_labels[key] = lbl

        # Navigation buttons
        nav_frame = tk.Frame(self, bg=BG_COLOR)
        nav_frame.pack(fill="both", expand=True, padx=30, pady=10)

        tk.Label(nav_frame, text="Quick Actions", font=FONT_MEDIUM,
                 bg=BG_COLOR, fg=TEXT_COLOR).pack(anchor="w", pady=(0, 12))

        buttons_data = [
            ("🎯  Mark Attendance",     "attendance", ACCENT_COLOR,
             "Start live face scanning to mark employee attendance automatically"),
            ("👤  Register Employee",   "register",   THEME_COLOR,
             "Register a new employee and capture their face for recognition"),
            ("👥  Manage Employees",    "employees",  "#8E44AD",
             "View, edit, or deactivate employees"),
            ("📋  Attendance Records",  "records",    "#2980B9",
             "View, filter, and export attendance history"),
        ]

        row1 = tk.Frame(nav_frame, bg=BG_COLOR)
        row1.pack(fill="x")
        row2 = tk.Frame(nav_frame, bg=BG_COLOR)
        row2.pack(fill="x", pady=12)

        for i, (label, page, color, desc) in enumerate(buttons_data):
            container = row1 if i < 2 else row2
            card = tk.Frame(container, bg=CARD_COLOR,
                            highlightbackground="#DDE1E7", highlightthickness=1,
                            cursor="hand2")
            card.pack(side="left", fill="both", expand=True, padx=8, ipadx=10, ipady=10)

            accent = tk.Frame(card, bg=color, height=4)
            accent.pack(fill="x")

            tk.Label(card, text=label, font=FONT_MEDIUM,
                     bg=CARD_COLOR, fg=color).pack(anchor="w", padx=16, pady=(14, 4))
            tk.Label(card, text=desc, font=FONT_SMALL,
                     bg=CARD_COLOR, fg=MUTED_COLOR, wraplength=220, justify="left"
                     ).pack(anchor="w", padx=16, pady=(0, 14))

            p = page  # capture for lambda
            card.bind("<Button-1>", lambda e, pg=p: self.nav(pg))
            for child in card.winfo_children():
                child.bind("<Button-1>", lambda e, pg=p: self.nav(pg))

    def on_show(self):
        threading.Thread(target=self._load_stats, daemon=True).start()

    def on_hide(self):
        pass

    def _load_stats(self):
        ok, msg = APIClient.test_connection()
        if ok:
            self.after(0, lambda: self.conn_lbl.config(
                text=f"✅ Connected to server  |  v{APP_VERSION}", fg="#2ECC71"))
        else:
            self.after(0, lambda: self.conn_lbl.config(
                text=f"⚠ {msg}", fg=WARNING_COLOR))
            return

        result = APIClient.get_stats()
        if result.get("success"):
            d = result["data"]["today"]
            self.after(0, lambda: self._update_stats(d))

    def _update_stats(self, data):
        self.stat_labels["total"].config(text=str(data.get("total", "—")))
        self.stat_labels["present"].config(text=str(data.get("present", "—")))
        self.stat_labels["absent"].config(text=str(data.get("absent", "—")))
        self.stat_labels["late"].config(text=str(data.get("late", "—")))


# ============================================================
# Main Application Window
# ============================================================

class App(tk.Tk):
    def __init__(self):
        super().__init__()
        self.title(APP_TITLE)
        self.configure(bg=BG_COLOR)
        self.geometry("1100x720")
        self.minsize(900, 600)
        self.resizable(True, True)

        # Center on screen
        self.update_idletasks()
        sw = self.winfo_screenwidth()
        sh = self.winfo_screenheight()
        x  = (sw - 1100) // 2
        y  = (sh - 720)  // 2
        self.geometry(f"1100x720+{x}+{y}")

        # Apply ttk theme
        style = ttk.Style()
        style.theme_use("clam")
        style.configure("TEntry",  fieldbackground="white", relief="flat", padding=4)
        style.configure("TButton", relief="flat")

        # Screen registry
        self.screens = {}
        self.current_screen = None

        self._build_screens()
        self._show("dashboard")
        self.protocol("WM_DELETE_WINDOW", self._on_close)

    def _build_screens(self):
        container = tk.Frame(self, bg=BG_COLOR)
        container.pack(fill="both", expand=True)

        def nav(page):
            self._show(page)

        self.screens["dashboard"]  = DashboardScreen(container, nav)
        self.screens["register"]   = RegisterScreen(container, on_back=lambda: self._show("dashboard"))
        self.screens["attendance"] = AttendanceScreen(container, on_back=lambda: self._show("dashboard"))
        self.screens["records"]    = RecordsScreen(container, on_back=lambda: self._show("dashboard"))
        self.screens["employees"]  = EmployeesScreen(container, on_back=lambda: self._show("dashboard"))

        for scr in self.screens.values():
            scr.place(relx=0, rely=0, relwidth=1, relheight=1)

    def _show(self, page: str):
        # Hide current
        if self.current_screen:
            old = self.screens.get(self.current_screen)
            if old and hasattr(old, "on_hide"):
                old.on_hide()

        # Raise new
        new = self.screens.get(page)
        if new:
            new.lift()
            if hasattr(new, "on_show"):
                new.on_show()
            self.current_screen = page
            self.title(f"{APP_TITLE}  —  {page.replace('_', ' ').title()}")

    def _on_close(self):
        # Stop any running camera
        for scr in self.screens.values():
            if hasattr(scr, "on_hide"):
                scr.on_hide()
        self.destroy()


# ============================================================
# Entry Point
# ============================================================

if __name__ == "__main__":
    # Quick dependency check
    try:
        import face_recognition
        import cv2
        from PIL import Image
    except ImportError as e:
        import tkinter as tk
        from tkinter import messagebox
        root = tk.Tk()
        root.withdraw()
        messagebox.showerror(
            "Missing Dependency",
            f"Required package not found:\n{e}\n\nPlease run:\n  install.bat\nor:\n  pip install -r requirements.txt"
        )
        sys.exit(1)

    app = App()
    app.mainloop()

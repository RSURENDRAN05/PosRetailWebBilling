# ============================================================
# Face Attendance System - Main Application
# Run:  python main.py
# ============================================================

import tkinter as tk
from tkinter import ttk, messagebox
import threading
import sys
import os
import site

# Ensure script directory is on path
sys.path.insert(0, os.path.dirname(os.path.abspath(__file__)))


def _configure_windows_dll_dirs():
    """Ensure native dependency DLLs are discoverable before importing dlib/cv2."""
    if os.name != "nt":
        return

    candidates = []

    if getattr(sys, "frozen", False):
        # PyInstaller onefile extracts into _MEIPASS; onedir keeps libs near executable.
        base = getattr(sys, "_MEIPASS", os.path.dirname(sys.executable))
        exe_dir = os.path.dirname(sys.executable)
        candidates.extend([
            base,
            os.path.join(base, "_internal"),
            os.path.join(base, "_internal", "numpy.libs"),
            os.path.join(base, "_internal", "cv2"),
            exe_dir,
            os.path.join(exe_dir, "_internal"),
            os.path.join(exe_dir, "_internal", "numpy.libs"),
            os.path.join(exe_dir, "_internal", "cv2"),
        ])
    else:
        # Source mode: include Python DLL dirs and known package DLL dirs.
        candidates.extend([
            os.path.dirname(sys.executable),
            os.path.join(os.path.dirname(sys.executable), "DLLs"),
        ])
        for sp in site.getsitepackages():
            candidates.extend([
                sp,
                os.path.join(sp, "dlib"),
                os.path.join(sp, "numpy.libs"),
                os.path.join(sp, "cv2"),
            ])

    seen = set()
    for path in candidates:
        if not path or path in seen or not os.path.isdir(path):
            continue
        seen.add(path)
        try:
            os.add_dll_directory(path)
        except Exception:
            pass
        os.environ["PATH"] = path + os.pathsep + os.environ.get("PATH", "")


_configure_windows_dll_dirs()

from config import *
from api_client import APIClient
from face_utils import face_cache
from ui_theme import THEMES, apply_theme
from screens.login_screen      import LoginScreen
from screens.register_screen   import RegisterScreen
from screens.attendance_screen import AttendanceScreen
from screens.records_screen    import RecordsScreen
from screens.employees_screen  import EmployeesScreen


# ============================================================
# Dashboard (Home) Screen
# ============================================================

class DashboardScreen(tk.Frame):
    def __init__(self, parent, nav_callback,
                 theme_var=None, themes=None, on_theme_change=None):
        super().__init__(parent, bg=BG_COLOR)
        self.nav = nav_callback
        self._clock_job  = None
        self._theme_var  = theme_var
        self._themes     = themes or []
        self._on_theme_change = on_theme_change
        self._build_ui()

    def _build_ui(self):
        import datetime as _dt
        # ---- Top navigation bar ----------------------------------
        nav_bar = tk.Frame(self, bg=THEME_COLOR)
        nav_bar.pack(fill="x")

        title_sec = tk.Frame(nav_bar, bg=THEME_COLOR)
        title_sec.pack(side="left", padx=20, pady=14)
        tk.Label(title_sec, text="🎯", font=("Segoe UI", 18),
                 bg=THEME_COLOR, fg="white").pack(side="left", padx=(0, 10))
        tk.Label(title_sec, text=APP_TITLE,
                 font=("Segoe UI", 15, "bold"),
                 bg=THEME_COLOR, fg="white").pack(side="left")

        right_nav = tk.Frame(nav_bar, bg=THEME_COLOR)
        right_nav.pack(side="right", padx=20, pady=10)

        # Live clock — rightmost
        self.clock_lbl = tk.Label(right_nav, text="",
                                  font=("Segoe UI", 9),
                                  bg=THEME_COLOR, fg="#A8BDD0")
        self.clock_lbl.pack(side="right", padx=(12, 0))

        # Connection status
        self.conn_lbl = tk.Label(right_nav, text="Checking…",
                                 font=("Segoe UI", 9),
                                 bg=THEME_COLOR, fg="#A8BDD0")
        self.conn_lbl.pack(side="right", padx=(12, 0))

        # Theme selector embedded in nav bar
        if self._themes and self._theme_var:
            tk.Label(right_nav, text="Theme:", font=("Segoe UI", 9),
                     bg=THEME_COLOR, fg="#A8BDD0").pack(side="right", padx=(0, 4))
            theme_combo = ttk.Combobox(
                right_nav,
                textvariable=self._theme_var,
                values=self._themes,
                state="readonly",
                width=13,
                font=("Segoe UI", 9),
            )
            theme_combo.pack(side="right")
            if self._on_theme_change:
                theme_combo.bind("<<ComboboxSelected>>", self._on_theme_change)

        # ---- Stats strip -----------------------------------------
        stats_card = tk.Frame(self, bg=CARD_COLOR,
                              highlightbackground=BORDER_COLOR,
                              highlightthickness=1)
        stats_card.pack(fill="x", padx=20, pady=(18, 0))

        tk.Label(stats_card, text="TODAY'S OVERVIEW",
                 font=("Segoe UI", 8, "bold"),
                 bg=CARD_COLOR, fg=MUTED_COLOR).pack(anchor="w", padx=16, pady=(10, 6))

        stats_row = tk.Frame(stats_card, bg=CARD_COLOR)
        stats_row.pack(fill="x", padx=10, pady=(0, 10))

        self.stat_labels = {}
        for key, title, icon, color in [
            ("total",   "Total Employees", "👤", "#3498DB"),
            ("present", "Present Today",   "✅", ACCENT_COLOR),
            ("absent",  "Absent Today",    "❌", DANGER_COLOR),
            ("late",    "Late Today",      "⏰", WARNING_COLOR),
        ]:
            sc = tk.Frame(stats_row, bg=HOVER_COLOR,
                          highlightbackground=BORDER_COLOR,
                          highlightthickness=1)
            sc.pack(side="left", expand=True, fill="x", padx=4)
            tk.Frame(sc, bg=color, height=3).pack(fill="x")
            inner = tk.Frame(sc, bg=HOVER_COLOR)
            inner.pack(fill="x", padx=12, pady=8)
            top_r = tk.Frame(inner, bg=HOVER_COLOR)
            top_r.pack(fill="x")
            tk.Label(top_r, text=icon, font=("Segoe UI", 15),
                     bg=HOVER_COLOR, fg=color).pack(side="left")
            lbl = tk.Label(top_r, text="—",
                           font=("Segoe UI", 20, "bold"),
                           bg=HOVER_COLOR, fg=color)
            lbl.pack(side="right")
            self.stat_labels[key] = lbl
            tk.Label(inner, text=title, font=("Segoe UI", 8),
                     bg=HOVER_COLOR, fg=MUTED_COLOR).pack(anchor="w", pady=(3, 0))

        # ---- Quick Actions grid ----------------------------------
        actions_sec = tk.Frame(self, bg=BG_COLOR)
        actions_sec.pack(fill="both", expand=True, padx=20, pady=18)

        tk.Label(actions_sec, text="Quick Actions",
                 font=("Segoe UI", 12, "bold"),
                 bg=BG_COLOR, fg=TEXT_COLOR).pack(anchor="w", pady=(0, 12))

        row1 = tk.Frame(actions_sec, bg=BG_COLOR)
        row1.pack(fill="x")
        row2 = tk.Frame(actions_sec, bg=BG_COLOR)
        row2.pack(fill="x", pady=(10, 0))

        buttons_data = [
            ("🎯", "Mark Attendance",    "attendance", ACCENT_COLOR,
             "Live face scanning to auto-mark attendance"),
            ("👤", "Register Employee",  "register",   THEME_COLOR,
             "Capture face data for new employees"),
            ("👥", "Manage Employees",   "employees",  "#8E44AD",
             "View, edit, or deactivate employee records"),
            ("📋", "Attendance Records", "records",    "#2980B9",
             "Filter, view and export attendance history"),
        ]

        for i, (icon, label, page, color, desc) in enumerate(buttons_data):
            container = row1 if i < 2 else row2
            card = tk.Frame(container, bg=CARD_COLOR,
                            highlightbackground=BORDER_COLOR,
                            highlightthickness=1, cursor="hand2")
            card.pack(side="left", fill="both", expand=True,
                      padx=(0, 8) if i % 2 == 0 else (0, 0))

            # Left color sidebar
            tk.Frame(card, bg=color, width=5).pack(side="left", fill="y")

            content = tk.Frame(card, bg=CARD_COLOR)
            content.pack(side="left", fill="both", expand=True,
                         padx=14, pady=14)

            tk.Label(content, text=icon, font=("Segoe UI", 24),
                     bg=CARD_COLOR, fg=color).pack(anchor="w")
            tk.Label(content, text=label,
                     font=("Segoe UI", 12, "bold"),
                     bg=CARD_COLOR, fg=TEXT_COLOR).pack(anchor="w", pady=(6, 3))
            tk.Label(content, text=desc, font=("Segoe UI", 9),
                     bg=CARD_COLOR, fg=MUTED_COLOR,
                     wraplength=200, justify="left").pack(anchor="w")

            arrow = tk.Label(card, text="›", font=("Segoe UI", 22),
                             bg=CARD_COLOR, fg=BORDER_COLOR, cursor="hand2")
            arrow.pack(side="right", padx=10)

            # Bind click to entire card tree
            p, col = page, color
            self._bind_tree(card, "<Button-1>", lambda _e, pg=p: self.nav(pg))
            card.bind("<Enter>", lambda _e, c=card, cl=col: c.configure(
                highlightbackground=cl, highlightthickness=2))
            card.bind("<Leave>", lambda _e, c=card: c.configure(
                highlightbackground=BORDER_COLOR, highlightthickness=1))

    @staticmethod
    def _bind_tree(widget, event, callback):
        widget.bind(event, callback)
        for child in widget.winfo_children():
            DashboardScreen._bind_tree(child, event, callback)

    def on_show(self):
        self._tick_clock()
        threading.Thread(target=self._load_stats, daemon=True).start()

    def on_hide(self):
        if self._clock_job:
            self.after_cancel(self._clock_job)
            self._clock_job = None

    def _tick_clock(self):
        import datetime as _dt
        now = _dt.datetime.now().strftime("%d %b %Y   %H:%M:%S")
        self.clock_lbl.configure(text=now)
        self._clock_job = self.after(1000, self._tick_clock)

    def _load_stats(self):
        ok, msg = APIClient.test_connection()
        if ok:
            self.after(0, lambda: self.conn_lbl.config(
                text=f"✅ Connected  |  v{APP_VERSION}", fg="#2ECC71"))
        else:
            self.after(0, lambda: self.conn_lbl.config(
                text=f"⚠  Offline", fg=WARNING_COLOR))
            return

        result = APIClient.get_stats()
        if result.get("success"):
            d = result["data"].get("today", result["data"])
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
        self.active_theme = "Ocean Mint"
        palette = apply_theme(self.active_theme)

        self.title(APP_TITLE)
        self.configure(bg=palette["BG_COLOR"])
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

        # Apply ttk style and theme selector
        self._configure_ttk(palette)

        # Screen registry
        self.screens = {}
        self.current_screen = None
        self.session_ctx = {}

        self.theme_var = tk.StringVar(value=self.active_theme)
        self._build_screens()
        self._show("login")
        self.protocol("WM_DELETE_WINDOW", self._on_close)

    def _configure_ttk(self, palette):
        style = ttk.Style()
        style.theme_use("clam")

        style.configure(
            "TEntry",
            fieldbackground=palette["CARD_COLOR"],
            foreground=palette["TEXT_COLOR"],
            relief="flat",
            borderwidth=0,
            padding=6,
        )
        style.configure(
            "TCombobox",
            fieldbackground=palette["CARD_COLOR"],
            foreground=palette["TEXT_COLOR"],
            background=palette["CARD_COLOR"],
            arrowcolor=palette["THEME_COLOR"],
            borderwidth=0,
            padding=5,
        )
        style.map(
            "TCombobox",
            fieldbackground=[("readonly", palette["CARD_COLOR"])],
            selectbackground=[("readonly", palette["CARD_COLOR"])],
            selectforeground=[("readonly", palette["TEXT_COLOR"])],
        )
        style.configure("Treeview", background=palette["CARD_COLOR"], foreground=palette["TEXT_COLOR"], rowheight=26)
        style.configure("Treeview.Heading", background=palette["THEME_COLOR"], foreground="white")

    def _on_theme_change(self, _event=None):
        selected = self.theme_var.get().strip()
        if not selected or selected == self.active_theme:
            return

        current_page = self.current_screen or "login"
        self.active_theme = selected
        palette = apply_theme(selected)
        self.configure(bg=palette["BG_COLOR"])
        self._configure_ttk(palette)

        # Rebuild screens so all pages inherit the newly selected palette.
        self._build_screens()
        self._show(current_page)
        self._animate_window_pulse()

    def _animate_window_pulse(self):
        self.attributes("-alpha", 0.94)

        def step(alpha):
            self.attributes("-alpha", alpha)

        self.after(30, lambda: step(0.97))
        self.after(60, lambda: step(1.0))

    def _build_screens(self):
        # Clear old screens when rebuilding (for theme changes).
        for scr in self.screens.values():
            try:
                scr.destroy()
            except Exception:
                pass

        if hasattr(self, "_screen_container") and self._screen_container.winfo_exists():
            self._screen_container.destroy()

        self.screens = {}

        self._screen_container = tk.Frame(self, bg=BG_COLOR)
        self._screen_container.pack(fill="both", expand=True)

        def nav(page):
            self._show(page)

        def login_success(ctx):
            self.session_ctx = ctx or {}
            self._show("dashboard")

        self.screens["login"]      = LoginScreen(self._screen_container, on_success=login_success)
        self.screens["dashboard"]  = DashboardScreen(
            self._screen_container, nav,
            theme_var=self.theme_var,
            themes=list(THEMES.keys()),
            on_theme_change=self._on_theme_change,
        )
        self.screens["register"]   = RegisterScreen(self._screen_container, on_back=lambda: self._show("dashboard"))
        self.screens["attendance"] = AttendanceScreen(self._screen_container, on_back=lambda: self._show("dashboard"))
        self.screens["records"]    = RecordsScreen(self._screen_container, on_back=lambda: self._show("dashboard"))
        self.screens["employees"]  = EmployeesScreen(self._screen_container, on_back=lambda: self._show("dashboard"))

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
            self._animate_window_pulse()

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

# ============================================================
# Login Screen — Split-panel design
# Left: branding panel  |  Right: login form
# ============================================================

import tkinter as tk
from tkinter import ttk
import threading
import datetime

from config import *
from api_client import APIClient


class LoginScreen(tk.Frame):
    def __init__(self, parent, on_success):
        super().__init__(parent, bg=THEME_COLOR)
        self.on_success  = on_success
        self.admin_users = []
        self._clock_job  = None
        self._build_ui()

    # ---- Layout --------------------------------------------

    def _build_ui(self):
        # ---- Left branding panel -------------------------
        left = tk.Frame(self, bg=THEME_COLOR, width=380)
        left.pack(side="left", fill="y")
        left.pack_propagate(False)

        inner_left = tk.Frame(left, bg=THEME_COLOR)
        inner_left.place(relx=0.5, rely=0.46, anchor="center")

        tk.Label(inner_left, text="🎯", font=("Segoe UI", 56),
                 bg=THEME_COLOR, fg="white").pack(pady=(0, 18))
        tk.Label(inner_left, text=APP_TITLE,
                 font=("Segoe UI", 20, "bold"),
                 bg=THEME_COLOR, fg="white").pack()
        tk.Label(inner_left,
                 text="Smart Face Recognition\nAttendance Management",
                 font=("Segoe UI", 10), bg=THEME_COLOR, fg="#A8BDD0",
                 justify="center").pack(pady=(8, 30))

        # Feature bullets
        for icon, feat in [
            ("✅", "Real-time Face Detection"),
            ("📊", "Instant Attendance Reports"),
            ("🔒", "Secure Admin Access"),
            ("☁",  "Cloud-Synced Data"),
        ]:
            row = tk.Frame(inner_left, bg=THEME_COLOR)
            row.pack(anchor="w", pady=3)
            tk.Label(row, text=icon, font=("Segoe UI", 10),
                     bg=THEME_COLOR, fg="white").pack(side="left", padx=(0, 10))
            tk.Label(row, text=feat, font=("Segoe UI", 10),
                     bg=THEME_COLOR, fg="#B8CAD8").pack(side="left")

        # Version + live clock at the bottom
        self.clock_lbl = tk.Label(left, text="", font=("Segoe UI", 9),
                                  bg=THEME_COLOR, fg="#5A7C96")
        self.clock_lbl.pack(side="bottom", pady=(0, 12))
        tk.Label(left, text=f"v{APP_VERSION}", font=("Segoe UI", 9),
                 bg=THEME_COLOR, fg="#4A6C84").pack(side="bottom")

        # Thin right border on left panel
        tk.Frame(self, bg=BORDER_COLOR, width=1).pack(side="left", fill="y")

        # ---- Right form panel ----------------------------
        right = tk.Frame(self, bg=BG_COLOR)
        right.pack(side="left", fill="both", expand=True)

        form_card = tk.Frame(right, bg=CARD_COLOR,
                             highlightbackground=BORDER_COLOR,
                             highlightthickness=1)
        form_card.place(relx=0.5, rely=0.5, anchor="center", width=390)

        tk.Label(form_card, text="Welcome Back",
                 font=("Segoe UI", 20, "bold"),
                 bg=CARD_COLOR, fg=TEXT_COLOR).pack(pady=(28, 2))
        tk.Label(form_card, text="Sign in to continue",
                 font=("Segoe UI", 10), bg=CARD_COLOR,
                 fg=MUTED_COLOR).pack(pady=(0, 18))

        # ---- Tab switcher (pill style) -------------------
        tab_bg = tk.Frame(form_card, bg="#F1F5F9", padx=4, pady=4)
        tab_bg.pack(fill="x", padx=24, pady=(0, 20))

        self.tab_var = tk.StringVar(value="admin")
        self._tab_btns = {}
        for t, lbl in [("admin", "Admin Login"), ("device", "Device PIN")]:
            btn = tk.Button(
                tab_bg, text=lbl, font=FONT_NORMAL,
                relief="flat", cursor="hand2",
                command=lambda v=t: self._switch_tab_to(v)
            )
            btn.pack(side="left", expand=True, fill="x", padx=2, pady=2, ipady=5)
            self._tab_btns[t] = btn
        self._highlight_tab("admin")

        # ---- Admin fields --------------------------------
        self.form_area = tk.Frame(form_card, bg=CARD_COLOR)
        self.form_area.pack(fill="x", padx=24)

        self.admin_frame = tk.Frame(self.form_area, bg=CARD_COLOR)
        self.admin_frame.pack(fill="x")

        self._field_label(self.admin_frame, "USERNAME")
        self.user_var = tk.StringVar()
        self.user_combo = ttk.Combobox(
            self.admin_frame, textvariable=self.user_var,
            font=FONT_NORMAL, width=36, state="readonly", values=[]
        )
        self.user_combo.pack(fill="x", ipady=5, pady=(2, 14))

        self._field_label(self.admin_frame, "PASSWORD")
        self.pass_var = tk.StringVar()
        self.pass_entry = ttk.Entry(
            self.admin_frame, textvariable=self.pass_var,
            font=FONT_NORMAL, show="•", width=36
        )
        self.pass_entry.pack(fill="x", ipady=5, pady=(2, 0))
        self.pass_entry.bind("<Return>", lambda _e: self._do_login())

        # ---- Device PIN fields ---------------------------
        self.device_frame = tk.Frame(self.form_area, bg=CARD_COLOR)

        self._field_label(self.device_frame, "LOCATION ID")
        self.loc_var = tk.StringVar()
        ttk.Entry(self.device_frame, textvariable=self.loc_var,
                  font=FONT_NORMAL, width=36).pack(fill="x", ipady=5, pady=(2, 14))

        self._field_label(self.device_frame, "DEVICE PIN")
        self.pin_var = tk.StringVar()
        pin_entry = ttk.Entry(self.device_frame, textvariable=self.pin_var,
                              font=FONT_NORMAL, show="•", width=36)
        pin_entry.pack(fill="x", ipady=5, pady=(2, 0))
        pin_entry.bind("<Return>", lambda _e: self._do_login())

        # ---- Status + Login button -----------------------
        self.status_var = tk.StringVar(value="")
        tk.Label(form_card, textvariable=self.status_var, font=FONT_SMALL,
                 bg=CARD_COLOR, fg=DANGER_COLOR, wraplength=340).pack(pady=(10, 0))

        self.login_btn = tk.Button(
            form_card, text="Sign In  →",
            font=("Segoe UI", 12, "bold"),
            bg=ACCENT_COLOR, fg="white", relief="flat",
            padx=16, pady=12, cursor="hand2",
            command=self._do_login
        )
        self.login_btn.pack(fill="x", padx=24, pady=(12, 28))
        self.login_btn.bind("<Enter>",
            lambda _e: self.login_btn.configure(bg=self._darken(ACCENT_COLOR)))
        self.login_btn.bind("<Leave>",
            lambda _e: self.login_btn.configure(bg=ACCENT_COLOR))

        self._tick_clock()

    # ---- Helpers -------------------------------------------

    @staticmethod
    def _field_label(parent, text):
        tk.Label(parent, text=text, font=("Segoe UI", 9, "bold"),
                 bg=CARD_COLOR, fg=MUTED_COLOR).pack(anchor="w")

    @staticmethod
    def _darken(hex_color, factor=0.85):
        h = hex_color.lstrip("#")
        r, g, b = int(h[0:2], 16), int(h[2:4], 16), int(h[4:6], 16)
        return f"#{int(r*factor):02x}{int(g*factor):02x}{int(b*factor):02x}"

    def _tick_clock(self):
        now = datetime.datetime.now().strftime("%A, %d %B %Y  •  %H:%M:%S")
        self.clock_lbl.configure(text=now)
        self._clock_job = self.after(1000, self._tick_clock)

    def _highlight_tab(self, active):
        for key, btn in self._tab_btns.items():
            if key == active:
                btn.configure(bg=CARD_COLOR, fg=THEME_COLOR,
                              font=("Segoe UI", 11, "bold"))
            else:
                btn.configure(bg="#F1F5F9", fg=MUTED_COLOR,
                              font=FONT_NORMAL)

    def _switch_tab_to(self, tab):
        self.tab_var.set(tab)
        self._highlight_tab(tab)
        if tab == "admin":
            self.device_frame.pack_forget()
            self.admin_frame.pack(fill="x")
            self._load_admin_users()
        else:
            self.admin_frame.pack_forget()
            self.device_frame.pack(fill="x")

    # ---- Auth logic ----------------------------------------

    def _do_login(self):
        self.login_btn.config(state="disabled", text="Signing in…")
        self.status_var.set("")
        threading.Thread(target=self._login_thread, daemon=True).start()

    def _load_admin_users(self):
        self.status_var.set("Loading users…")
        self.login_btn.config(state="disabled")
        threading.Thread(target=self._load_admin_users_thread, daemon=True).start()

    def _load_admin_users_thread(self):
        data = APIClient.get_admin_users()
        if data.get("success"):
            users = data.get("data", [])
            self.after(0, lambda: self._set_admin_users(users))
        else:
            self.after(0, lambda: self._load_users_failed(
                data.get("message", "Failed to load users")))

    def _set_admin_users(self, users):
        self.admin_users = users
        usernames = [u.get("username", "") for u in users if u.get("username")]
        self.user_combo["values"] = usernames
        if usernames and not self.user_var.get():
            self.user_var.set(usernames[0])
        self.status_var.set("")
        self.login_btn.config(state="normal")

    def _load_users_failed(self, msg):
        self.status_var.set(f"⚠  {msg}")
        self.login_btn.config(state="normal")

    def _login_thread(self):
        tab = self.tab_var.get()
        try:
            if tab == "admin":
                data = APIClient.admin_login(
                    self.user_var.get().strip(),
                    self.pass_var.get(),
                )
            else:
                data = APIClient.pin_login(
                    self.loc_var.get().strip().upper(),
                    self.pin_var.get(),
                )
        except Exception as e:
            self.after(0, lambda: self._login_fail(str(e)))
            return

        if data.get("success"):
            self.after(0, lambda: self._login_ok(data))
        else:
            self.after(0, lambda: self._login_fail(
                data.get("message", "Unknown error")))

    def _login_ok(self, data):
        ctx   = data.get("data") or {}
        token = ctx.get("token", "")

        SESSION["token"]     = token
        SESSION["username"]  = ctx.get("username") or ""
        SESSION["com_id"]    = ctx.get("com_id",    DEFAULT_COM_ID)
        SESSION["branch_id"] = ctx.get("branch_id") or ctx.get("com_id") or ""
        SESSION["loc_id"]    = ctx.get("loc_id")    or ""
        SESSION["role"]      = ctx.get("role",      "admin")

        self.status_var.set("")
        self.login_btn.config(text="Sign In  →", state="normal")
        self.on_success(ctx)

    def _login_fail(self, msg):
        self.status_var.set(f"⚠  {msg}")
        self.login_btn.config(state="normal", text="Sign In  →")

    # ---- Lifecycle -----------------------------------------

    def on_show(self):
        if self.tab_var.get() == "admin":
            self._load_admin_users()

    def on_hide(self):
        if self._clock_job:
            self.after_cancel(self._clock_job)
            self._clock_job = None

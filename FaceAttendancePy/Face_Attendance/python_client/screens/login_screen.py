# ============================================================
# Login Screen — Admin + Device PIN tabs
# Sets SESSION context (com_id, branch_id, loc_id, token)
# ============================================================

import tkinter as tk
from tkinter import ttk, messagebox
import threading

from config import *
from api_client import APIClient


class LoginScreen(tk.Frame):
    def __init__(self, parent, on_success):
        super().__init__(parent, bg=THEME_COLOR)
        self.on_success = on_success
        self.admin_users = []
        self._build_ui()

    def _build_ui(self):
        # Center card
        outer = tk.Frame(self, bg=THEME_COLOR)
        outer.pack(fill="both", expand=True)
        outer.grid_rowconfigure(0, weight=1)
        outer.grid_columnconfigure(0, weight=1)

        card = tk.Frame(outer, bg=CARD_COLOR, padx=32, pady=28,
                        highlightbackground="#DDE1E7", highlightthickness=1)
        card.place(relx=0.5, rely=0.5, anchor="center", width=420)

        # Logo
        tk.Label(card, text="🎯", font=("Segoe UI", 40), bg=CARD_COLOR).pack()
        tk.Label(card, text=APP_TITLE, font=FONT_LARGE,
                 bg=CARD_COLOR, fg=THEME_COLOR).pack(pady=(4, 2))
        tk.Label(card, text=f"v{APP_VERSION}", font=FONT_SMALL,
                 bg=CARD_COLOR, fg=MUTED_COLOR).pack(pady=(0, 16))

        # Tab switcher
        tab_frame = tk.Frame(card, bg=BG_COLOR, bd=0)
        tab_frame.pack(fill="x", pady=(0, 16))
        self.tab_var = tk.StringVar(value="admin")
        for t, lbl in [("admin", "Admin Login"), ("device", "Device PIN")]:
            v = t
            tk.Radiobutton(tab_frame, text=lbl, variable=self.tab_var, value=v,
                           font=FONT_NORMAL, bg=BG_COLOR, command=self._switch_tab,
                           indicatoron=0, padx=12, pady=6,
                           selectcolor=THEME_COLOR, fg=MUTED_COLOR,
                           activebackground=THEME_COLOR, activeforeground="white"
                           ).pack(side="left", expand=True, fill="x")

        # Admin fields
        self.admin_frame = tk.Frame(card, bg=CARD_COLOR)
        self.admin_frame.pack(fill="x")

        tk.Label(self.admin_frame, text="USERNAME", font=FONT_SMALL,
                 bg=CARD_COLOR, fg=MUTED_COLOR).pack(anchor="w")
        self.user_var = tk.StringVar()
        self.user_combo = ttk.Combobox(
            self.admin_frame,
            textvariable=self.user_var,
            font=FONT_NORMAL,
            width=34,
            state="readonly",
            values=[],
        )
        self.user_combo.pack(fill="x", ipady=4, pady=(2, 10))

        tk.Label(self.admin_frame, text="PASSWORD", font=FONT_SMALL,
                 bg=CARD_COLOR, fg=MUTED_COLOR).pack(anchor="w")
        self.pass_var = tk.StringVar()
        ttk.Entry(self.admin_frame, textvariable=self.pass_var,
                  font=FONT_NORMAL, show="•", width=36).pack(fill="x", ipady=4, pady=(2, 16))

        # Device PIN fields
        self.device_frame = tk.Frame(card, bg=CARD_COLOR)

        tk.Label(self.device_frame, text="LOCATION ID", font=FONT_SMALL,
                 bg=CARD_COLOR, fg=MUTED_COLOR).pack(anchor="w")
        self.loc_var = tk.StringVar()
        ttk.Entry(self.device_frame, textvariable=self.loc_var,
                  font=FONT_NORMAL, width=36).pack(fill="x", ipady=4, pady=(2, 10))

        tk.Label(self.device_frame, text="DEVICE PIN", font=FONT_SMALL,
                 bg=CARD_COLOR, fg=MUTED_COLOR).pack(anchor="w")
        self.pin_var = tk.StringVar()
        ttk.Entry(self.device_frame, textvariable=self.pin_var,
                  font=FONT_NORMAL, show="•", width=36).pack(fill="x", ipady=4, pady=(2, 16))

        # Status
        self.status_var = tk.StringVar(value="")
        tk.Label(card, textvariable=self.status_var, font=FONT_SMALL,
                 bg=CARD_COLOR, fg=DANGER_COLOR, wraplength=340).pack()

        # Login button
        self.login_btn = tk.Button(
            card, text="🔓  Sign In", font=FONT_MEDIUM,
            bg=ACCENT_COLOR, fg="white", relief="flat",
            padx=16, pady=10, cursor="hand2",
            command=self._do_login
        )
        self.login_btn.pack(fill="x", pady=(12, 0))

        # Bind Enter
        for widget in [self.user_var, self.pass_var, self.pin_var]:
            card.bind("<Return>", lambda e: self._do_login())

    def _switch_tab(self):
        tab = self.tab_var.get()
        if tab == "admin":
            self.device_frame.pack_forget()
            self.admin_frame.pack(fill="x")
            self._load_admin_users()
        else:
            self.admin_frame.pack_forget()
            self.device_frame.pack(fill="x")

    def _do_login(self):
        self.login_btn.config(state="disabled")
        self.status_var.set("Signing in…")
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
            self.after(0, lambda: self._load_users_failed(data.get("message", "Failed to load users")))

    def _set_admin_users(self, users):
        self.admin_users = users
        usernames = [u.get("username", "") for u in users if u.get("username")]
        self.user_combo["values"] = usernames
        if usernames and not self.user_var.get():
            self.user_var.set(usernames[0])
        self.status_var.set("")
        self.login_btn.config(state="normal")

    def _load_users_failed(self, msg):
        self.status_var.set(f"⚠ {msg}")
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
            self.after(0, lambda: self._login_fail(data.get("message", "Unknown error")))

    def _login_ok(self, data):
        ctx   = data.get("data") or {}
        token = ctx.get("token", "")

        # Save to SESSION
        SESSION["token"]     = token
        SESSION["username"]  = ctx.get("username") or ""
        SESSION["com_id"]    = ctx.get("com_id",    DEFAULT_COM_ID)
        SESSION["branch_id"] = ctx.get("branch_id") or ctx.get("com_id") or ""
        SESSION["loc_id"]    = ctx.get("loc_id")    or ""
        SESSION["role"]      = ctx.get("role",      "admin")

        self.status_var.set("")
        self.on_success(ctx)

    def _login_fail(self, msg):
        self.status_var.set(f"⚠ {msg}")
        self.login_btn.config(state="normal")

    def on_show(self):
        if self.tab_var.get() == "admin":
            self._load_admin_users()

# ============================================================
# Employees Management Screen
# - View all employees
# - Edit / deactivate
# - Delete face data
# ============================================================

import tkinter as tk
from tkinter import ttk
import threading

from config import *
from api_client import APIClient
from ui_dialogs import dialogs as messagebox


class EmployeesScreen(tk.Frame):
    def __init__(self, parent, on_back=None):
        super().__init__(parent, bg=BG_COLOR)
        self.on_back = on_back
        self._build_ui()

    def _build_ui(self):
        # ---- Header ------------------------------------------
        hdr = tk.Frame(self, bg=THEME_COLOR)
        hdr.pack(fill="x")

        left_hdr = tk.Frame(hdr, bg=THEME_COLOR)
        left_hdr.pack(side="left", padx=20, pady=14)
        tk.Label(left_hdr, text="👥", font=("Segoe UI", 18),
                 bg=THEME_COLOR, fg="white").pack(side="left", padx=(0, 10))
        col = tk.Frame(left_hdr, bg=THEME_COLOR)
        col.pack(side="left")
        tk.Label(col, text="Employee Management",
                 font=("Segoe UI", 15, "bold"),
                 bg=THEME_COLOR, fg="white").pack(anchor="w")
        tk.Label(col, text="View, edit or deactivate employees",
                 font=("Segoe UI", 9),
                 bg=THEME_COLOR, fg="#A8BDD0").pack(anchor="w")

        back_btn = tk.Button(hdr, text="← Back", font=FONT_NORMAL,
                             bg=ACCENT_COLOR, fg="white",
                             relief="flat", padx=14, pady=6,
                             cursor="hand2", command=self._go_back)
        back_btn.pack(side="right", padx=20, pady=14)
        back_btn.bind("<Enter>",
            lambda _e: back_btn.configure(bg=self._darken(ACCENT_COLOR)))
        back_btn.bind("<Leave>",
            lambda _e: back_btn.configure(bg=ACCENT_COLOR))

        # ---- Search & filter bar -----------------------------
        bar = tk.Frame(self, bg=CARD_COLOR,
                       highlightbackground=BORDER_COLOR,
                       highlightthickness=1)
        bar.pack(fill="x", padx=20, pady=12)

        top_bar = tk.Frame(bar, bg=CARD_COLOR)
        top_bar.pack(fill="x", padx=14, pady=(10, 6))

        tk.Label(top_bar, text="Search:", font=FONT_SMALL,
                 bg=CARD_COLOR, fg=MUTED_COLOR).pack(side="left", padx=(0, 6))
        self.search_var = tk.StringVar()
        ttk.Entry(top_bar, textvariable=self.search_var,
                  width=26, font=FONT_NORMAL).pack(side="left", ipady=3)

        search_btn = tk.Button(top_bar, text="🔍 Search",
                               font=FONT_SMALL, bg=THEME_COLOR, fg="white",
                               relief="flat", padx=10, pady=5, cursor="hand2",
                               command=self._load)
        search_btn.pack(side="left", padx=8)
        search_btn.bind("<Enter>",
            lambda _e: search_btn.configure(bg=self._darken(THEME_COLOR)))
        search_btn.bind("<Leave>",
            lambda _e: search_btn.configure(bg=THEME_COLOR))

        tk.Label(top_bar, text="Status:", font=FONT_SMALL,
                 bg=CARD_COLOR, fg=MUTED_COLOR).pack(side="left", padx=(12, 6))
        self.status_var = tk.StringVar(value="active")
        for v, lbl in [("active", "Active"), ("inactive", "Inactive")]:
            tk.Radiobutton(top_bar, text=lbl, variable=self.status_var, value=v,
                           font=FONT_SMALL, bg=CARD_COLOR, fg=TEXT_COLOR,
                           selectcolor=SELECTED_COLOR,
                           activebackground=CARD_COLOR,
                           command=self._load).pack(side="left", padx=4)

        refresh_btn = tk.Button(top_bar, text="🔄 Refresh",
                                font=FONT_SMALL, bg=HOVER_COLOR, fg=TEXT_COLOR,
                                relief="flat", padx=10, pady=5, cursor="hand2",
                                command=self._load)
        refresh_btn.pack(side="right")
        refresh_btn.bind("<Enter>",
            lambda _e: refresh_btn.configure(bg=BORDER_COLOR))
        refresh_btn.bind("<Leave>",
            lambda _e: refresh_btn.configure(bg=HOVER_COLOR))

        # ---- Table -------------------------------------------
        self._build_table()

        # ---- Action buttons ----------------------------------
        btn_bar = tk.Frame(self, bg=BG_COLOR)
        btn_bar.pack(fill="x", padx=20, pady=(4, 14))

        del_btn = tk.Button(btn_bar, text="🗑  Delete Face Data",
                            font=FONT_SMALL, bg=WARNING_COLOR, fg="white",
                            relief="flat", padx=12, pady=7,
                            cursor="hand2", command=self._delete_face)
        del_btn.pack(side="left", padx=(0, 8))
        del_btn.bind("<Enter>",
            lambda _e: del_btn.configure(bg=self._darken(WARNING_COLOR)))
        del_btn.bind("<Leave>",
            lambda _e: del_btn.configure(bg=WARNING_COLOR))

        deact_btn = tk.Button(btn_bar, text="⛔  Deactivate Employee",
                              font=FONT_SMALL, bg=DANGER_COLOR, fg="white",
                              relief="flat", padx=12, pady=7,
                              cursor="hand2", command=self._deactivate)
        deact_btn.pack(side="left")
        deact_btn.bind("<Enter>",
            lambda _e: deact_btn.configure(bg=self._darken(DANGER_COLOR)))
        deact_btn.bind("<Leave>",
            lambda _e: deact_btn.configure(bg=DANGER_COLOR))

        self.info_var = tk.StringVar(value="")
        tk.Label(btn_bar, textvariable=self.info_var, font=FONT_SMALL,
                 bg=BG_COLOR, fg=MUTED_COLOR).pack(side="right")

    def _build_table(self):
        cols   = ("#", "Emp ID", "Name", "Department", "Position",
                  "Email", "Phone", "Status", "Registered")
        widths = (40, 80, 150, 120, 130, 180, 110, 80, 100)

        frame = tk.Frame(self, bg=BG_COLOR)
        frame.pack(fill="both", expand=True, padx=20)

        vsb = ttk.Scrollbar(frame, orient="vertical")
        hsb = ttk.Scrollbar(frame, orient="horizontal")

        self.tree = ttk.Treeview(frame, columns=cols, show="headings",
                                 yscrollcommand=vsb.set,
                                 xscrollcommand=hsb.set, height=20)
        for col, w in zip(cols, widths):
            self.tree.heading(col, text=col)
            self.tree.column(col, width=w, anchor="w")

        vsb.config(command=self.tree.yview)
        hsb.config(command=self.tree.xview)
        vsb.pack(side="right", fill="y")
        hsb.pack(side="bottom", fill="x")
        self.tree.pack(fill="both", expand=True)

        self.tree.tag_configure("odd",  background=CARD_COLOR)
        self.tree.tag_configure("even", background=HOVER_COLOR)

        style = ttk.Style()
        style.configure("Treeview.Heading",
                        font=("Segoe UI", 9, "bold"),
                        background=THEME_COLOR, foreground="white")
        style.configure("Treeview", font=FONT_SMALL, rowheight=28)

    # ---- Helpers -------------------------------------------

    @staticmethod
    def _darken(hex_color, factor=0.85):
        h = hex_color.lstrip("#")
        r, g, b = int(h[0:2], 16), int(h[2:4], 16), int(h[4:6], 16)
        return f"#{int(r*factor):02x}{int(g*factor):02x}{int(b*factor):02x}"

    # ---- Data loading --------------------------------------

    def on_show(self):
        self._load()

    def on_hide(self):
        pass

    def _load(self):
        self.info_var.set("Loading…")
        self.tree.delete(*self.tree.get_children())
        threading.Thread(target=self._fetch_thread, daemon=True).start()

    def _fetch_thread(self):
        result = APIClient.get_employees(
            status=self.status_var.get(),
            search=self.search_var.get().strip() or None,
        )
        if result.get("success"):
            emps = result.get("data", [])
            self.after(0, lambda: self._populate(emps))
        else:
            msg = result.get("message", "Error")
            self.after(0, lambda: self.info_var.set(f"⚠  {msg}"))

    def _populate(self, emps):
        self.tree.delete(*self.tree.get_children())
        for i, e in enumerate(emps, 1):
            created = (e.get("created_at") or "")[:10]
            tag = "odd" if i % 2 else "even"
            self.tree.insert("", "end", iid=e["emp_id"], tags=(tag,), values=(
                i,
                e.get("emp_id", ""),
                e.get("emp_printname", e.get("employee_name", e.get("name", ""))),
                e.get("department", ""),
                e.get("position", ""),
                e.get("email", ""),
                e.get("phone", ""),
                e.get("status", "").capitalize(),
                created,
            ))
        self.info_var.set(f"{len(emps)} employee(s) found")

    def _get_selected_id(self):
        sel = self.tree.selection()
        if not sel:
            messagebox.showinfo("Select Employee",
                                "Please select an employee from the list first.")
            return None
        return sel[0]

    def _delete_face(self):
        emp_id = self._get_selected_id()
        if not emp_id:
            return
        name = self.tree.item(emp_id, "values")[2]
        if not messagebox.askyesno(
            "Delete Face Data",
            f"Delete face data for {name}?\n\nThey won't be recognized until re-registered."
        ):
            return
        result = APIClient.delete_face_encodings(emp_id)
        if result.get("success"):
            messagebox.showinfo("Done", f"Face data for {name} deleted successfully.")
        else:
            messagebox.showerror("Error", result.get("message", "Failed to delete face data."))

    def _deactivate(self):
        emp_id = self._get_selected_id()
        if not emp_id:
            return
        name = self.tree.item(emp_id, "values")[2]
        if not messagebox.askyesno(
            "Deactivate Employee",
            f"Deactivate {name}?\n\nThey won't be able to mark attendance."
        ):
            return
        result = APIClient.update_employee(emp_id, status="inactive")
        if result.get("success"):
            messagebox.showinfo("Done", f"{name} has been deactivated.")
            self._load()
        else:
            messagebox.showerror("Error", result.get("message", "Failed to deactivate."))

    def _go_back(self):
        if self.on_back:
            self.on_back()

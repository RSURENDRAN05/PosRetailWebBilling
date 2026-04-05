# ============================================================
# Employees Management Screen
# - View all employees
# - Edit / deactivate
# - Delete face data
# ============================================================

import tkinter as tk
from tkinter import ttk, messagebox
import threading

from config import *
from api_client import APIClient


class EmployeesScreen(tk.Frame):
    def __init__(self, parent, on_back=None):
        super().__init__(parent, bg=BG_COLOR)
        self.on_back  = on_back
        self._build_ui()

    def _build_ui(self):
        # Header
        hdr = tk.Frame(self, bg=THEME_COLOR, pady=12)
        hdr.pack(fill="x")
        tk.Label(hdr, text="👥  Employee Management",
                 font=FONT_LARGE, bg=THEME_COLOR, fg="white").pack(side="left", padx=20)
        tk.Button(hdr, text="← Back", font=FONT_NORMAL, bg=ACCENT_COLOR, fg="white",
                  relief="flat", padx=12, pady=4, cursor="hand2",
                  command=self._go_back).pack(side="right", padx=20)

        # Search bar
        bar = tk.Frame(self, bg=CARD_COLOR,
                       highlightbackground="#DDE1E7", highlightthickness=1)
        bar.pack(fill="x", padx=20, pady=12, ipady=8)

        tk.Label(bar, text="Search:", font=FONT_SMALL, bg=CARD_COLOR).pack(side="left", padx=(12, 4))
        self.search_var = tk.StringVar()
        ttk.Entry(bar, textvariable=self.search_var, width=24, font=FONT_NORMAL).pack(side="left")
        tk.Button(bar, text="🔍", font=FONT_NORMAL, bg=THEME_COLOR, fg="white",
                  relief="flat", padx=8, pady=4, cursor="hand2",
                  command=self._load).pack(side="left", padx=8)

        self.status_var = tk.StringVar(value="active")
        for v, lbl in [("active", "Active"), ("inactive", "Inactive")]:
            tk.Radiobutton(bar, text=lbl, variable=self.status_var, value=v,
                           font=FONT_SMALL, bg=CARD_COLOR,
                           command=self._load).pack(side="left", padx=4)

        tk.Button(bar, text="🔄 Refresh", font=FONT_SMALL, bg=BG_COLOR, fg=TEXT_COLOR,
                  relief="flat", padx=8, pady=4, cursor="hand2",
                  command=self._load).pack(side="right", padx=12)

        # Table
        self._build_table()

        # Action buttons
        btn_bar = tk.Frame(self, bg=BG_COLOR)
        btn_bar.pack(fill="x", padx=20, pady=(4, 12))

        tk.Button(btn_bar, text="🗑 Delete Face Data", font=FONT_SMALL,
                  bg=WARNING_COLOR, fg="white", relief="flat", padx=8, pady=6,
                  cursor="hand2", command=self._delete_face).pack(side="left", padx=(0, 8))

        tk.Button(btn_bar, text="❌ Deactivate Employee", font=FONT_SMALL,
                  bg=DANGER_COLOR, fg="white", relief="flat", padx=8, pady=6,
                  cursor="hand2", command=self._deactivate).pack(side="left")

        self.info_var = tk.StringVar(value="")
        tk.Label(btn_bar, textvariable=self.info_var, font=FONT_SMALL,
                 bg=BG_COLOR, fg=MUTED_COLOR).pack(side="right")

    def _build_table(self):
        cols = ("#", "Emp ID", "Name", "Department", "Position", "Email", "Phone", "Status", "Registered")
        widths = (40, 80, 150, 120, 130, 180, 110, 80, 100)

        frame = tk.Frame(self, bg=BG_COLOR)
        frame.pack(fill="both", expand=True, padx=20)

        vsb = ttk.Scrollbar(frame, orient="vertical")
        hsb = ttk.Scrollbar(frame, orient="horizontal")

        self.tree = ttk.Treeview(frame, columns=cols, show="headings",
                                 yscrollcommand=vsb.set, xscrollcommand=hsb.set, height=20)
        for col, w in zip(cols, widths):
            self.tree.heading(col, text=col)
            self.tree.column(col, width=w, anchor="w")

        vsb.config(command=self.tree.yview)
        hsb.config(command=self.tree.xview)
        vsb.pack(side="right", fill="y")
        hsb.pack(side="bottom", fill="x")
        self.tree.pack(fill="both", expand=True)

        style = ttk.Style()
        style.configure("Treeview.Heading", font=FONT_SMALL)
        style.configure("Treeview", font=FONT_SMALL, rowheight=26)

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
            self.after(0, lambda: self.info_var.set(f"⚠ {msg}"))

    def _populate(self, emps):
        self.tree.delete(*self.tree.get_children())
        for i, e in enumerate(emps, 1):
            created = (e.get("created_at") or "")[:10]
            self.tree.insert("", "end", iid=e["emp_id"], values=(
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
            messagebox.showinfo("Select", "Please select an employee first.")
            return None
        return sel[0]

    def _delete_face(self):
        emp_id = self._get_selected_id()
        if not emp_id:
            return
        name = self.tree.item(emp_id, "values")[2]
        if not messagebox.askyesno("Confirm", f"Delete face data for {name}?\nThey won't be recognized until re-registered."):
            return
        result = APIClient.delete_face_encodings(emp_id)
        if result.get("success"):
            messagebox.showinfo("Done", f"Face data for {name} deleted.")
        else:
            messagebox.showerror("Error", result.get("message", "Failed"))

    def _deactivate(self):
        emp_id = self._get_selected_id()
        if not emp_id:
            return
        name = self.tree.item(emp_id, "values")[2]
        if not messagebox.askyesno("Confirm", f"Deactivate {name}?\nThey won't be able to mark attendance."):
            return
        result = APIClient.update_employee(emp_id, status="inactive")
        if result.get("success"):
            messagebox.showinfo("Done", f"{name} deactivated.")
            self._load()
        else:
            messagebox.showerror("Error", result.get("message", "Failed"))

    def _go_back(self):
        if self.on_back:
            self.on_back()

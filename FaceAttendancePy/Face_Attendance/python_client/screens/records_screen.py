# ============================================================
# Attendance Records Screen
# - Filter by date range, employee, department
# - Tabular view with export to CSV
# ============================================================

import tkinter as tk
from tkinter import ttk, messagebox, filedialog
import threading
import csv
from datetime import date, timedelta

from config import *
from api_client import APIClient


class RecordsScreen(tk.Frame):
    def __init__(self, parent, on_back=None):
        super().__init__(parent, bg=BG_COLOR)
        self.on_back   = on_back
        self._records  = []
        self._build_ui()

    # ---- Layout --------------------------------------------

    def _build_ui(self):
        # Header
        hdr = tk.Frame(self, bg=THEME_COLOR, pady=12)
        hdr.pack(fill="x")
        tk.Label(hdr, text="📋  Attendance Records",
                 font=FONT_LARGE, bg=THEME_COLOR, fg="white").pack(side="left", padx=20)
        tk.Button(hdr, text="← Back", font=FONT_NORMAL, bg=ACCENT_COLOR, fg="white",
                  relief="flat", padx=12, pady=4, cursor="hand2",
                  command=self._go_back).pack(side="right", padx=20)

        # Filter bar
        self._build_filters()

        # Table
        self._build_table()

        # Status bar
        self.status_var = tk.StringVar(value="")
        tk.Label(self, textvariable=self.status_var, font=FONT_SMALL,
                 bg=BG_COLOR, fg=MUTED_COLOR).pack(anchor="w", padx=20, pady=(4, 0))

    def _build_filters(self):
        bar = tk.Frame(self, bg=CARD_COLOR,
                       highlightbackground="#DDE1E7", highlightthickness=1)
        bar.pack(fill="x", padx=20, pady=12, ipady=8)

        tk.Label(bar, text="From:", font=FONT_SMALL, bg=CARD_COLOR).pack(side="left", padx=(12, 4))
        self.from_var = tk.StringVar(value=str(date.today()))
        ttk.Entry(bar, textvariable=self.from_var, width=12, font=FONT_NORMAL).pack(side="left")

        tk.Label(bar, text="To:", font=FONT_SMALL, bg=CARD_COLOR).pack(side="left", padx=(12, 4))
        self.to_var = tk.StringVar(value=str(date.today()))
        ttk.Entry(bar, textvariable=self.to_var, width=12, font=FONT_NORMAL).pack(side="left")

        tk.Label(bar, text="Employee ID:", font=FONT_SMALL, bg=CARD_COLOR).pack(side="left", padx=(12, 4))
        self.emp_var = tk.StringVar()
        ttk.Entry(bar, textvariable=self.emp_var, width=12, font=FONT_NORMAL).pack(side="left")

        tk.Button(bar, text="🔍 Search", font=FONT_NORMAL, bg=THEME_COLOR, fg="white",
                  relief="flat", padx=10, pady=4, cursor="hand2",
                  command=self._load_records).pack(side="left", padx=12)

        tk.Button(bar, text="Today", font=FONT_SMALL, bg=BG_COLOR, fg=TEXT_COLOR,
                  relief="flat", padx=6, pady=4, cursor="hand2",
                  command=self._filter_today).pack(side="left")

        tk.Button(bar, text="This Week", font=FONT_SMALL, bg=BG_COLOR, fg=TEXT_COLOR,
                  relief="flat", padx=6, pady=4, cursor="hand2",
                  command=self._filter_week).pack(side="left")

        tk.Button(bar, text="This Month", font=FONT_SMALL, bg=BG_COLOR, fg=TEXT_COLOR,
                  relief="flat", padx=6, pady=4, cursor="hand2",
                  command=self._filter_month).pack(side="left")

        tk.Button(bar, text="📥 Export CSV", font=FONT_SMALL, bg=ACCENT_COLOR, fg="white",
                  relief="flat", padx=8, pady=4, cursor="hand2",
                  command=self._export_csv).pack(side="right", padx=12)

    def _build_table(self):
        cols = ("#", "Emp ID", "Name", "Department", "Date", "Check-In", "Check-Out", "Duration", "Status")
        col_widths = (40, 80, 150, 120, 100, 90, 90, 80, 80)

        frame = tk.Frame(self, bg=BG_COLOR)
        frame.pack(fill="both", expand=True, padx=20, pady=(0, 12))

        vsb = ttk.Scrollbar(frame, orient="vertical")
        hsb = ttk.Scrollbar(frame, orient="horizontal")

        self.tree = ttk.Treeview(frame, columns=cols, show="headings",
                                 yscrollcommand=vsb.set, xscrollcommand=hsb.set,
                                 height=22)

        for col, w in zip(cols, col_widths):
            self.tree.heading(col, text=col)
            self.tree.column(col, width=w, anchor="center" if col in ("#", "Status") else "w")

        vsb.config(command=self.tree.yview)
        hsb.config(command=self.tree.xview)

        vsb.pack(side="right", fill="y")
        hsb.pack(side="bottom", fill="x")
        self.tree.pack(fill="both", expand=True)

        # Row coloring
        self.tree.tag_configure("late",    background="#FFF3CD")
        self.tree.tag_configure("present", background="#D4EDDA")
        self.tree.tag_configure("absent",  background="#F8D7DA")

        # Style header
        style = ttk.Style()
        style.configure("Treeview.Heading", font=FONT_SMALL, background=THEME_COLOR, foreground="white")
        style.configure("Treeview", font=FONT_SMALL, rowheight=26)

    # ---- Data loading --------------------------------------

    def on_show(self):
        self._load_records()

    def on_hide(self):
        pass

    def _load_records(self):
        self.status_var.set("Loading…")
        self.tree.delete(*self.tree.get_children())
        threading.Thread(target=self._fetch_thread, daemon=True).start()

    def _fetch_thread(self):
        result = APIClient.get_attendance(
            from_date  = self.from_var.get().strip() or None,
            to_date    = self.to_var.get().strip()   or None,
            emp_id     = self.emp_var.get().strip()  or None,
            all_records= True,
        )

        if result.get("success"):
            records = result["data"]["records"]
            summary = result["data"]["summary"]
            self._records = records
            self.after(0, lambda: self._populate_table(records, summary))
        else:
            msg = result.get("message", "Unknown error")
            self.after(0, lambda: self.status_var.set(f"⚠ {msg}"))

    def _populate_table(self, records, summary):
        self.tree.delete(*self.tree.get_children())

        for i, r in enumerate(records, 1):
            ci  = r.get("check_in",  "")
            co  = r.get("check_out", "")
            dur = r.get("duration_mins")

            ci_str  = ci[-8:]  if ci else "—"
            co_str  = co[-8:]  if co else "—"
            dur_str = f"{dur} min" if dur else "—"
            status  = r.get("status", "present").capitalize()

            tag = r.get("status", "present")
            self.tree.insert("", "end", tags=(tag,), values=(
                i,
                r.get("emp_id", ""),
                r.get("employee_name", ""),
                r.get("department", ""),
                r.get("date", ""),
                ci_str,
                co_str,
                dur_str,
                status,
            ))

        total   = summary.get("total", len(records))
        present = summary.get("present", 0)
        late    = summary.get("late", 0)
        self.status_var.set(
            f"Showing {total} record(s)  |  Present: {present}  |  Late: {late}"
        )

    # ---- Quick filters -------------------------------------

    def _filter_today(self):
        today = str(date.today())
        self.from_var.set(today)
        self.to_var.set(today)
        self._load_records()

    def _filter_week(self):
        today = date.today()
        self.from_var.set(str(today - timedelta(days=today.weekday())))
        self.to_var.set(str(today))
        self._load_records()

    def _filter_month(self):
        today = date.today()
        self.from_var.set(str(today.replace(day=1)))
        self.to_var.set(str(today))
        self._load_records()

    # ---- Export CSV ----------------------------------------

    def _export_csv(self):
        if not self._records:
            messagebox.showinfo("No Data", "No records to export.")
            return
        path = filedialog.asksaveasfilename(
            defaultextension=".csv",
            filetypes=[("CSV files", "*.csv")],
            initialfile=f"attendance_{date.today()}.csv"
        )
        if not path:
            return
        try:
            with open(path, "w", newline="", encoding="utf-8") as f:
                writer = csv.DictWriter(f, fieldnames=[
                    "emp_id", "employee_name", "emp_designation",
                    "att_date", "check_in", "check_out", "total_work_hours", "status"
                ], extrasaction='ignore')
                writer.writeheader()
                writer.writerows(self._records)
            messagebox.showinfo("Exported", f"Saved {len(self._records)} records to:\n{path}")
        except Exception as e:
            messagebox.showerror("Export Error", str(e))

    # ---- Navigation ----------------------------------------

    def _go_back(self):
        if self.on_back:
            self.on_back()

# ============================================================
# Attendance Records Screen
# - Filter by date range, employee, department
# - Tabular view with export to CSV
# ============================================================

import tkinter as tk
from tkinter import ttk, filedialog
import threading
import csv
import calendar
from datetime import date, timedelta

from config import *
from api_client import APIClient
from ui_dialogs import dialogs as messagebox


class DatePickerPopup(tk.Toplevel):
    def __init__(self, parent, initial_date, on_select):
        super().__init__(parent)
        self.configure(bg="#B6C9C9")
        self.resizable(False, False)
        self.transient(parent)
        self.title("Select Date")

        self._on_select = on_select
        self._selected_date = initial_date
        self._view_month = initial_date.replace(day=1)

        self._build_ui()
        self._render_month()

        self.bind("<Escape>", lambda _e: self.destroy())
        self.grab_set()

    def _build_ui(self):
        card = tk.Frame(self, bg="#FFFFFF", highlightbackground="#D8E0E8", highlightthickness=1)
        card.pack(padx=10, pady=10)

        top = tk.Frame(card, bg="#FFFFFF")
        top.pack(fill="x", padx=14, pady=(12, 8))

        tk.Label(top, text="Select Date", bg="#FFFFFF", fg="#1E2A36", font=("Segoe UI", 11, "bold")).pack(side="left")
        tk.Button(top, text="x", bg="#FFFFFF", fg="#9AA7B5", relief="flat", cursor="hand2",
                  font=("Segoe UI", 10), command=self.destroy).pack(side="right")

        nav = tk.Frame(card, bg="#FFFFFF")
        nav.pack(fill="x", padx=14, pady=(0, 8))

        tk.Button(nav, text="◀", bg="#EFF3F7", fg="#425466", relief="flat", width=2, cursor="hand2",
                  command=lambda: self._change_month(-1)).pack(side="left")
        self.month_lbl = tk.Label(nav, text="", bg="#FFFFFF", fg="#1E2A36", font=("Segoe UI", 10, "bold"))
        self.month_lbl.pack(side="left", expand=True)
        tk.Button(nav, text="▶", bg="#EFF3F7", fg="#425466", relief="flat", width=2, cursor="hand2",
                  command=lambda: self._change_month(1)).pack(side="right")

        wk = tk.Frame(card, bg="#FFFFFF")
        wk.pack(fill="x", padx=14)
        for d in ["Sun", "Mon", "Tue", "Wed", "Thu", "Fri", "Sat"]:
            tk.Label(wk, text=d, bg="#FFFFFF", fg="#7A8793", width=4, font=("Segoe UI", 8, "bold")).pack(side="left")

        self.days_frame = tk.Frame(card, bg="#FFFFFF")
        self.days_frame.pack(padx=14, pady=(4, 14))

    def _change_month(self, offset):
        y = self._view_month.year
        m = self._view_month.month + offset
        if m < 1:
            y -= 1
            m = 12
        elif m > 12:
            y += 1
            m = 1
        self._view_month = self._view_month.replace(year=y, month=m, day=1)
        self._render_month()

    def _render_month(self):
        for child in self.days_frame.winfo_children():
            child.destroy()

        self.month_lbl.configure(text=self._view_month.strftime("%B, %Y"))

        cal = calendar.Calendar(firstweekday=6)
        for week in cal.monthdayscalendar(self._view_month.year, self._view_month.month):
            row = tk.Frame(self.days_frame, bg="#FFFFFF")
            row.pack()
            for day in week:
                if day == 0:
                    tk.Label(row, text="", width=4, bg="#FFFFFF").pack(side="left", padx=1, pady=1)
                    continue

                d = date(self._view_month.year, self._view_month.month, day)
                is_selected = d == self._selected_date
                bg = "#2E7A9C" if is_selected else "#FFFFFF"
                fg = "#FFFFFF" if is_selected else "#233444"
                tk.Button(
                    row, text=f"{day:02d}", width=4, relief="flat", cursor="hand2",
                    bg=bg, fg=fg, activebackground="#D9E8EF", activeforeground="#233444",
                    command=lambda picked=d: self._pick(picked)
                ).pack(side="left", padx=1, pady=1)

    def _pick(self, picked):
        self._selected_date = picked
        self._on_select(picked)
        self.destroy()


class RecordsScreen(tk.Frame):
    def __init__(self, parent, on_back=None):
        super().__init__(parent, bg=BG_COLOR)
        self.on_back   = on_back
        self._records  = []
        self._employees = []
        self._emp_display_to_id = {}
        self._quick_filter_buttons = {}
        self._pulse_job = None
        self._pulse_phase = 0
        self._date_popup = None
        self._build_ui()

    # ---- Layout --------------------------------------------

    @staticmethod
    def _darken(hex_color, factor=0.85):
        h = hex_color.lstrip("#")
        r, g, b = int(h[0:2], 16), int(h[2:4], 16), int(h[4:6], 16)
        return f"#{int(r*factor):02x}{int(g*factor):02x}{int(b*factor):02x}"

    def _build_ui(self):
        # ---- Header ------------------------------------------
        hdr = tk.Frame(self, bg=THEME_COLOR)
        hdr.pack(fill="x")

        left_hdr = tk.Frame(hdr, bg=THEME_COLOR)
        left_hdr.pack(side="left", padx=20, pady=14)
        tk.Label(left_hdr, text="📋", font=("Segoe UI", 18),
                 bg=THEME_COLOR, fg="white").pack(side="left", padx=(0, 10))
        col = tk.Frame(left_hdr, bg=THEME_COLOR)
        col.pack(side="left")
        tk.Label(col, text="Attendance Records",
                 font=("Segoe UI", 15, "bold"),
                 bg=THEME_COLOR, fg="white").pack(anchor="w")
        tk.Label(col, text="Filter, view and export attendance history",
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

        # Filter bar
        self._build_filters()

        # Table
        self._build_table()

        # Status bar
        status_bar = tk.Frame(self, bg=CARD_COLOR,
                              highlightbackground=BORDER_COLOR,
                              highlightthickness=1)
        status_bar.pack(fill="x", padx=20, pady=(0, 4))
        self.status_var = tk.StringVar(value="")
        tk.Label(status_bar, textvariable=self.status_var, font=FONT_SMALL,
                 bg=CARD_COLOR, fg=MUTED_COLOR).pack(
                     anchor="w", padx=14, pady=6)

    def _build_filters(self):
        bar = tk.Frame(self, bg=CARD_COLOR, highlightbackground=BORDER_COLOR, highlightthickness=1)
        bar.pack(fill="x", padx=20, pady=12)

        top = tk.Frame(bar, bg=CARD_COLOR)
        top.pack(fill="x", padx=12, pady=(10, 6))

        tk.Label(top, text="From", font=FONT_SMALL, bg=CARD_COLOR, fg=MUTED_COLOR).pack(side="left", padx=(0, 6))
        self.from_var = tk.StringVar(value=str(date.today()))
        self.from_prev_btn = tk.Button(top, text="◀", font=FONT_SMALL, bg="#EEF2F7", fg=TEXT_COLOR,
                                       relief="flat", width=2, cursor="hand2",
                                       command=lambda: self._shift_date(self.from_var, -1))
        self.from_prev_btn.pack(side="left")
        self.from_picker_btn = tk.Button(
            top, text="", font=FONT_NORMAL, bg="#F6F9FC", fg=TEXT_COLOR,
            relief="flat", padx=10, pady=5, cursor="hand2",
            command=lambda: self._open_date_picker("from", self.from_picker_btn)
        )
        self.from_picker_btn.pack(side="left", padx=4)
        self.from_next_btn = tk.Button(top, text="▶", font=FONT_SMALL, bg="#EEF2F7", fg=TEXT_COLOR,
                                       relief="flat", width=2, cursor="hand2",
                                       command=lambda: self._shift_date(self.from_var, 1))
        self.from_next_btn.pack(side="left")

        tk.Label(top, text="To", font=FONT_SMALL, bg=CARD_COLOR, fg=MUTED_COLOR).pack(side="left", padx=(14, 6))
        self.to_var = tk.StringVar(value=str(date.today()))
        self.to_prev_btn = tk.Button(top, text="◀", font=FONT_SMALL, bg="#EEF2F7", fg=TEXT_COLOR,
                                     relief="flat", width=2, cursor="hand2",
                                     command=lambda: self._shift_date(self.to_var, -1))
        self.to_prev_btn.pack(side="left")
        self.to_picker_btn = tk.Button(
            top, text="", font=FONT_NORMAL, bg="#F6F9FC", fg=TEXT_COLOR,
            relief="flat", padx=10, pady=5, cursor="hand2",
            command=lambda: self._open_date_picker("to", self.to_picker_btn)
        )
        self.to_picker_btn.pack(side="left", padx=4)
        self.to_next_btn = tk.Button(top, text="▶", font=FONT_SMALL, bg="#EEF2F7", fg=TEXT_COLOR,
                                     relief="flat", width=2, cursor="hand2",
                                     command=lambda: self._shift_date(self.to_var, 1))
        self.to_next_btn.pack(side="left")

        tk.Label(top, text="Employee", font=FONT_SMALL, bg=CARD_COLOR, fg=MUTED_COLOR).pack(side="left", padx=(14, 6))
        self.emp_display_var = tk.StringVar(value="All Employees")
        self.emp_combo = ttk.Combobox(
            top, textvariable=self.emp_display_var, width=28, font=FONT_NORMAL,
            values=["All Employees"], state="readonly"
        )
        self.emp_combo.pack(side="left")

        self.search_btn = tk.Button(top, text="Search", font=FONT_NORMAL, bg=THEME_COLOR, fg="white",
                                    relief="flat", padx=16, pady=6, cursor="hand2",
                                    command=self._load_records)
        self.search_btn.pack(side="left", padx=(12, 0))

        bottom = tk.Frame(bar, bg=CARD_COLOR)
        bottom.pack(fill="x", padx=12, pady=(0, 10))

        tk.Label(bottom, text="Quick Range", font=FONT_SMALL, bg=CARD_COLOR, fg=MUTED_COLOR).pack(side="left", padx=(0, 8))

        self._quick_filter_buttons["today"] = self._create_filter_chip(bottom, "Today", self._filter_today)
        self._quick_filter_buttons["week"] = self._create_filter_chip(bottom, "This Week", self._filter_week)
        self._quick_filter_buttons["month"] = self._create_filter_chip(bottom, "This Month", self._filter_month)

        self.export_btn = tk.Button(bottom, text="Export CSV", font=FONT_SMALL, bg=ACCENT_COLOR, fg="white",
                                    relief="flat", padx=10, pady=5, cursor="hand2",
                                    command=self._export_csv)
        self.export_btn.pack(side="right")

        self.refresh_emp_btn = tk.Button(bottom, text="Reload Employees", font=FONT_SMALL,
                                         bg="#EEF2F7", fg=TEXT_COLOR, relief="flat",
                                         padx=10, pady=5, cursor="hand2",
                                         command=self._load_employee_filters)
        self.refresh_emp_btn.pack(side="right", padx=(0, 8))

        for btn in [
            self.from_prev_btn, self.from_next_btn, self.to_prev_btn, self.to_next_btn,
            self.refresh_emp_btn, self.from_picker_btn, self.to_picker_btn
        ]:
            if btn in (self.from_picker_btn, self.to_picker_btn):
                self._bind_hover(btn, "#F6F9FC", "#EAF1F8")
            else:
                self._bind_hover(btn, "#EEF2F7", "#E0E8F2")

        self._bind_hover(self.search_btn, THEME_COLOR, "#3A5168")
        self._bind_hover(self.export_btn, ACCENT_COLOR, "#219653")

        self._refresh_date_buttons()

    def _create_filter_chip(self, parent, text, command):
        btn = tk.Button(parent, text=text, font=FONT_SMALL, bg="#EEF2F7", fg=TEXT_COLOR,
                        relief="flat", padx=10, pady=5, cursor="hand2",
                        command=command)
        btn.pack(side="left", padx=(0, 6))
        self._bind_hover(btn, "#EEF2F7", "#DFE8F3")
        return btn

    def _set_active_quick_filter(self, key):
        for name, btn in self._quick_filter_buttons.items():
            if name == key:
                btn.configure(bg=THEME_COLOR, fg="white")
            else:
                btn.configure(bg="#EEF2F7", fg=TEXT_COLOR)

    def _bind_hover(self, button, base_color, hover_color):
        button.bind("<Enter>", lambda _e: self._animate_button_bg(button, hover_color))
        button.bind("<Leave>", lambda _e: self._animate_button_bg(button, base_color))

    def _animate_button_bg(self, button, target_color, steps=5, delay=16):
        try:
            start_color = button.cget("bg")
            s_r, s_g, s_b = self._hex_to_rgb(start_color)
            t_r, t_g, t_b = self._hex_to_rgb(target_color)
        except Exception:
            button.configure(bg=target_color)
            return

        for i in range(1, steps + 1):
            ratio = i / steps
            r = int(s_r + (t_r - s_r) * ratio)
            g = int(s_g + (t_g - s_g) * ratio)
            b = int(s_b + (t_b - s_b) * ratio)
            color = f"#{r:02x}{g:02x}{b:02x}"
            button.after(i * delay, lambda c=color, btn=button: btn.configure(bg=c))

    @staticmethod
    def _hex_to_rgb(hex_color):
        hex_color = (hex_color or "").strip().lstrip("#")
        if len(hex_color) != 6:
            raise ValueError("Invalid color")
        return int(hex_color[0:2], 16), int(hex_color[2:4], 16), int(hex_color[4:6], 16)

    def _shift_date(self, target_var, days):
        try:
            current = date.fromisoformat(target_var.get().strip())
        except Exception:
            current = date.today()
        new_value = str(current + timedelta(days=days))
        target_var.set(new_value)
        self._refresh_date_buttons()
        self._set_active_quick_filter(None)

    def _refresh_date_buttons(self):
        self.from_picker_btn.configure(text=f"📅 {self.from_var.get()}")
        self.to_picker_btn.configure(text=f"📅 {self.to_var.get()}")

    def _open_date_picker(self, which, anchor_widget):
        target_var = self.from_var if which == "from" else self.to_var
        try:
            initial = date.fromisoformat(target_var.get().strip())
        except Exception:
            initial = date.today()

        if self._date_popup and self._date_popup.winfo_exists():
            self._date_popup.destroy()

        self._date_popup = DatePickerPopup(
            self,
            initial_date=initial,
            on_select=lambda picked: self._on_date_picked(target_var, picked)
        )
        x = anchor_widget.winfo_rootx() - 20
        y = anchor_widget.winfo_rooty() + anchor_widget.winfo_height() + 8
        self._date_popup.geometry(f"+{x}+{y}")

    def _on_date_picked(self, target_var, picked_date):
        target_var.set(str(picked_date))
        self._refresh_date_buttons()
        self._set_active_quick_filter(None)

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
        self._load_employee_filters()
        self._load_records()

    def on_hide(self):
        self._stop_search_pulse()
        if self._date_popup and self._date_popup.winfo_exists():
            self._date_popup.destroy()

    def _load_records(self):
        if self._invalid_date_range():
            messagebox.showwarning("Invalid Date Range", "From date cannot be after To date.")
            return
        self.status_var.set("Loading…")
        self.tree.delete(*self.tree.get_children())
        self._set_loading_state(True)
        threading.Thread(target=self._fetch_thread, daemon=True).start()

    def _fetch_thread(self):
        selected_emp_id = self._selected_emp_id()
        result = APIClient.get_attendance(
            from_date  = self.from_var.get().strip() or None,
            to_date    = self.to_var.get().strip()   or None,
            emp_id     = selected_emp_id,
            all_records= True,
        )

        if result.get("success"):
            data = result.get("data")
            if isinstance(data, dict) and "records" in data:
                records = data.get("records", [])
                summary = data.get("summary", {})
            else:
                records = data if isinstance(data, list) else []
                present = sum(1 for r in records if r.get("morning_in") or r.get("check_in"))
                late = sum(
                    1 for r in records
                    if (r.get("morning_in") or r.get("check_in", ""))[-8:] > "09:00:00"
                )
                summary = {"total": len(records), "present": present, "late": late}
            self._records = records
            self.after(0, lambda: self._populate_table(records, summary))
        else:
            msg = result.get("message", "Unknown error")
            self.after(0, lambda: self.status_var.set(f"⚠ {msg}"))
            self.after(0, lambda: self._set_loading_state(False))

    def _invalid_date_range(self):
        try:
            from_d = date.fromisoformat(self.from_var.get().strip())
            to_d = date.fromisoformat(self.to_var.get().strip())
            return from_d > to_d
        except Exception:
            return False

    def _set_loading_state(self, loading):
        if loading:
            self.search_btn.configure(state="disabled", text="Searching...")
            self._start_search_pulse()
        else:
            self.search_btn.configure(state="normal", text="Search", bg=THEME_COLOR)
            self._stop_search_pulse()

    def _start_search_pulse(self):
        self._stop_search_pulse()
        self._pulse_phase = 0
        self._pulse_step()

    def _pulse_step(self):
        pulse_colors = ["#2C3E50", "#36506A", "#3F5E7E", "#36506A"]
        color = pulse_colors[self._pulse_phase % len(pulse_colors)]
        self.search_btn.configure(bg=color)
        self._pulse_phase += 1
        self._pulse_job = self.after(120, self._pulse_step)

    def _stop_search_pulse(self):
        if self._pulse_job:
            self.after_cancel(self._pulse_job)
            self._pulse_job = None

    def _selected_emp_id(self):
        selected = self.emp_display_var.get().strip()
        if not selected or selected == "All Employees":
            return None
        return self._emp_display_to_id.get(selected)

    def _load_employee_filters(self):
        threading.Thread(target=self._fetch_employee_filters_thread, daemon=True).start()

    def _fetch_employee_filters_thread(self):
        result = APIClient.get_employees(status="active")
        if not result.get("success"):
            return

        employees = result.get("data", [])
        self._employees = employees
        self.after(0, lambda: self._populate_employee_filters(employees))

    def _populate_employee_filters(self, employees):
        values = ["All Employees"]
        display_to_id = {}

        for e in employees:
            emp_id = str(e.get("emp_id", "")).strip()
            if not emp_id:
                continue
            name = e.get("emp_printname") or e.get("employee_name") or e.get("emp_name") or ""
            display = f"{emp_id} - {name}" if name else emp_id
            values.append(display)
            display_to_id[display] = emp_id

        self._emp_display_to_id = display_to_id
        self.emp_combo["values"] = values
        if self.emp_display_var.get() not in values:
            self.emp_display_var.set("All Employees")

    def _populate_table(self, records, summary):
        self._set_loading_state(False)
        self.tree.delete(*self.tree.get_children())

        for i, r in enumerate(records, 1):
            ci = r.get("check_in") or r.get("morning_in") or ""
            co = r.get("check_out") or r.get("evening_out") or ""
            dur = r.get("duration_mins")
            work_hours = r.get("total_work_hours")

            ci_str  = ci[-8:]  if ci else "—"
            co_str  = co[-8:]  if co else "—"
            if dur not in (None, ""):
                dur_str = f"{dur} min"
            elif work_hours not in (None, ""):
                dur_str = str(work_hours)
            else:
                dur_str = "—"
            status  = r.get("status", "present").capitalize()

            tag = str(r.get("status", "present")).lower()
            self.tree.insert("", "end", tags=(tag,), values=(
                i,
                r.get("emp_id", ""),
                r.get("employee_name") or r.get("emp_printname") or r.get("emp_name", ""),
                r.get("department") or r.get("emp_designation", ""),
                r.get("date") or r.get("att_date", ""),
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
        self._refresh_date_buttons()
        self._set_active_quick_filter("today")
        self._load_records()

    def _filter_week(self):
        today = date.today()
        self.from_var.set(str(today - timedelta(days=today.weekday())))
        self.to_var.set(str(today))
        self._refresh_date_buttons()
        self._set_active_quick_filter("week")
        self._load_records()

    def _filter_month(self):
        today = date.today()
        self.from_var.set(str(today.replace(day=1)))
        self.to_var.set(str(today))
        self._refresh_date_buttons()
        self._set_active_quick_filter("month")
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
            rows = []
            for r in self._records:
                rows.append({
                    "emp_id": r.get("emp_id", ""),
                    "employee_name": r.get("employee_name") or r.get("emp_printname") or r.get("emp_name", ""),
                    "emp_designation": r.get("emp_designation") or r.get("department", ""),
                    "att_date": r.get("att_date") or r.get("date", ""),
                    "check_in": r.get("check_in") or r.get("morning_in", ""),
                    "check_out": r.get("check_out") or r.get("evening_out", ""),
                    "total_work_hours": r.get("total_work_hours") if r.get("total_work_hours") not in (None, "") else r.get("duration_mins", ""),
                    "status": r.get("status", "present"),
                })

            with open(path, "w", newline="", encoding="utf-8") as f:
                writer = csv.DictWriter(f, fieldnames=[
                    "emp_id", "employee_name", "emp_designation",
                    "att_date", "check_in", "check_out", "total_work_hours", "status"
                ], extrasaction='ignore')
                writer.writeheader()
                writer.writerows(rows)
            messagebox.showinfo("Exported", f"Saved {len(self._records)} records to:\n{path}")
        except Exception as e:
            messagebox.showerror("Export Error", str(e))

    # ---- Navigation ----------------------------------------

    def _go_back(self):
        self._stop_search_pulse()
        if self._date_popup and self._date_popup.winfo_exists():
            self._date_popup.destroy()
        if self.on_back:
            self.on_back()

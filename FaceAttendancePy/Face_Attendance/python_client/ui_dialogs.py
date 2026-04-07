import tkinter as tk
from config import (THEME_COLOR, ACCENT_COLOR, DANGER_COLOR, WARNING_COLOR,
                    CARD_COLOR, TEXT_COLOR, MUTED_COLOR, FONT_NORMAL)


class AnimatedDialog(tk.Toplevel):
    """Modern animated dialog with colored icon badge and smooth fade."""

    def __init__(self, parent, title, message, kind="info", ask=False):
        super().__init__(parent)
        self.result = False
        self._ask = ask

        self.overrideredirect(True)
        self.configure(bg="#B8C4CE")    # outer shadow border
        self.transient(parent)
        self.attributes("-alpha", 0.0)

        width  = 460
        height = 252 if ask else 220
        x = parent.winfo_rootx() + max((parent.winfo_width()  - width)  // 2, 10)
        y = parent.winfo_rooty() + max((parent.winfo_height() - height) // 2, 10)
        self.geometry(f"{width}x{height}+{x}+{y}")

        icon_char, stripe = self._style_for(kind)

        # ---- Card ------------------------------------------------
        card = tk.Frame(self, bg=CARD_COLOR,
                        highlightbackground="#C8D2DC", highlightthickness=1)
        card.pack(fill="both", expand=True, padx=3, pady=3)

        # Top color accent stripe
        tk.Frame(card, bg=stripe, height=4).pack(fill="x")

        # ---- Header row ------------------------------------------
        top = tk.Frame(card, bg=CARD_COLOR)
        top.pack(fill="x", padx=18, pady=(14, 0))

        # Colored icon badge
        badge = tk.Frame(top, bg=stripe, width=36, height=36)
        badge.pack(side="left")
        badge.pack_propagate(False)
        tk.Label(badge, text=icon_char, font=("Segoe UI", 14, "bold"),
                 bg=stripe, fg="white").place(relx=0.5, rely=0.5, anchor="center")

        tk.Label(top, text=f"  {title}",
                 font=("Segoe UI", 13, "bold"), bg=CARD_COLOR, fg=TEXT_COLOR
                 ).pack(side="left")

        close_btn = tk.Button(top, text="×", font=("Segoe UI", 17),
                              bg=CARD_COLOR, fg=MUTED_COLOR, relief="flat",
                              cursor="hand2", bd=0, command=self._close_no)
        close_btn.pack(side="right")
        close_btn.bind("<Enter>", lambda _e: close_btn.configure(fg=DANGER_COLOR))
        close_btn.bind("<Leave>", lambda _e: close_btn.configure(fg=MUTED_COLOR))

        # ---- Message ---------------------------------------------
        tk.Label(card, text=message, bg=CARD_COLOR, fg=TEXT_COLOR,
                 justify="left", wraplength=408,
                 font=FONT_NORMAL).pack(fill="x", padx=18, pady=(12, 10))

        # ---- Divider ---------------------------------------------
        tk.Frame(card, bg="#E8ECF0", height=1).pack(fill="x", padx=18)

        # ---- Buttons ---------------------------------------------
        btn_row = tk.Frame(card, bg=CARD_COLOR)
        btn_row.pack(fill="x", padx=18, pady=(10, 16))

        if ask:
            cancel_btn = tk.Button(
                btn_row, text="Cancel", font=FONT_NORMAL,
                bg="#F1F5F9", fg=TEXT_COLOR, relief="flat",
                padx=20, pady=7, cursor="hand2", command=self._close_no
            )
            cancel_btn.pack(side="right", padx=(10, 0))
            self._bind_hover(cancel_btn, "#F1F5F9", "#E2E8F0")

            confirm_btn = tk.Button(
                btn_row, text="Confirm", font=FONT_NORMAL,
                bg=stripe, fg="white", relief="flat",
                padx=20, pady=7, cursor="hand2", command=self._close_yes
            )
            confirm_btn.pack(side="right")
            self._bind_hover(confirm_btn, stripe, self._darken(stripe))
        else:
            ok_btn = tk.Button(
                btn_row, text="   OK   ", font=FONT_NORMAL,
                bg=ACCENT_COLOR, fg="white", relief="flat",
                padx=20, pady=7, cursor="hand2", command=self._close_yes
            )
            ok_btn.pack(side="right")
            self._bind_hover(ok_btn, ACCENT_COLOR, self._darken(ACCENT_COLOR))

        self.bind("<Return>", lambda _e: self._close_yes())
        self.bind("<Escape>", lambda _e: self._close_no())
        self.after(1, self._fade_in)

    # ---- Helpers -----------------------------------------------

    @staticmethod
    def _bind_hover(btn, normal, hover):
        btn.bind("<Enter>", lambda _e: btn.configure(bg=hover))
        btn.bind("<Leave>", lambda _e: btn.configure(bg=normal))

    @staticmethod
    def _darken(hex_color, factor=0.82):
        h = hex_color.lstrip("#")
        r, g, b = int(h[0:2], 16), int(h[2:4], 16), int(h[4:6], 16)
        return f"#{int(r*factor):02x}{int(g*factor):02x}{int(b*factor):02x}"

    @staticmethod
    def _style_for(kind):
        if kind == "error":
            return "✕", DANGER_COLOR
        if kind == "warning":
            return "!", WARNING_COLOR
        return "✓", THEME_COLOR

    def _fade_in(self):
        alpha = self.attributes("-alpha")
        alpha = min(1.0, alpha + 0.14)
        self.attributes("-alpha", alpha)
        if alpha < 1.0:
            self.after(16, self._fade_in)

    def _fade_out(self):
        alpha = self.attributes("-alpha")
        alpha = max(0.0, alpha - 0.18)
        self.attributes("-alpha", alpha)
        if alpha > 0.0:
            self.after(14, self._fade_out)
        else:
            self.destroy()

    def _close_yes(self):
        self.result = True
        self._fade_out()

    def _close_no(self):
        self.result = False
        self._fade_out()


class DialogService:
    def _show(self, parent, title, message, kind="info", ask=False):
        root = parent
        if root is None:
            root = tk._default_root
        if root is None:
            return False if ask else None

        dlg = AnimatedDialog(root, title, message, kind=kind, ask=ask)
        dlg.grab_set()
        dlg.wait_window()
        return dlg.result if ask else None

    def showinfo(self, title, message, parent=None):
        self._show(parent, title, message, kind="info", ask=False)

    def showwarning(self, title, message, parent=None):
        self._show(parent, title, message, kind="warning", ask=False)

    def showerror(self, title, message, parent=None):
        self._show(parent, title, message, kind="error", ask=False)

    def askyesno(self, title, message, parent=None):
        return self._show(parent, title, message, kind="warning", ask=True)


dialogs = DialogService()

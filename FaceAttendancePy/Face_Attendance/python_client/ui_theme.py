import sys
from typing import Dict

THEMES: Dict[str, Dict[str, str]] = {
    "Ocean Mint": {
        "THEME_COLOR":    "#2C3E50",
        "ACCENT_COLOR":   "#27AE60",
        "DANGER_COLOR":   "#E74C3C",
        "WARNING_COLOR":  "#F39C12",
        "BG_COLOR":       "#ECF0F1",
        "CARD_COLOR":     "#FFFFFF",
        "TEXT_COLOR":     "#2C3E50",
        "MUTED_COLOR":    "#95A5A6",
        "BORDER_COLOR":   "#DDE1E7",
        "HOVER_COLOR":    "#EEF2F7",
        "SELECTED_COLOR": "#E8F4FD",
    },
    "Royal Indigo": {
        "THEME_COLOR":    "#3D3BC2",
        "ACCENT_COLOR":   "#2E7DFF",
        "DANGER_COLOR":   "#E84A5F",
        "WARNING_COLOR":  "#FFB703",
        "BG_COLOR":       "#EDF0FA",
        "CARD_COLOR":     "#FFFFFF",
        "TEXT_COLOR":     "#1F2A44",
        "MUTED_COLOR":    "#7C87A1",
        "BORDER_COLOR":   "#D4D8F0",
        "HOVER_COLOR":    "#E5E8FF",
        "SELECTED_COLOR": "#DCE0FF",
    },
    "Rose Coral": {
        "THEME_COLOR":    "#BD4B6A",
        "ACCENT_COLOR":   "#2D7C7A",
        "DANGER_COLOR":   "#D64550",
        "WARNING_COLOR":  "#F08A24",
        "BG_COLOR":       "#F8F1F2",
        "CARD_COLOR":     "#FFFFFF",
        "TEXT_COLOR":     "#3C2A30",
        "MUTED_COLOR":    "#8E7C82",
        "BORDER_COLOR":   "#DCCFD2",
        "HOVER_COLOR":    "#F5EAEC",
        "SELECTED_COLOR": "#F2E0E4",
    },
    "Midnight Dark": {
        "THEME_COLOR":    "#7C5CBF",
        "ACCENT_COLOR":   "#56C596",
        "DANGER_COLOR":   "#FF6B6B",
        "WARNING_COLOR":  "#FFC857",
        "BG_COLOR":       "#13151F",
        "CARD_COLOR":     "#1E2130",
        "TEXT_COLOR":     "#E8EAED",
        "MUTED_COLOR":    "#6B7280",
        "BORDER_COLOR":   "#2D3148",
        "HOVER_COLOR":    "#252840",
        "SELECTED_COLOR": "#2A2D4A",
    },
    "Emerald Pro": {
        "THEME_COLOR":    "#1B5E3B",
        "ACCENT_COLOR":   "#2D9E6B",
        "DANGER_COLOR":   "#D32F2F",
        "WARNING_COLOR":  "#F57C00",
        "BG_COLOR":       "#F0F7F3",
        "CARD_COLOR":     "#FFFFFF",
        "TEXT_COLOR":     "#1A2E22",
        "MUTED_COLOR":    "#6B8C76",
        "BORDER_COLOR":   "#C8DDD0",
        "HOVER_COLOR":    "#E5F2EB",
        "SELECTED_COLOR": "#D4EDE0",
    },
}


def _patch_module_colors(module, palette: Dict[str, str]) -> None:
    for key, value in palette.items():
        if hasattr(module, key):
            setattr(module, key, value)


def apply_theme(theme_name: str) -> Dict[str, str]:
    palette = THEMES.get(theme_name, THEMES["Ocean Mint"])

    import config
    _patch_module_colors(config, palette)

    module_names = [
        "main",
        "__main__",
        "screens.login_screen",
        "screens.register_screen",
        "screens.attendance_screen",
        "screens.records_screen",
        "screens.employees_screen",
        "ui_dialogs",
    ]

    for name in module_names:
        mod = sys.modules.get(name)
        if mod:
            _patch_module_colors(mod, palette)

    return palette

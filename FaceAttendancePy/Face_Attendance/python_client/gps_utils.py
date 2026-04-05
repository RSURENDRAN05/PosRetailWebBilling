# ============================================================
# GPS / Location Utilities for Windows (Python)
# Uses Windows Location API via COM / geocoder fallback
# ============================================================

import math
import threading
import time

try:
    import geocoder
    _GEOCODER_AVAILABLE = True
except ImportError:
    _GEOCODER_AVAILABLE = False

try:
    import win32com.client
    _WIN32_AVAILABLE = True
except ImportError:
    _WIN32_AVAILABLE = False

from config import (
    GPS_ENABLED, GPS_MAX_ACCURACY_M,
    GPS_GEOFENCE_ENABLED, GPS_OFFICE_LAT, GPS_OFFICE_LNG, GPS_GEOFENCE_RADIUS
)


def haversine_distance(lat1, lng1, lat2, lng2) -> float:
    """Return distance in metres between two GPS coordinates."""
    R  = 6371000  # Earth radius in metres
    φ1 = math.radians(lat1); φ2 = math.radians(lat2)
    dφ = math.radians(lat2 - lat1)
    dλ = math.radians(lng2 - lng1)
    a  = math.sin(dφ/2)**2 + math.cos(φ1)*math.cos(φ2)*math.sin(dλ/2)**2
    return R * 2 * math.atan2(math.sqrt(a), math.sqrt(1-a))


def get_location(timeout: float = 8.0) -> dict:
    """
    Try to get current GPS/location.
    Returns: { 'lat': float, 'lng': float, 'accuracy': float, 'method': str }
    or None on failure.
    """
    if not GPS_ENABLED:
        return None

    result = [None]
    exc    = [None]

    def _fetch():
        try:
            # Method 1: geocoder IP-based (always works, less accurate)
            if _GEOCODER_AVAILABLE:
                g = geocoder.ip('me')
                if g.ok:
                    result[0] = {
                        'lat':      g.lat,
                        'lng':      g.lng,
                        'accuracy': 2000,   # IP-based = low accuracy
                        'method':   'ip',
                        'city':     g.city or '',
                        'country':  g.country or '',
                    }
                    return
            # Method 2: fallback placeholder
            result[0] = None
        except Exception as e:
            exc[0] = e

    t = threading.Thread(target=_fetch, daemon=True)
    t.start()
    t.join(timeout)

    return result[0]


def check_geofence(lat: float, lng: float) -> tuple[bool, float]:
    """
    Returns (is_inside, distance_metres).
    """
    if not GPS_GEOFENCE_ENABLED:
        return True, 0.0
    dist = haversine_distance(lat, lng, GPS_OFFICE_LAT, GPS_OFFICE_LNG)
    return dist <= GPS_GEOFENCE_RADIUS, dist


def format_location(loc: dict) -> str:
    if not loc:
        return "Location unavailable"
    city = loc.get('city', '')
    return f"{'📍 ' + city + ' ' if city else ''}({loc['lat']:.4f}, {loc['lng']:.4f})"

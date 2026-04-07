# -*- mode: python ; coding: utf-8 -*-
from PyInstaller.utils.hooks import collect_data_files, collect_dynamic_libs
import os, sys

datas    = []
binaries = []

datas    += collect_data_files('face_recognition_models')
datas    += collect_data_files('cv2')
datas    += collect_data_files('dlib')

binaries += collect_dynamic_libs('dlib')
binaries += collect_dynamic_libs('cv2')

# Pull in every .dll sitting next to the dlib pyd so nothing is missed
import dlib as _dlib_mod
_dlib_dir = os.path.dirname(_dlib_mod.__file__)
for _f in os.listdir(_dlib_dir):
    if _f.lower().endswith('.dll'):
        binaries.append((os.path.join(_dlib_dir, _f), '.'))

# Some target PCs miss OpenMP runtime required by dlib.
for _vcomp in (r'C:\Windows\System32\vcomp140.dll', r'C:\Windows\SysWOW64\vcomp140.dll'):
    if os.path.exists(_vcomp):
        binaries.append((_vcomp, '.'))
        break

a = Analysis(
    ['main.py'],
    pathex=[],
    binaries=binaries,
    datas=datas,
    hiddenimports=[
        'cv2',
        'dlib',
        'face_recognition',
        'face_recognition_models',
        'PIL._tkinter_finder',
        'PIL._imagingtk',
        'numpy',
        'numpy.core._multiarray_umath',
    ],
    hookspath=[],
    hooksconfig={},
    runtime_hooks=[],
    excludes=[],
    noarchive=False,
    optimize=0,
)
pyz = PYZ(a.pure)

exe = EXE(
    pyz,
    a.scripts,
    [],
    exclude_binaries=True,
    name='FaceAttendanceApp',
    debug=False,
    bootloader_ignore_signals=False,
    strip=False,
    upx=False,           # disabled — UPX can corrupt dlib/cv2 DLLs
    console=False,
    disable_windowed_traceback=False,
    argv_emulation=False,
    target_arch=None,
    codesign_identity=None,
    entitlements_file=None,
    icon='icon.ico' if os.path.exists('icon.ico') else None,
)

coll = COLLECT(
    exe,
    a.binaries,
    a.datas,
    strip=False,
    upx=False,           # disabled — UPX can corrupt dlib/cv2 DLLs
    upx_exclude=[],
    name='FaceAttendanceApp',
)

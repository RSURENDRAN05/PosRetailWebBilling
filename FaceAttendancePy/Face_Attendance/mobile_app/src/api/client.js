// ============================================================
// API Client for React Native
// ============================================================
import AsyncStorage from '@react-native-async-storage/async-storage';

// UPDATE THIS to your server URL
export const API_BASE = 'https://yourdomain.com/face_attendance/api';
// Python recognition service (runs on local Windows PC or server)
export const RECOG_SERVICE = 'http://YOUR_WINDOWS_PC_IP:5001';

const FACE_TOLERANCE = 0.45;

async function getToken() {
  return await AsyncStorage.getItem('fa_token') || '';
}

export async function apiCall(path, method = 'GET', body = null, params = null) {
  const token = await getToken();
  let url = API_BASE + path;
  if (params) url += '?' + new URLSearchParams(params).toString();

  const opts = {
    method,
    headers: { 'Content-Type': 'application/json' },
  };
  if (token) opts.headers['Authorization'] = 'Bearer ' + token;
  if (body)  opts.body = JSON.stringify(body);

  try {
    const r = await fetch(url, opts);
    return await r.json();
  } catch (e) {
    return { success: false, message: e.message };
  }
}

// ---- Auth -------------------------------------------------
export async function adminLogin(username, password) {
  return apiCall('/auth.php', 'POST', { action: 'admin_login', username, password });
}

export async function pinLogin(loc_id, pin) {
  return apiCall('/auth.php', 'POST', { action: 'pin_login', loc_id, pin });
}

export async function saveAuth(token, ctx) {
  await AsyncStorage.setItem('fa_token', token);
  await AsyncStorage.setItem('fa_ctx',   JSON.stringify(ctx));
}

export async function getAuthCtx() {
  const raw = await AsyncStorage.getItem('fa_ctx');
  return raw ? JSON.parse(raw) : {};
}

export async function logout() {
  await AsyncStorage.removeItem('fa_token');
  await AsyncStorage.removeItem('fa_ctx');
}

// ---- Stats -----------------------------------------------
export async function getStats(ctx) {
  const params = {};
  if (ctx.com_id)    params.com_id    = ctx.com_id;
  if (ctx.branch_id) params.branch_id = ctx.branch_id;
  if (ctx.loc_id)    params.loc_id    = ctx.loc_id;
  return apiCall('/stats.php', 'GET', null, params);
}

// ---- Employees -------------------------------------------
export async function getEmployees(ctx, search = '') {
  const params = { status: 'active' };
  if (ctx.com_id)    params.com_id    = ctx.com_id;
  if (ctx.branch_id) params.branch_id = ctx.branch_id;
  if (ctx.loc_id)    params.loc_id    = ctx.loc_id;
  if (search)        params.search    = search;
  return apiCall('/employees.php', 'GET', null, params);
}

export async function createEmployee(data) {
  return apiCall('/employees.php', 'POST', data);
}

// ---- Face encodings ---------------------------------------
export async function getFaceEncodings(ctx) {
  const params = {};
  if (ctx.com_id)    params.com_id    = ctx.com_id;
  if (ctx.branch_id) params.branch_id = ctx.branch_id;
  if (ctx.loc_id)    params.loc_id    = ctx.loc_id;
  return apiCall('/faces.php', 'GET', null, params);
}

export async function saveFaceEncodings(emp_id, com_id, encodings) {
  return apiCall('/faces.php', 'POST', {
    emp_id,
    com_id,
    encodings,
    replace:   true,
    face_name: 'face',   // mirrors finger_name in employee_fingerprints
    facetype:  'full',   // mirrors fingertype in employee_fingerprints
  });
}

// ---- Attendance ------------------------------------------
export async function checkIn(emp_id, ctx = {}) {
  return apiCall('/attendance.php', 'POST', {
    action:        'checkin',
    emp_id,
    com_id:        ctx.com_id,
    loc_id:        ctx.loc_id,
    source:        ctx.source || 'mobile',
    method:        ctx.method || 'mobile_face',
    liveness_pass: ctx.liveness_pass,
    latitude:      ctx.latitude,
    longitude:     ctx.longitude,
    gps_accuracy:  ctx.gps_accuracy ?? ctx.accuracy,
  });
}

export async function getAttendance(ctx, from, to) {
  const params = { all: '1' };
  if (ctx.com_id)    params.com_id    = ctx.com_id;
  if (ctx.branch_id) params.branch_id = ctx.branch_id;
  if (ctx.loc_id)    params.loc_id    = ctx.loc_id;
  if (from) params.from = from;
  if (to)   params.to   = to;
  return apiCall('/attendance.php', 'GET', null, params);
}

// ---- Branches / Locations --------------------------------
export async function getBranches(com_id) {
  return apiCall('/branches.php', 'GET', null, { com_id });
}

export async function getLocations(branch_id) {
  return apiCall('/locations.php', 'GET', null, { branch: branch_id });
}

// ---- Recognition service (Python Flask) -----------------
export async function recognizeFace(base64Image) {
  try {
    const r = await fetch(RECOG_SERVICE + '/recognize', {
      method: 'POST',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify({ image: base64Image }),
      signal: AbortSignal.timeout(5000),
    });
    return await r.json();
  } catch (e) {
    return { success: false, message: e.message };
  }
}

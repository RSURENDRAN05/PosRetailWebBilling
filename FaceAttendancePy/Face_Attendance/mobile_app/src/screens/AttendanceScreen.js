import React, { useState, useRef, useEffect, useCallback } from 'react';
import {
  View, Text, StyleSheet, TouchableOpacity, FlatList,
  Alert, ActivityIndicator, Platform,
} from 'react-native';
import { CameraView, useCameraPermissions } from 'expo-camera';
import * as LocalAuthentication from 'expo-local-authentication';
import * as Location from 'expo-location';
import { recognizeFace, checkIn, getAttendance } from '../api/client';
import { useAuth } from '../context/AuthContext';
import { C } from '../theme';

const CAPTURE_INTERVAL = 2500;

export default function AttendanceScreen() {
  const { authCtx } = useAuth();
  const [camPermission,  requestCamPermission]  = useCameraPermissions();
  const [running,     setRunning]     = useState(false);
  const [status,      setStatus]      = useState('Press Start to scan');
  const [log,         setLog]         = useState([]);
  const [cooldowns,   setCooldowns]   = useState({});
  const [location,    setLocation]    = useState(null);
  const [fpAvailable, setFpAvailable] = useState(false);
  const cameraRef   = useRef(null);
  const intervalRef = useRef(null);

  useEffect(() => {
    loadTodayLog();
    checkBiometrics();
    requestLocationPermission();
    return () => stopScan();
  }, []);

  // ---- Permissions ----------------------------------------
  async function checkBiometrics() {
    const has = await LocalAuthentication.hasHardwareAsync();
    const enrolled = await LocalAuthentication.isEnrolledAsync();
    setFpAvailable(has && enrolled);
  }

  async function requestLocationPermission() {
    const { status } = await Location.requestForegroundPermissionsAsync();
    if (status === 'granted') {
      try {
        const loc = await Location.getCurrentPositionAsync({
          accuracy: Location.Accuracy.Balanced, timeout: 8000,
        });
        setLocation({ lat: loc.coords.latitude, lng: loc.coords.longitude, accuracy: loc.coords.accuracy });
      } catch {}
    }
  }

  // ---- Fingerprint auth (for manual check-in override) ----
  async function doFingerprintCheckin(empId, empName) {
    if (!fpAvailable) { Alert.alert('Fingerprint not available'); return; }
    const result = await LocalAuthentication.authenticateAsync({
      promptMessage: `Verify fingerprint for ${empName}`,
      fallbackLabel: 'Use PIN',
    });
    if (result.success) {
      await markAttendance(empId, empName, 'fingerprint');
    } else {
      Alert.alert('Authentication failed');
    }
  }

  // ---- Camera scan ----------------------------------------
  async function startScan() {
    if (!camPermission?.granted) {
      const res = await requestCamPermission();
      if (!res.granted) { Alert.alert('Camera permission required for attendance'); return; }
    }
    // Refresh location
    requestLocationPermission();
    setRunning(true);
    setStatus('🔍 Liveness check — blink twice then turn head');
    intervalRef.current = setInterval(captureAndRecognize, CAPTURE_INTERVAL);
  }

  function stopScan() {
    setRunning(false);
    setStatus('Stopped');
    if (intervalRef.current) clearInterval(intervalRef.current);
  }

  async function captureAndRecognize() {
    if (!cameraRef.current) return;
    try {
      const photo = await cameraRef.current.takePictureAsync({
        base64: true, quality: 0.5, skipProcessing: true,
      });
      const r = await recognizeFace(photo.base64);
      if (r.success && r.emp_id) {
        setStatus(`✅ Detected: ${r.employee_name} (${r.confidence?.toFixed(1)}%)`);
        await markAttendance(r.emp_id, r.employee_name, 'face');
      } else {
        setStatus('🔍 Scanning — blink twice & turn head slightly');
      }
    } catch (e) {
      setStatus('⚠ ' + e.message);
    }
  }

  async function markAttendance(empId, empName, method = 'face') {
    const now  = Date.now();
    const last = cooldowns[empId] || 0;
    if (now - last < 8000) return;
    setCooldowns(prev => ({ ...prev, [empId]: now }));

    const body = {
      emp_id: empId,
      source: 'mobile',
      method,
    };
    // Attach GPS
    if (location) {
      body.latitude  = location.lat;
      body.longitude = location.lng;
      body.gps_accuracy = location.accuracy;
    }

    const r = await checkIn(empId, { ...authCtx, ...body });
    const time = new Date().toLocaleTimeString();
    const entry = {
      id:            Date.now().toString(),
      emp_id:        empId,
      employee_name: empName,
      check_in:      time,
      status:        r.data?.status || 'present',
      method,
      location_str:  location ? `${location.lat.toFixed(4)},${location.lng.toFixed(4)}` : '',
    };
    setLog(prev => [entry, ...prev.slice(0, 49)]);
    loadTodayLog();
  }

  async function loadTodayLog() {
    const today = new Date().toISOString().slice(0, 10);
    const r = await getAttendance(authCtx, today, today);
    if (r.success) setLog(r.data.records.slice(0, 20));
  }

  const renderItem = ({ item }) => (
    <View style={s.logRow}>
      <Text style={s.logTime}>{typeof item.check_in==='string'&&item.check_in.length>8?item.check_in.slice(-8):item.check_in}</Text>
      <View style={{flex:1}}>
        <Text style={s.logName}>{item.employee_name||item.emp_id}</Text>
        {item.location_str ? <Text style={s.logMeta}>📍 {item.location_str}</Text> : null}
        {item.method       ? <Text style={s.logMeta}>{item.method==='fingerprint'?'🖐 Fingerprint':'📷 Face'}</Text> : null}
      </View>
      <View style={[s.badge,{backgroundColor:item.status==='late'?'#FEF9E7':'#D5F5E3'}]}>
        <Text style={[s.badgeTxt,{color:item.status==='late'?C.warn:C.accent}]}>{item.status||'present'}</Text>
      </View>
    </View>
  );

  return (
    <View style={s.bg}>
      {/* Camera */}
      <View style={s.cameraBox}>
        {running && camPermission?.granted ? (
          <CameraView ref={cameraRef} style={s.camera} facing="front"/>
        ) : (
          <View style={[s.camera,s.camPlaceholder]}>
            <Text style={{fontSize:40}}>📷</Text>
            <Text style={{color:'#aaa',marginTop:8}}>Camera inactive</Text>
          </View>
        )}
        {running && <View style={s.faceGuide} pointerEvents="none"/>}
        {/* GPS indicator */}
        {location && (
          <View style={s.gpsTag}>
            <Text style={s.gpsTxt}>📍 {location.lat.toFixed(4)}, {location.lng.toFixed(4)}</Text>
          </View>
        )}
      </View>

      {/* Status */}
      <View style={s.statusBox}>
        <Text style={s.statusTxt}>{status}</Text>
      </View>

      {/* Controls */}
      <View style={s.controls}>
        <TouchableOpacity style={[s.ctrlBtn,running?s.stopBtn:s.startBtn]} onPress={running?stopScan:startScan}>
          <Text style={s.ctrlTxt}>{running?'⏹ Stop':'▶ Start Face Scan'}</Text>
        </TouchableOpacity>
        {fpAvailable && (
          <TouchableOpacity style={[s.ctrlBtn,{backgroundColor:C.purple,flex:0,paddingHorizontal:16}]}
            onPress={()=>Alert.alert('Fingerprint','Enter Employee ID for fingerprint check-in\n(Use for manual override)')}>
            <Text style={s.ctrlTxt}>🖐</Text>
          </TouchableOpacity>
        )}
      </View>

      {fpAvailable && (
        <Text style={{textAlign:'center',fontSize:11,color:C.muted,marginBottom:4}}>
          🖐 Fingerprint available for manual check-in
        </Text>
      )}

      {/* Log */}
      <View style={s.logHeader}>
        <Text style={s.logTitle}>Today's Log</Text>
        <Text style={s.logCount}>{log.length} record(s)</Text>
      </View>
      <FlatList
        data={log} keyExtractor={(item,i)=>item.id||i.toString()}
        style={s.list} renderItem={renderItem}
        ListEmptyComponent={<Text style={s.empty}>No attendance today</Text>}
      />
    </View>
  );
}

const s = StyleSheet.create({
  bg:            { flex:1, backgroundColor:C.bg },
  cameraBox:     { height:240, position:'relative', backgroundColor:'#000' },
  camera:        { flex:1 },
  camPlaceholder:{ justifyContent:'center', alignItems:'center', backgroundColor:'#1a1a2e' },
  faceGuide:     { position:'absolute', top:'10%', left:'25%', right:'25%', bottom:'10%',
                   borderWidth:2, borderColor:'rgba(255,255,255,.5)', borderRadius:8 },
  gpsTag:        { position:'absolute', bottom:6, left:6, backgroundColor:'rgba(0,0,0,.6)',
                   padding:4, borderRadius:6 },
  gpsTxt:        { color:'#fff', fontSize:10 },
  statusBox:     { backgroundColor:C.primary, padding:10, alignItems:'center' },
  statusTxt:     { color:'#fff', fontWeight:'600', fontSize:14 },
  controls:      { padding:10, flexDirection:'row', gap:8 },
  ctrlBtn:       { flex:1, padding:13, borderRadius:10, alignItems:'center' },
  startBtn:      { backgroundColor:C.accent },
  stopBtn:       { backgroundColor:C.danger },
  ctrlTxt:       { color:'#fff', fontWeight:'700', fontSize:14 },
  logHeader:     { flexDirection:'row', justifyContent:'space-between', paddingHorizontal:16, paddingVertical:6 },
  logTitle:      { fontWeight:'700', color:C.text },
  logCount:      { color:C.muted, fontSize:12 },
  list:          { flex:1, paddingHorizontal:10 },
  logRow:        { backgroundColor:C.card, borderRadius:10, padding:12, marginBottom:6,
                   flexDirection:'row', alignItems:'center' },
  logTime:       { fontSize:11, color:C.muted, width:60 },
  logName:       { fontSize:13, fontWeight:'600', color:C.text },
  logMeta:       { fontSize:10, color:C.muted, marginTop:1 },
  badge:         { paddingHorizontal:8, paddingVertical:3, borderRadius:10 },
  badgeTxt:      { fontSize:11, fontWeight:'600' },
  empty:         { textAlign:'center', color:C.muted, padding:24 },
});

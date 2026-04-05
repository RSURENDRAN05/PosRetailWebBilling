import React, { useState, useCallback } from 'react';
import {
  View, Text, StyleSheet, ScrollView, TouchableOpacity,
  RefreshControl, ActivityIndicator,
} from 'react-native';
import { useFocusEffect } from '@react-navigation/native';
import { getStats } from '../api/client';
import { useAuth } from '../context/AuthContext';
import { C } from '../theme';

export default function DashboardScreen() {
  const { authCtx, logout } = useAuth();
  const [stats,     setStats]     = useState(null);
  const [recent,    setRecent]    = useState([]);
  const [loading,   setLoading]   = useState(true);
  const [refreshing,setRefreshing]= useState(false);

  const load = useCallback(async () => {
    const r = await getStats(authCtx);
    if (r.success) { setStats(r.data.today); setRecent(r.data.recent || []); }
    setLoading(false); setRefreshing(false);
  }, [authCtx]);

  useFocusEffect(useCallback(() => { setLoading(true); load(); }, [load]));

  if (loading) return <View style={s.center}><ActivityIndicator size="large" color={C.accent}/></View>;

  const ctx = authCtx.full_name || authCtx.company_name || '';
  const sub = authCtx.loc_name  || authCtx.branch_name  || '';

  return (
    <ScrollView style={s.bg} refreshControl={<RefreshControl refreshing={refreshing} onRefresh={()=>{setRefreshing(true);load()}} colors={[C.accent]}/>}>
      {/* Header */}
      <View style={s.header}>
        <View style={{flex:1}}>
          <Text style={s.greeting}>👋 Hello,</Text>
          <Text style={s.name}>{ctx}</Text>
          {sub ? <Text style={s.sub}>{sub}</Text> : null}
        </View>
        <TouchableOpacity onPress={logout} style={s.logoutBtn}>
          <Text style={s.logoutTxt}>Logout</Text>
        </TouchableOpacity>
      </View>

      {/* Stats grid */}
      <View style={s.grid}>
        {[
          { val: stats?.total_employees, lbl: 'Total Staff', color: C.blue },
          { val: stats?.present, lbl: 'Present',        color: C.accent },
          { val: stats?.absent,  lbl: 'Absent',         color: C.danger },
          { val: stats?.late,    lbl: 'Late',           color: C.warn   },
        ].map(({ val, lbl, color }) => (
          <View key={lbl} style={s.statCard}>
            <View style={s.statInner}>
              <Text style={[s.statVal, { color }]}>{val ?? '—'}</Text>
              <Text style={s.statLbl}>{lbl}</Text>
            </View>
          </View>
        ))}
      </View>

      {/* Recent */}
      <View style={s.card}>
        <Text style={s.sectionTitle}>📋 Recent Activity</Text>
        {recent.length === 0
          ? <Text style={s.empty}>No attendance today</Text>
          : recent.map((r, i) => (
            <View key={i} style={s.logRow}>
              <Text style={s.logTime}>{(r.check_in||'').slice(-8)||'—'}</Text>
              <View style={{flex:1}}>
                <Text style={s.logName}>{r.employee_name}</Text>
                <Text style={s.logMeta}>{r.department||''} • {r.source||''}</Text>
              </View>
              <View style={[s.badge, {backgroundColor: r.status==='late'?'#FEF9E7':'#D5F5E3'}]}>
                <Text style={[s.badgeTxt,{color:r.status==='late'?C.warn:C.accent}]}>{r.status}</Text>
              </View>
            </View>
          ))
        }
      </View>
    </ScrollView>
  );
}

const s = StyleSheet.create({
  bg:           { flex:1, backgroundColor:C.bg },
  center:       { flex:1, justifyContent:'center', alignItems:'center' },
  header:       { backgroundColor:C.primary, padding:20, paddingTop:50, flexDirection:'row', alignItems:'flex-start' },
  greeting:     { color:'rgba(255,255,255,.7)', fontSize:13 },
  name:         { color:'#fff', fontSize:18, fontWeight:'700', marginTop:2 },
  sub:          { color:'rgba(255,255,255,.6)', fontSize:12, marginTop:2 },
  logoutBtn:    { backgroundColor:'rgba(255,255,255,.15)', padding:8, borderRadius:8 },
  logoutTxt:    { color:'#fff', fontSize:13 },
  grid:         { flexDirection:'row', flexWrap:'wrap', padding:12, gap:0 },
  statCard:     { width:'50%', padding:6 },
  statInner:    { backgroundColor:C.card, borderRadius:12, padding:16, alignItems:'center',
                  shadowColor:'#000',shadowOpacity:.06,shadowRadius:6,elevation:2 },
  statVal:      { fontSize:32, fontWeight:'700' },
  statLbl:      { fontSize:11, color:C.muted, marginTop:2, textTransform:'uppercase', letterSpacing:.5 },
  card:         { backgroundColor:C.card, margin:12, borderRadius:12, padding:16,
                  shadowColor:'#000',shadowOpacity:.06,shadowRadius:6,elevation:2 },
  sectionTitle: { fontSize:15, fontWeight:'700', color:C.text, marginBottom:12 },
  empty:        { color:C.muted, textAlign:'center', padding:16 },
  logRow:       { flexDirection:'row', alignItems:'center', paddingVertical:10, borderBottomWidth:1, borderBottomColor:'#f5f5f5' },
  logTime:      { fontSize:12, color:C.muted, width:60 },
  logName:      { fontSize:14, fontWeight:'600', color:C.text },
  logMeta:      { fontSize:11, color:C.muted, marginTop:2 },
  badge:        { paddingHorizontal:8, paddingVertical:3, borderRadius:10 },
  badgeTxt:     { fontSize:11, fontWeight:'600' },
});

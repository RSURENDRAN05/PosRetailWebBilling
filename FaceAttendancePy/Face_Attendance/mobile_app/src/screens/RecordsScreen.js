import React, { useState, useCallback } from 'react';
import {
  View, Text, StyleSheet, FlatList, TextInput,
  TouchableOpacity, ActivityIndicator, RefreshControl,
} from 'react-native';
import { useFocusEffect } from '@react-navigation/native';
import { getAttendance } from '../api/client';
import { useAuth } from '../context/AuthContext';
import { C } from '../theme';

export default function RecordsScreen() {
  const { authCtx } = useAuth();
  const today = new Date().toISOString().slice(0, 10);
  const [from,       setFrom]       = useState(today);
  const [to,         setTo]         = useState(today);
  const [records,    setRecords]    = useState([]);
  const [summary,    setSummary]    = useState({});
  const [loading,    setLoading]    = useState(true);
  const [refreshing, setRefreshing] = useState(false);

  const load = useCallback(async () => {
    const r = await getAttendance(authCtx, from, to);
    if (r.success) { setRecords(r.data.records); setSummary(r.data.summary); }
    setLoading(false); setRefreshing(false);
  }, [authCtx, from, to]);

  useFocusEffect(useCallback(() => { setLoading(true); load(); }, [load]));

  function setPreset(days) {
    const end = new Date();
    const start = new Date();
    start.setDate(end.getDate() - days);
    setFrom(start.toISOString().slice(0, 10));
    setTo(end.toISOString().slice(0, 10));
  }

  const renderItem = ({ item }) => {
    const ci = (item.check_in  || '').slice(-8) || '—';
    const co = (item.check_out || '').slice(-8) || '—';
    const recordDate = item.att_date || item.date || '—';
    return (
      <View style={s.row}>
        <View style={s.rowLeft}>
          <Text style={s.rowName}>{item.employee_name || item.emp_id}</Text>
          <Text style={s.rowMeta}>{recordDate}  •  In: {ci}  Out: {co}</Text>
          <Text style={s.rowMeta}>{item.emp_id}</Text>
        </View>
        <View style={[s.badge, { backgroundColor: item.status==='late'?'#FEF9E7':'#D5F5E3' }]}>
          <Text style={[s.badgeTxt,{color:item.status==='late'?C.warn:C.accent}]}>{item.status}</Text>
        </View>
      </View>
    );
  };

  return (
    <View style={s.bg}>
      {/* Filters */}
      <View style={s.filters}>
        <View style={s.dateRow}>
          <TextInput style={s.dateInput} value={from} onChangeText={setFrom} placeholder="From YYYY-MM-DD"/>
          <Text style={{color:C.muted,paddingHorizontal:4}}>→</Text>
          <TextInput style={s.dateInput} value={to}   onChangeText={setTo}   placeholder="To YYYY-MM-DD"/>
          <TouchableOpacity style={s.searchBtn} onPress={load}>
            <Text style={{color:'#fff',fontWeight:'700'}}>🔍</Text>
          </TouchableOpacity>
        </View>
        <View style={s.presets}>
          {[['Today',0],['Week',7],['Month',30]].map(([lbl,d])=>(
            <TouchableOpacity key={lbl} style={s.preset} onPress={()=>{setPreset(d);setTimeout(load,100)}}>
              <Text style={s.presetTxt}>{lbl}</Text>
            </TouchableOpacity>
          ))}
        </View>
      </View>

      {/* Summary */}
      {summary.total > 0 && (
        <View style={s.summary}>
          <Text style={s.sumTxt}>Total: <Text style={{fontWeight:'700'}}>{summary.total}</Text></Text>
          <Text style={[s.sumTxt,{color:C.accent}]}>Present: {summary.present}</Text>
          <Text style={[s.sumTxt,{color:C.warn}]}>Late: {summary.late}</Text>
        </View>
      )}

      {loading
        ? <ActivityIndicator style={{marginTop:40}} size="large" color={C.accent}/>
        : <FlatList
            data={records}
            keyExtractor={(item,i)=>i.toString()}
            renderItem={renderItem}
            contentContainerStyle={{padding:12}}
            refreshControl={<RefreshControl refreshing={refreshing} onRefresh={()=>{setRefreshing(true);load()}} colors={[C.accent]}/>}
            ListEmptyComponent={<Text style={s.empty}>No records found</Text>}
          />
      }
    </View>
  );
}

const s = StyleSheet.create({
  bg:       { flex:1, backgroundColor:C.bg },
  filters:  { backgroundColor:C.card, padding:12, shadowColor:'#000',shadowOpacity:.06,shadowRadius:6,elevation:2 },
  dateRow:  { flexDirection:'row', alignItems:'center', marginBottom:8 },
  dateInput:{ flex:1, borderWidth:1, borderColor:'#ddd', borderRadius:8, padding:8, fontSize:12, color:C.text },
  searchBtn:{ backgroundColor:C.primary, padding:10, borderRadius:8, marginLeft:6 },
  presets:  { flexDirection:'row', gap:8 },
  preset:   { backgroundColor:C.bg, paddingHorizontal:14, paddingVertical:6, borderRadius:16 },
  presetTxt:{ fontSize:12, color:C.text, fontWeight:'600' },
  summary:  { flexDirection:'row', justifyContent:'space-around', padding:10, backgroundColor:C.card,
              borderBottomWidth:1, borderBottomColor:'#eee' },
  sumTxt:   { fontSize:13, color:C.muted },
  row:      { backgroundColor:C.card, borderRadius:10, padding:14, marginBottom:8,
              flexDirection:'row', alignItems:'center', shadowColor:'#000',shadowOpacity:.04,shadowRadius:4,elevation:1 },
  rowLeft:  { flex:1 },
  rowName:  { fontSize:14, fontWeight:'600', color:C.text },
  rowMeta:  { fontSize:11, color:C.muted, marginTop:2 },
  badge:    { paddingHorizontal:10, paddingVertical:4, borderRadius:12 },
  badgeTxt: { fontSize:12, fontWeight:'600' },
  empty:    { textAlign:'center', color:C.muted, padding:32, fontSize:14 },
});

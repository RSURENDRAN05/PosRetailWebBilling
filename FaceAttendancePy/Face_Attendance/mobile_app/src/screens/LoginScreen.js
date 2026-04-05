import React, { useState } from 'react';
import {
  View, Text, TextInput, TouchableOpacity, StyleSheet,
  ScrollView, ActivityIndicator, Alert, KeyboardAvoidingView, Platform,
} from 'react-native';
import { adminLogin, pinLogin, saveAuth } from '../api/client';
import { useAuth } from '../context/AuthContext';
import { C } from '../theme';

export default function LoginScreen() {
  const { login } = useAuth();
  const [tab, setTab]       = useState('admin');  // 'admin' | 'device'
  const [loading, setLoading] = useState(false);

  // Admin fields
  const [username, setUsername] = useState('');
  const [password, setPassword] = useState('');

  // Device PIN fields
  const [locId, setLocId] = useState('');
  const [pin,   setPin]   = useState('');

  async function handleAdminLogin() {
    if (!username || !password) { Alert.alert('Error', 'Enter username and password'); return; }
    setLoading(true);
    const r = await adminLogin(username, password);
    setLoading(false);
    if (r.success) {
      await login(r.data.token, r.data.user);
    } else {
      Alert.alert('Login Failed', r.message);
    }
  }

  async function handlePinLogin() {
    if (!locId || !pin) { Alert.alert('Error', 'Enter Location ID and PIN'); return; }
    setLoading(true);
    const r = await pinLogin(locId, pin);
    setLoading(false);
    if (r.success) {
      await login(r.data.token, r.data.location);
    } else {
      Alert.alert('Login Failed', r.message);
    }
  }

  return (
    <KeyboardAvoidingView style={s.bg} behavior={Platform.OS === 'ios' ? 'padding' : undefined}>
      <ScrollView contentContainerStyle={s.container} keyboardShouldPersistTaps="handled">
        <View style={s.logoBox}>
          <Text style={s.logoEmoji}>🎯</Text>
          <Text style={s.title}>Face Attendance</Text>
          <Text style={s.subtitle}>Sign in to continue</Text>
        </View>

        <View style={s.card}>
          {/* Tab switcher */}
          <View style={s.tabs}>
            <TouchableOpacity style={[s.tab, tab==='admin' && s.tabActive]} onPress={()=>setTab('admin')}>
              <Text style={[s.tabTxt, tab==='admin' && s.tabTxtActive]}>Admin Login</Text>
            </TouchableOpacity>
            <TouchableOpacity style={[s.tab, tab==='device' && s.tabActive]} onPress={()=>setTab('device')}>
              <Text style={[s.tabTxt, tab==='device' && s.tabTxtActive]}>Device PIN</Text>
            </TouchableOpacity>
          </View>

          {tab === 'admin' ? (
            <>
              <Text style={s.label}>USERNAME</Text>
              <TextInput style={s.input} value={username} onChangeText={setUsername}
                placeholder="admin" autoCapitalize="none" placeholderTextColor={C.muted}/>

              <Text style={s.label}>PASSWORD</Text>
              <TextInput style={s.input} value={password} onChangeText={setPassword}
                placeholder="••••••••" secureTextEntry placeholderTextColor={C.muted}/>

              <TouchableOpacity style={s.btn} onPress={handleAdminLogin} disabled={loading}>
                {loading ? <ActivityIndicator color="#fff"/>
                         : <Text style={s.btnTxt}>Sign In</Text>}
              </TouchableOpacity>
            </>
          ) : (
            <>
              <Text style={s.label}>LOCATION ID</Text>
              <TextInput style={s.input} value={locId} onChangeText={setLocId}
                placeholder="e.g. LOC001" autoCapitalize="characters" placeholderTextColor={C.muted}/>

              <Text style={s.label}>DEVICE PIN</Text>
              <TextInput style={s.input} value={pin} onChangeText={setPin}
                placeholder="1234" secureTextEntry keyboardType="numeric" placeholderTextColor={C.muted}/>

              <TouchableOpacity style={[s.btn,{backgroundColor:C.blue}]} onPress={handlePinLogin} disabled={loading}>
                {loading ? <ActivityIndicator color="#fff"/>
                         : <Text style={s.btnTxt}>🔓 Unlock Device</Text>}
              </TouchableOpacity>
            </>
          )}
        </View>
      </ScrollView>
    </KeyboardAvoidingView>
  );
}

const s = StyleSheet.create({
  bg:        { flex:1, backgroundColor: C.primary },
  container: { flexGrow:1, alignItems:'center', justifyContent:'center', padding:24 },
  logoBox:   { alignItems:'center', marginBottom:28 },
  logoEmoji: { fontSize:52 },
  title:     { fontSize:24, fontWeight:'700', color:'#fff', marginTop:8 },
  subtitle:  { fontSize:14, color:'rgba(255,255,255,.7)', marginTop:4 },
  card:      { backgroundColor:'#fff', borderRadius:16, padding:24, width:'100%', maxWidth:400,
               shadowColor:'#000', shadowOpacity:.2, shadowRadius:16, elevation:8 },
  tabs:      { flexDirection:'row', backgroundColor:C.bg, borderRadius:8, marginBottom:20, overflow:'hidden' },
  tab:       { flex:1, padding:10, alignItems:'center' },
  tabActive: { backgroundColor:C.primary },
  tabTxt:    { fontWeight:'600', color:C.muted, fontSize:13 },
  tabTxtActive: { color:'#fff' },
  label:     { fontSize:11, color:C.muted, fontWeight:'700', letterSpacing:.5, marginBottom:4, marginTop:4 },
  input:     { borderWidth:1, borderColor:'#ddd', borderRadius:8, padding:12,
               fontSize:15, color:C.text, marginBottom:12 },
  btn:       { backgroundColor:C.accent, borderRadius:10, padding:14, alignItems:'center', marginTop:4 },
  btnTxt:    { color:'#fff', fontWeight:'700', fontSize:15 },
});

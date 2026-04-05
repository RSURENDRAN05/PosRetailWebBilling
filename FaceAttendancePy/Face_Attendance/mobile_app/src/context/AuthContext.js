import React, { createContext, useContext, useState, useEffect } from 'react';
import { getAuthCtx, logout as apiLogout } from '../api/client';
import AsyncStorage from '@react-native-async-storage/async-storage';

const AuthContext = createContext(null);

export function AuthProvider({ children }) {
  const [token,   setToken]   = useState(null);
  const [authCtx, setAuthCtx] = useState({});
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    (async () => {
      const t   = await AsyncStorage.getItem('fa_token');
      const ctx = await getAuthCtx();
      setToken(t);
      setAuthCtx(ctx);
      setLoading(false);
    })();
  }, []);

  const login = async (t, ctx) => {
    await AsyncStorage.setItem('fa_token', t);
    await AsyncStorage.setItem('fa_ctx',   JSON.stringify(ctx));
    setToken(t);
    setAuthCtx(ctx);
  };

  const logout = async () => {
    await apiLogout();
    setToken(null);
    setAuthCtx({});
  };

  return (
    <AuthContext.Provider value={{ token, authCtx, loading, login, logout }}>
      {children}
    </AuthContext.Provider>
  );
}

export const useAuth = () => useContext(AuthContext);

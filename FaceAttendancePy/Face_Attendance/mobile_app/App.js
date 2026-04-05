import React from 'react';
import { NavigationContainer } from '@react-navigation/native';
import { createNativeStackNavigator } from '@react-navigation/native-stack';
import { createBottomTabNavigator } from '@react-navigation/bottom-tabs';
import { Ionicons } from '@expo/vector-icons';
import { StatusBar } from 'expo-status-bar';
import { ActivityIndicator, View } from 'react-native';

import { AuthProvider, useAuth } from './src/context/AuthContext';
import LoginScreen     from './src/screens/LoginScreen';
import DashboardScreen from './src/screens/DashboardScreen';
import AttendanceScreen from './src/screens/AttendanceScreen';
import RecordsScreen   from './src/screens/RecordsScreen';
import { C } from './src/theme';

const Stack = createNativeStackNavigator();
const Tab   = createBottomTabNavigator();

function MainTabs() {
  return (
    <Tab.Navigator
      screenOptions={({ route }) => ({
        headerStyle:      { backgroundColor: C.primary },
        headerTintColor:  '#fff',
        headerTitleStyle: { fontWeight: '700' },
        tabBarActiveTintColor:   C.accent,
        tabBarInactiveTintColor: C.muted,
        tabBarStyle:      { backgroundColor: C.card, borderTopColor: '#eee' },
        tabBarIcon: ({ focused, color, size }) => {
          const icons = {
            Dashboard:  focused ? 'grid'       : 'grid-outline',
            Attendance: focused ? 'camera'     : 'camera-outline',
            Records:    focused ? 'list'       : 'list-outline',
          };
          return <Ionicons name={icons[route.name]} size={size} color={color}/>;
        },
      })}
    >
      <Tab.Screen name="Dashboard"  component={DashboardScreen}  options={{ title:'Dashboard' }}/>
      <Tab.Screen name="Attendance" component={AttendanceScreen} options={{ title:'Attendance' }}/>
      <Tab.Screen name="Records"    component={RecordsScreen}    options={{ title:'Records' }}/>
    </Tab.Navigator>
  );
}

function RootNavigator() {
  const { token, loading } = useAuth();

  if (loading) {
    return (
      <View style={{ flex:1, justifyContent:'center', alignItems:'center', backgroundColor:C.primary }}>
        <ActivityIndicator size="large" color="#fff"/>
      </View>
    );
  }

  return (
    <Stack.Navigator screenOptions={{ headerShown: false }}>
      {token ? (
        <Stack.Screen name="Main" component={MainTabs}/>
      ) : (
        <Stack.Screen name="Login" component={LoginScreen}/>
      )}
    </Stack.Navigator>
  );
}

export default function App() {
  return (
    <AuthProvider>
      <NavigationContainer>
        <StatusBar style="light"/>
        <RootNavigator/>
      </NavigationContainer>
    </AuthProvider>
  );
}

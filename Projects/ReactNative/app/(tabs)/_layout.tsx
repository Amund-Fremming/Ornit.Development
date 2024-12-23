import React, { useEffect, useState } from "react";
import FontAwesome from "@expo/vector-icons/FontAwesome";
import { Tabs } from "expo-router";
import { Colors } from "@/src/shared/constants/Colors";
import * as SecureStore from "expo-secure-store";
import Auth from "@/src/features/Auth/Auth";

function TabBarIcon(props: {
  name: React.ComponentProps<typeof FontAwesome>["name"];
  color: string;
}) {
  return <FontAwesome size={28} style={{ marginBottom: -3 }} {...props} />;
}

export default function TabLayout() {
  const [loggedIn, setLoggedIn] = useState<boolean>(false);

  /* Remove this comment for a login wall.
    - Still needs functionality for getting a token, and storing it on login
*/

  useEffect(() => {
    checkForToken();
  }, []);

  const checkForToken = async () => {
    const token = await SecureStore.getItemAsync("token");
    if (token == null) {
      setLoggedIn(false);
    }
  };

  if (!loggedIn) {
    return <Auth />;
  }

  return (
    <Tabs
      screenOptions={{
        tabBarActiveTintColor: Colors.Black,
        headerShown: false,
        tabBarInactiveTintColor: "gray",
        tabBarStyle: {
          backgroundColor: Colors.White,
        },
      }}
    >
      <Tabs.Screen
        name="index"
        options={{
          title: "Tab One",
          tabBarIcon: ({ color, focused }) => (
            // Use focused ? x : x-outline, for more dynamic style
            <TabBarIcon name="code" color={color} />
          ),
        }}
      />
      <Tabs.Screen
        name="two"
        options={{
          title: "Tab Two",
          tabBarIcon: ({ color, focused }) => (
            // Use focused ? x : x-outline, for more dynamic style
            <TabBarIcon name="code" color={color} />
          ),
        }}
      />
    </Tabs>
  );
}

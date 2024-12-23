import { View, Text, KeyboardAvoidingView, Platform } from "react-native";

import { styles } from "./authScreenStyles";
import { useState } from "react";
import Login from "./components/Login/Login";
import Register from "./components/Register/Register";

export default function AuthScreen() {
  const [isLoginView, setIsLoginView] = useState<boolean>(true);

  const toggleView = () => setIsLoginView(!isLoginView);

  return (
    <KeyboardAvoidingView
      behavior={Platform.OS === "ios" ? "padding" : "height"}
      style={styles.container}
    >
      <View style={styles.container}>
        <Text style={styles.header}>Auth</Text>
        {isLoginView && <Login toggleView={toggleView} />}
        {!isLoginView && <Register toggleView={toggleView} />}
      </View>
    </KeyboardAvoidingView>
  );
}

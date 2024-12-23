import { View, Text, Pressable, TextInput } from "react-native";

import { styles } from "./loginStyles";
import { Colors } from "@/src/shared/constants/Colors";
import { useState } from "react";

interface ILogin {
  toggleView: () => void;
}

export default function Login(props: ILogin) {
  const handleLogin = () => {
    setUsername("");
    setPassword("");
  };

  const [username, setUsername] = useState<string>("");
  const [password, setPassword] = useState<string>("");

  return (
    <View style={styles.container}>
      <Text style={styles.header}>Login</Text>
      <TextInput
        placeholder="Username"
        placeholderTextColor={Colors.Gray}
        style={styles.input}
        value={username}
        onChangeText={setUsername}
      />
      <TextInput
        placeholder="Password"
        placeholderTextColor={Colors.Gray}
        style={styles.input}
        value={password}
        onChangeText={setPassword}
      />
      <Pressable onPress={handleLogin}>
        <Text style={styles.login}>Login</Text>
      </Pressable>
      <Pressable onPress={props.toggleView}>
        <Text style={styles.registerLink}>register new user</Text>
      </Pressable>
    </View>
  );
}

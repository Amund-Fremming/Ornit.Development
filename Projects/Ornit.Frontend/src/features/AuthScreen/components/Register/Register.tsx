import { View, Text, Pressable, TextInput } from "react-native";

import { styles } from "./registerStyles";
import { Colors } from "@/src/shared/constants/Colors";
import { useState } from "react";

interface IRegister {
  toggleView: () => void;
}

export default function Register(props: IRegister) {
  const [username, setUsername] = useState<string>("");
  const [password, setPassword] = useState<string>("");
  const [retypedPassword, setRetypedPassword] = useState<string>("");

  const handleRegister = () => {
    setUsername("");
    setPassword("");
    setRetypedPassword("");
  };

  return (
    <View style={styles.container}>
      <Text style={styles.header}>Register</Text>
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
      <TextInput
        placeholder="Retyped password"
        placeholderTextColor={Colors.Gray}
        style={styles.input}
        value={retypedPassword}
        onChangeText={setRetypedPassword}
      />
      <Pressable onPress={handleRegister}>
        <Text style={styles.register}>Register</Text>
      </Pressable>
      <Pressable onPress={props.toggleView}>
        <Text style={styles.loginLink}>login existing user</Text>
      </Pressable>
    </View>
  );
}

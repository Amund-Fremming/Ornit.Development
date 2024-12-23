import { View, Text } from "react-native";

import { styles } from "./exampleScreenStyles";

export default function ExampleScreen() {
  return (
    <View style={styles.container}>
      <Text style={styles.header}>ExampleScreen</Text>
    </View>
  );
}

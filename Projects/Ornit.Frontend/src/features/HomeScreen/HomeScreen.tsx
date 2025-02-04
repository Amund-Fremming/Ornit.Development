import { View, Text } from "react-native";

import { styles } from "./homeScreenStyles";

export default function HomeScreen() {
  return (
    <View style={styles.container}>
      <Text style={styles.header}>HomeScreen</Text>
    </View>
  );
}

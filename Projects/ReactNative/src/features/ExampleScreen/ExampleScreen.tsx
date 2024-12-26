import { View, Text } from "react-native";

import { styles } from "./exampleScreenStyles";
import SmallButton from "@/src/shared/components/SmallButton/SmallButton";
import { Colors } from "@/src/shared/constants/Colors";
import MediumButton from "@/src/shared/components/MediumButton/MediumButton";
import BigButton from "@/src/shared/components/BigButton/BigButton";
import { useModalProvider } from "@/src/shared/providers/ModalProvider";

export default function ExampleScreen() {
  return (
    <View style={styles.container}>
      <Text style={styles.header}>ExampleScreen</Text>
      <SmallButton
        text="Small"
        color={Colors.Black}
        inverted={true}
        onClick={() => console.log("Small clicked")}
      />
      <MediumButton
        text="Medium"
        color={Colors.Black}
        inverted={true}
        onClick={() => console.log("Medium clicked")}
      />
      <BigButton
        text="Big"
        color={Colors.Black}
        inverted={true}
        onClick={() => console.log("Big clicked")}
      />
      <SmallButton
        text="Small"
        color={Colors.Gray}
        inverted={true}
        onClick={}
      />
    </View>
  );
}

import { Text, TouchableOpacity } from "react-native";

import { styles } from "./bigButtonStyles";

interface IBigButton {
  text: string;
  primaryColor: string;
  secondaryColor: string;
  onClick: () => void;
}

export default function BigButton(props: IBigButton) {
  return (
    <TouchableOpacity
      onPress={props.onClick}
      style={{ ...styles, backgroundColor: props.primaryColor }}
    >
      <Text style={{ ...styles, color: props.secondaryColor }}>
        {props.text}
      </Text>
    </TouchableOpacity>
  );
}

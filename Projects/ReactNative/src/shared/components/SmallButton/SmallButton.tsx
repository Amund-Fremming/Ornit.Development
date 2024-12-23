import { Text, TouchableOpacity } from "react-native";

import { styles } from "./smallButtonStyles";

interface IMediumButton {
  text: string;
  primaryColor: string;
  secondaryColor: string;
  onClick: () => void;
}

export default function MediumButton(props: IMediumButton) {
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

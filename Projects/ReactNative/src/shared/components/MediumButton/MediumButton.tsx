import { Text, TouchableOpacity } from "react-native";

import { styles } from "./mediumButtonStyles";
import { Colors } from "react-native/Libraries/NewAppScreen";

interface IMediumButton {
  text: string;
  color: string;
  inverted: boolean;
  onClick: () => void;
}

export default function MediumButton(props: IMediumButton) {
  const getButtonStyles = () => {
    if (props.inverted) {
      return {
        ...styles.container,
        backgroundColor: Colors.White,
        borderColor: props.color,
        borderWidth: 2,
      };
    } else {
      return { ...styles.container, backgroundColor: props.color };
    }
  };

  const getTextStyles = () => {
    if (props.inverted) {
      return { ...styles.text, color: props.color };
    } else {
      return { ...styles.text, color: Colors.White };
    }
  };

  return (
    <TouchableOpacity onPress={props.onClick} style={getButtonStyles()}>
      <Text style={getTextStyles()}>{props.text}</Text>
    </TouchableOpacity>
  );
}

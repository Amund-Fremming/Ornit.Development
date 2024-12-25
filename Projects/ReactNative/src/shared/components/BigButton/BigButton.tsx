import { Text, TouchableOpacity } from "react-native";

import { styles } from "./bigButtonStyles";
import { Colors } from "react-native/Libraries/NewAppScreen";

interface IBigButton {
  text: string;
  color: string;
  inverted: boolean;
  onClick: () => void;
}

export default function BigButton(props: IBigButton) {
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

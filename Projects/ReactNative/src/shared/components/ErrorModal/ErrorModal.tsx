import { Modal, Pressable, Text, View } from "react-native";

import { styles } from "./errorModalStyles";

interface IErrorModal {
  message: string;
  errorModalVisible: boolean;
  setErrorModalVisible: (condition: boolean) => void;
}

export default function ErrorModal(props: IErrorModal) {
  return (
    <Modal
      visible={props.errorModalVisible}
      animationType="fade"
      transparent={true}
    >
      <View style={styles.container}>
        <Text style={styles.message}>{props.message}</Text>
        <Pressable
          onPress={() => props.setErrorModalVisible(!props.errorModalVisible)}
          style={styles.button}
        >
          <Text style={styles.buttonText}>Close</Text>
        </Pressable>
      </View>
    </Modal>
  );
}

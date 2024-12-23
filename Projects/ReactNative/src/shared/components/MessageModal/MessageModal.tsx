import { Modal, Pressable, Text, View } from "react-native";

import { styles } from "./messageModalStyles";

interface IMessageModal {
  message: string;
  errorModalVisible: boolean;
  setErrorModalVisible: (condition: boolean) => void;
}

export default function MessageModal(props: IMessageModal) {
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

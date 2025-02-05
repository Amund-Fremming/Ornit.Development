import { Modal, Pressable, Text, View } from "react-native";
import { styles } from "./infoModalStyles";

interface IInfoModal {
  message: string;
  isError: boolean;
  modalVisible: boolean;
  setModalVisible: (condition: boolean) => void;
}

export default function InfoModal(props: IInfoModal) {
  return (
    <Modal visible={props.modalVisible} animationType="fade" transparent={true}>
      <View style={styles.overlay}>
        <View
          style={[
            styles.genericContainer,
            props.isError ? styles.errorContainer : styles.messageContainer,
          ]}
        >
          <Text style={styles.header}>{props.isError ? "Ooops" : "Hey"}</Text>
          <Text style={styles.message}>{props.message}</Text>
          <Pressable
            onPress={() => props.setModalVisible(!props.modalVisible)}
            style={styles.button}
          >
            <Text style={styles.buttonText}>Close</Text>
          </Pressable>
        </View>
      </View>
    </Modal>
  );
}

import { Modal, Pressable, Text, View } from "react-native";
import { styles } from "./actionModalStyles";

interface IActionModal {
  message: string;
  modalVisible: boolean;
  setModalVisible: (condition: boolean) => void;
  onNoClick: () => void;
  onYesClick: () => void;
}

export default function ActionModal(props: IActionModal) {
  const handleYesPressed = () => {
    props.setModalVisible(false);
    props.onYesClick();
  };

  const handleNoPressed = () => {
    props.setModalVisible(false);
    props.onNoClick();
  };

  return (
    <Modal visible={props.modalVisible} animationType="fade" transparent={true}>
      <View style={styles.overlay}>
        <View style={styles.container}>
          <Text style={styles.header}>Hey</Text>
          <Text style={styles.message}>{props.message}</Text>
          <View style={styles.buttonsWrapper}>
            <Pressable onPress={handleNoPressed} style={styles.button}>
              <Text style={styles.buttonText}>no</Text>
            </Pressable>
            <Pressable onPress={handleYesPressed} style={styles.button}>
              <Text style={styles.buttonText}>yes</Text>
            </Pressable>
          </View>
        </View>
      </View>
    </Modal>
  );
}

import React, { createContext, ReactNode, useContext, useState } from "react";
import { Modal, Text, View, Button, StyleSheet } from "react-native";
import InfoModal from "../components/InfoModal/InfoModal";

interface IModalContext {
  isError: boolean;
  setIsError: React.Dispatch<React.SetStateAction<boolean>>;
  message: string;
  setMessage: React.Dispatch<React.SetStateAction<string>>;
  modalVisible: boolean;
  setModalVisible: React.Dispatch<React.SetStateAction<boolean>>;
}

const defaultContextValue: IModalContext = {
  isError: true,
  setIsError: () => {},
  message: "",
  setMessage: () => {},
  modalVisible: false,
  setModalVisible: () => {},
};

const ModalContext = createContext<IModalContext>(defaultContextValue);

export const useModalProvider = () => useContext(ModalContext);

interface ModalProviderProps {
  children: ReactNode;
}

export const ModalProvider = ({ children }: ModalProviderProps) => {
  const [isError, setIsError] = useState<boolean>(false);
  const [message, setMessage] = useState<string>("");
  const [modalVisible, setModalVisible] = useState<boolean>(false);

  const value = {
    isError,
    setIsError,
    message,
    setMessage,
    modalVisible,
    setModalVisible,
  };

  return (
    <ModalContext.Provider value={value}>
      {children}
      <InfoModal
        message={message}
        isError={isError}
        modalVisible={modalVisible}
        setModalVisible={setModalVisible}
      />
    </ModalContext.Provider>
  );
};

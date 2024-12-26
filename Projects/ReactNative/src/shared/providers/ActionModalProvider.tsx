import React, { createContext, ReactNode, useContext, useState } from "react";
import ActionModal from "../components/ActionModal/ActionModal";

interface IActionModalContext {
  toggleActionModal: (
    message: string,
    onNoClick: () => void,
    onYesClick: () => void
  ) => void;
}

const defaultContextValue: IActionModalContext = {
  toggleActionModal: () => {},
};

const ActionModalContext =
  createContext<IActionModalContext>(defaultContextValue);

export const useActionModalProvider = () => useContext(ActionModalContext);

interface ActionModalProviderProps {
  children: ReactNode;
}

export const ActionModalProvider = ({ children }: ActionModalProviderProps) => {
  const [message, setMessage] = useState<string>("");
  const [modalVisible, setModalVisible] = useState<boolean>(false);
  const [onYesClick, setOnYesClick] = useState<() => void>(() => {});
  const [onNoClick, setOnNoClick] = useState<() => void>(() => {});

  const toggleActionModal = (
    localMesssage: string,
    localOnNoClick: () => void,
    localOnYesClick: () => void
  ) => {
    setMessage(localMesssage);
    setOnNoClick(() => localOnNoClick);
    setOnYesClick(() => localOnYesClick);
    setModalVisible(true);
  };

  const value = {
    toggleActionModal,
  };

  return (
    <ActionModalContext.Provider value={value}>
      {children}
      <ActionModal
        message={message}
        modalVisible={modalVisible}
        setModalVisible={setModalVisible}
        onYesClick={onYesClick}
        onNoClick={onNoClick}
      />
    </ActionModalContext.Provider>
  );
};

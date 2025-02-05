import AuthScreen from "@/src/features/AuthScreen/AuthScreen";
import { ReactNode, createContext, useContext, useState } from "react";
import * as SecureStore from "expo-secure-store";

interface IAuthContext {
  getToken: () => Promise<string>;
  setToken: (token: string) => Promise<void>;
  logout: () => Promise<void>;
}

const defaultContextValue: IAuthContext = {
  getToken: async () => "",
  setToken: async () => {},
  logout: async () => {},
};

const AuthContext = createContext<IAuthContext>(defaultContextValue);

export const useAuthProvider = () => useContext(AuthContext);

interface AuthProviderProps {
  children: ReactNode;
}

export const AuthProvider = ({ children }: AuthProviderProps) => {
  const TOKEN_KEY = "6e444484-ffe5-4935-a20e-3ee1595288ad";
  const [loggedIn, setLoggedIn] = useState<boolean>(false);

  const value = {
    getToken,
    setToken,
    logout,
  };

  async function setToken(token: string): Promise<void> {
    try {
      await SecureStore.setItemAsync(TOKEN_KEY, token, {
        keychainAccessible: SecureStore.WHEN_UNLOCKED_THIS_DEVICE_ONLY,
      });
      setLoggedIn(true);
    } catch (error) {
      console.error("Error saving token:", error);
    }
  }

  async function getToken(): Promise<string> {
    try {
      var result = await SecureStore.getItemAsync(TOKEN_KEY);
      if (result === null) {
        return "";
      }

      return result;
    } catch (error) {
      console.error("Error getting token:", error);
      return "";
    }
  }

  async function logout(): Promise<void> {
    try {
      setLoggedIn(false);
      await SecureStore.deleteItemAsync(TOKEN_KEY);
      console.log("Token was deleted!"); // remove
    } catch (error) {
      console.error("Error deleting token:", error);
    }
  }

  return (
    <AuthContext.Provider value={value}>
      {loggedIn && children}
      {!loggedIn && <AuthScreen />}
    </AuthContext.Provider>
  );
};

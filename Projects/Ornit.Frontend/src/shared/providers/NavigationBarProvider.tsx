import { ReactNode, createContext, useContext, useState } from "react";
import { Animated } from "react-native";

interface INavBarContext {
  navBarVisibility:
    | Animated.Value
    | Animated.AnimatedInterpolation<string | number>
    | "flex"
    | "none"
    | undefined;
  setNavBarVisibility: React.Dispatch<
    React.SetStateAction<
      | Animated.Value
      | Animated.AnimatedInterpolation<string | number>
      | "flex"
      | "none"
      | undefined
    >
  >;
}

const defaultContextValue: INavBarContext = {
  navBarVisibility: "flex",
  setNavBarVisibility: () => {},
};

const NavBarContext = createContext<INavBarContext>(defaultContextValue);

export const useNavBarProvider = () => useContext(NavBarContext);

interface NavBarProviderProps {
  children: ReactNode;
}

export const AuthProvider = ({ children }: NavBarProviderProps) => {
  const [navBarVisibility, setNavBarVisibility] = useState<
    | Animated.Value
    | Animated.AnimatedInterpolation<string | number>
    | "flex"
    | "none"
    | undefined
  >("flex");

  const value = {
    navBarVisibility,
    setNavBarVisibility,
  };

  return (
    <NavBarContext.Provider value={value}>{children}</NavBarContext.Provider>
  );
};

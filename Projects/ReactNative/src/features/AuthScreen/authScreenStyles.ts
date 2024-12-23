import { moderateScale } from "@/src/shared/constants/dimensions";
import { StyleSheet } from "react-native";

export const styles = StyleSheet.create({
  container: {
    justifyContent: "center",
    alignItems: "center",
    height: "100%",
    width: "100%",
  },
  header: {
    fontFamily: "SpaceMono",
    fontSize: moderateScale(35),
  },
});

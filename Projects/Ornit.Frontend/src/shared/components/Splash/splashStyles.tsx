import { StyleSheet } from "react-native";
import { moderateScale } from "../../common/dimensions";

export const styles = StyleSheet.create({
  container: {
    justifyContent: "center",
    alignItems: "center",
    width: "100%",
    height: "100%",
  },
  text: {
    fontFamily: "SpaceMono",
    fontSize: moderateScale(30),
  },
});

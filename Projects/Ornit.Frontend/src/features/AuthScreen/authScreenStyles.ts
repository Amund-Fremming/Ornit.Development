import { moderateScale } from "@/src/shared/common/dimensions";
import { Colors } from "@/src/shared/constants/Colors";
import { StyleSheet } from "react-native";

export const styles = StyleSheet.create({
  container: {
    justifyContent: "center",
    alignItems: "center",
    height: "100%",
    width: "100%",
    backgroundColor: Colors.White,
  },
  header: {
    fontFamily: "SpaceMono",
    fontSize: moderateScale(35),
  },
});

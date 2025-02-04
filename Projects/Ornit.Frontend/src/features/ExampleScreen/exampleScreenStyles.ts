import { moderateScale, verticalScale } from "@/src/shared/common/dimensions";
import { StyleSheet } from "react-native";

export const styles = StyleSheet.create({
  container: {
    justifyContent: "center",
    alignItems: "center",
    width: "100%",
    height: "100%",
    gap: verticalScale(25),
  },
  header: {
    fontFamily: "SpaceMono",
    fontSize: moderateScale(26),
    fontWeight: 800,
  },
  paragraph: {},
});

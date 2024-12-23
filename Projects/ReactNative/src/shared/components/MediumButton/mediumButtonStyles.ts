import { StyleSheet } from "react-native";
import {
  horizontalScale,
  moderateScale,
  verticalScale,
} from "../../constants/dimensions";

export const styles = StyleSheet.create({
  container: {
    width: horizontalScale(140),
    height: verticalScale(60),
    justifyContent: "center",
    alignItems: "center",
  },
  text: {
    fontFamily: "SpaceMono",
    fontSize: moderateScale(18),
  },
});

import { StyleSheet } from "react-native";
import {
  horizontalScale,
  moderateScale,
  verticalScale,
} from "../../constants/dimensions";

export const styles = StyleSheet.create({
  container: {
    width: horizontalScale(200),
    height: verticalScale(80),
    justifyContent: "center",
    alignItems: "center",
  },
  text: {
    fontFamily: "SpaceMono",
    fontSize: moderateScale(22),
  },
});

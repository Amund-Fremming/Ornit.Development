import {
  horizontalScale,
  moderateScale,
  verticalScale,
} from "@/src/shared/common/dimensions";
import { StyleSheet } from "react-native";
import { Colors } from "react-native/Libraries/NewAppScreen";

export const styles = StyleSheet.create({
  container: {
    justifyContent: "center",
    alignItems: "center",
    gap: verticalScale(10),
    width: horizontalScale(300),
    borderWidth: moderateScale(2),
    paddingVertical: verticalScale(30),
  },
  header: {
    fontFamily: "SpaceMono",
    fontSize: moderateScale(25),
  },
  input: {
    fontFamily: "SpaceMono",
    color: Colors.Black,
  },
  register: {
    fontFamily: "SpaceMono",
    fontSize: moderateScale(16),
  },
  loginLink: {
    fontFamily: "SpaceMono",
    fontSize: moderateScale(12),
  },
});

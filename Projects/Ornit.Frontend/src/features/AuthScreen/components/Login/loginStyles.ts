import { Colors } from "@/src/shared/constants/Colors";
import {
  horizontalScale,
  moderateScale,
  verticalScale,
} from "@/src/shared/common/dimensions";
import { StyleSheet } from "react-native";

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
  login: {
    fontFamily: "SpaceMono",
    fontSize: moderateScale(16),
  },
  registerLink: {
    fontFamily: "SpaceMono",
    fontSize: moderateScale(12),
  },
});

import TError from "../types/models/TError";
import { enqueueSnackbar } from "notistack";
type TErrorResponse = {
  status: number;
  data: TError;
};

export const errorHandler = (errResponse: any) => {
  //message
  //errors: []
  console.log("==ERROR HANDLER==");
  console.log(errResponse);
  if (errResponse.data) {
    if (errResponse.data.errors && errResponse.data.errors.length > 0) {
      errResponse.data.errors.map((error: TError) => {
        enqueueSnackbar(error.message, { variant: "error" });
        return;
      });
    } else if (
      errResponse.data.message &&
      errResponse.data.message.length > 0
    ) {
      enqueueSnackbar(errResponse.data.message, { variant: "error" });
      return;
    }
  }
  switch (errResponse.status) {
    case 400:
      enqueueSnackbar("Bad request", { variant: "error" });
      break;
    case 500:
      enqueueSnackbar("Internal server problem.", { variant: "error" });
      break;
    case 404:
      enqueueSnackbar("Not found", { variant: "error" });
      break;
    case 401:
      enqueueSnackbar("You are not allowed to do this", { variant: "error" });
      break;
    default:
      enqueueSnackbar("Something went wrong :(", { variant: "error" });
      break;
  }
};

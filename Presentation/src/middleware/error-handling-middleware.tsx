import {
  Middleware,
  MiddlewareAPI,
  isRejected,
  isRejectedWithValue,
} from "@reduxjs/toolkit";
import { useSnackbar } from "notistack";
import { useAppSelector } from "../features/hooks";

export const rtkQueryErrorLogger: Middleware =
  (api: MiddlewareAPI) => (next) => (action) => {
    if (isRejectedWithValue(action)) {
      console.warn(action.error.data);
      //enqueueSnackbar("some error", { variant: "error" });
    } else if (isRejected(action)) {
      console.warn(action.error);
    }

    return next(action);
  };

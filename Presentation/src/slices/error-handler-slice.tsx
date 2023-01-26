import { PayloadAction, createSlice } from "@reduxjs/toolkit";
import TUser from "../types/models/TUser";
import TUserAuth from "../types/models/TUserAuth";
import jwtDecode, { JwtPayload } from "jwt-decode";

let initialState: ErrorState = {
  isError: false,
};

type TError = {
  message: string;
  errors?: TError[];
};

type ErrorState = {
  isError: boolean;
  content?: {
    status?: number;
    data?: TError;
  };
};

const errorsSlice = createSlice({
  name: "users",
  initialState,
  reducers: {
    setErrors: (state, action: PayloadAction<Partial<ErrorState>>) => {
      state.content = {
        status: action.payload.content?.status,
        data: action.payload.content?.data,
      };
    },
    clearErrors: (state) => {
      state.content = undefined;
    },
  },
});

export const { clearErrors } = errorsSlice.actions;
export default errorsSlice.reducer;

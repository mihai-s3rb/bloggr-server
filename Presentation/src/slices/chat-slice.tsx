import { PayloadAction, createSlice } from "@reduxjs/toolkit";
import TUser from "../types/models/TUser";
import TUserAuth from "../types/models/TUserAuth";
import jwtDecode, { JwtPayload } from "jwt-decode";

type ChatState = {
  isOpened: boolean;
  sendTo?: string;
};

let initialState: ChatState = {
  isOpened: false,
};

const chatSlice = createSlice({
  name: "chat",
  initialState,
  reducers: {
    openChat: (state, action: PayloadAction<string>) => {
      state.isOpened = true;
      state.sendTo = action.payload;
    },
    closeChat: (state) => {
      state.isOpened = false;
      state.sendTo = undefined;
    },
  },
});

export const { openChat, closeChat } = chatSlice.actions;
export default chatSlice.reducer;

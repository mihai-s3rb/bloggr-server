import { PayloadAction, createSlice } from "@reduxjs/toolkit";
import TUser from "../types/models/TUser";
import TUserAuth from "../types/models/TUserAuth";
import jwtDecode, { JwtPayload } from "jwt-decode";

let initialState: UserState = {
  isLoggedIn: false,
};

export const checkTokenIfValid = (token: string) => {
  const payload = jwtDecode<JwtPayload>(token);
  const exp = payload.exp;
  if (exp && Date.now() < exp * 1000) return true;
  return false;
};

const storedState = localStorage.getItem("initialState");
if (storedState !== null) {
  const data: UserState = JSON.parse(storedState);
  if (data.token) {
    if (checkTokenIfValid(data.token)) {
      initialState = {
        isLoggedIn: true,
        token: data.token,
        user: data.user,
      };
    }
  }
}

type UserState = {
  search?: string;
  isLoggedIn: boolean;
  token?: string;
  user?: TUser;
};

const userSlice = createSlice({
  name: "users",
  initialState,
  reducers: {
    setUserAuth: (state, action: PayloadAction<TUserAuth>) => {
      state.isLoggedIn = true;
      state.user = action.payload.user;
      state.token = action.payload.token;
      localStorage.setItem("initialState", JSON.stringify(state));
    },
    setUser: (state, action: PayloadAction<TUser>) => {
      state.user = action.payload;
      localStorage.setItem("initialState", JSON.stringify(state));
    },
    logoutUser: (state) => {
      state.isLoggedIn = false;
      state.user = undefined;
      state.token = undefined;
      localStorage.clear();
    },
    setUserSearch: (state, action: PayloadAction<string>) => {
      state.search = action.payload;
    },
    clearUserSearch: (state) => {
      state.search = undefined;
    },
  },
});

export const {
  setUser,
  setUserAuth,
  logoutUser,
  setUserSearch,
  clearUserSearch,
} = userSlice.actions;
export default userSlice.reducer;

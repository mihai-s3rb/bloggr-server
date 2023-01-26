import { store } from "../store";
import { setUser, setUserAuth } from "../slices/user-slice";
import TUserAuth from "../types/models/TUserAuth";

const SetCredentials = (userData: TUserAuth) => {
  store.dispatch(setUserAuth(userData));
};

export default SetCredentials;

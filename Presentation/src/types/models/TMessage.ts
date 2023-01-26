import TUser from "./TUser";

export type TMessage = {
  id: number;
  creationDate: string;
  sender: TUser;
  senderId: number;
  receiverId: number;
  content: string;
};

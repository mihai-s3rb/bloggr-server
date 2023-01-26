import { TMessage } from "./TMessage";

export type TMessagesPage = {
  nextCursor: number | null;
  result: TMessage[];
};

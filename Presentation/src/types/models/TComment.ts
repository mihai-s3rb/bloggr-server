import TUser from "./TUser";

interface TComment {
  id: number;
  creationDate: string;
  content: string;
  user: TUser;
  userId: number;
  postId: number;
}

export default TComment;

import { RawDraftContentState } from "draft-js";
import TInterest from "./TInterest";
import TUser from "./TUser";

interface TPost {
  id: number;
  userId: number;
  title: string;
  content: string;
  caption: string;
  captionImageUrl: string;
  creationDate: string;
  user: TUser;
  interests: TInterest[];
  numberOfLikes?: number;
  numberOfComments?: number;
  isLikedByUser?: boolean;
  isBookmarkedByUser?: boolean;
}

export default TPost;

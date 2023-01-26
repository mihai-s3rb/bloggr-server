import TInterest from "./TInterest";
import TUser from "./TUser";

interface TFeedPost {
  id: number;
  userId: number;
  title: string;
  caption: string;
  creationDate: string;
  captionImageUrl: string;
  user: TUser;
  interests: TInterest[];
}

export default TFeedPost;

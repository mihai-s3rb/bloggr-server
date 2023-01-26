import TInterest from "./TInterest";

interface User {
  id: number;
  email: string;
  userName: string;
  firstName: string;
  lastName: string;
  profileImageUrl: string;
  backgroundImageUrl: string;
  bio: string;
  birthDate: string;
  interests: TInterest[];
}

export default User;

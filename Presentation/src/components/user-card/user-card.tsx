import {
  Card,
  CardContent,
  Typography,
  CardActions,
  Button,
  Box,
  Divider,
} from "@mui/material";
import ProfileImage from "./profile-image";
import TUser from "../../types/models/TUser";
import AvatarChip from "../avatar-chip/avatar-chip";
import { store } from "../../store";
import { openChat } from "../../slices/chat-slice";
import ProtectedComponent from "../protected-component/protected-component";

type UserCard = {
  user: Partial<TUser> | undefined;
};

const UserCard = (props: UserCard) => {
  if (!props.user) return <>Could not find user</>;
  const { firstName, lastName, bio, userName, profileImageUrl } = props.user;

  const handleClick = () => {
    if (userName) store.dispatch(openChat(userName));
  };

  return (
    <Box
      sx={{
        wordBreak: "break-all",
        maxWidth: "500px",
        minWidth: "250px",
        mr: 4,
      }}
    >
      <ProfileImage backgroundImage={profileImageUrl!} />
      <Box>
        <AvatarChip user={props.user} />
        <Typography sx={{ mb: 1 }} variant="h5">
          {`${firstName} ${lastName}`}
        </Typography>
        <Typography>{bio}</Typography>
      </Box>
      <Divider sx={{ mt: 2, mb: 2 }} />
      <ProtectedComponent>
        <Button variant="contained" onClick={handleClick}>
          Chat
        </Button>
      </ProtectedComponent>
    </Box>
  );
};

export default UserCard;

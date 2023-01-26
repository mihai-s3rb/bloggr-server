import { Chip, Avatar } from "@mui/material";
import TUser from "../../types/models/TUser";
import { useNavigate } from "react-router-dom";

type AvatarChip = {
  user: Partial<TUser> | undefined;
};

const AvatarChip = (props: AvatarChip) => {
  const navigate = useNavigate();
  const { userName, profileImageUrl = "" } = props.user
    ? props.user
    : { userName: "deleted" };

  const handleClick = (e: React.MouseEvent<HTMLElement>) => {
    e.stopPropagation();
    if (userName != "deleted") navigate(`/${userName}`);
  };
  return (
    <Chip
      onClick={(e) => handleClick(e)}
      avatar={<Avatar alt={userName} src={profileImageUrl} />}
      label={userName}
      variant="outlined"
    />
  );
};

export default AvatarChip;

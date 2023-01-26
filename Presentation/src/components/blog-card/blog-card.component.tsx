import {
  Card,
  CardMedia,
  CardContent,
  Typography,
  Stack,
  Chip,
  Avatar,
  CardActions,
  Button,
  Box,
  Checkbox,
} from "@mui/material";
import TInterest from "../../types/models/TInterest";
import { useNavigate } from "react-router-dom";
import PostFeedImg from "./imageContainer";
import { Favorite, FavoriteBorder } from "@mui/icons-material";
import BookmarkBorderIcon from "@mui/icons-material/BookmarkBorder";
import BookmarkIcon from "@mui/icons-material/Bookmark";
import PostInteractions from "../post-interactions/post-interactions";
import RenderInterests from "../render-interests/render-interests";
import AvatarChip from "../avatar-chip/avatar-chip";
import TUser from "../../types/models/TUser";
import ProtectedComponent from "../protected-component/protected-component";
import TPost from "../../types/models/TPost";
import TFeedPost from "../../types/models/TFeedPost";
import CommentIcon from "@mui/icons-material/Comment";
// type BlogCard = {
//   id: number;
//   title: string;
//   user: Partial<TUser>;
//   imageCaptionUrl: string;
//   interests: TInterest[];
//   caption: string;
//   children?: React.ReactNode;
// };

type BlogCard = {
  post: Partial<TPost>;
  children?: React.ReactNode;
};

const BlogCard = (props: BlogCard) => {
  const navigate = useNavigate();
  const {
    id,
    title,
    user,
    captionImageUrl,
    interests,
    caption,
    isLikedByUser,
    isBookmarkedByUser,
    numberOfLikes,
    numberOfComments,
    creationDate,
  } = props.post;
  let formatedDate = "";
  if (creationDate) {
    const date = Date.parse(creationDate);
    const options = { hour: "numeric", month: "short" };
    formatedDate = new Intl.DateTimeFormat("en-GB").format(date);
  }

  return (
    <Box
      onClick={() => navigate(`/post/${id}`)}
      sx={{
        cursor: "pointer",
        width: "100%",
        maxWidth: "800px",
        display: "flex",
        justifyContent: "space-between",
        flexDirection: { xs: "column", sm: "row" },
      }}
    >
      <Box
        sx={{
          display: "flex",
          justifyContent: "space-between",
          flexDirection: "column",
          flexWrap: "wrap",
          wordBreak: "break-all",
          width: { sm: "60%" },
        }}
      >
        <Box sx={{ display: "flex", justifyContent: "space-between" }}>
          <AvatarChip user={user} />
          <Typography>{formatedDate}</Typography>
        </Box>

        <Box sx={{ alignSelf: "flex-start" }}>
          <Typography gutterBottom variant="h5" component="div">
            {title?.substring(0, 100)}
          </Typography>
          <Typography variant="body2" color="text.secondary">
            {caption?.substring(0, 200)}
          </Typography>
        </Box>

        <Box
          sx={{
            display: "flex",
            flexWrap: "wrap",
            justifyContent: "space-between",
            alignItems: "center",
          }}
        >
          <Box sx={{ display: "flex", flexWrap: "wrap", gap: 0.25, mt: 2 }}>
            <RenderInterests interests={interests ? interests : []} />
          </Box>
          <Box sx={{ marginLeft: "auto", display: "inline-flex" }}>
            <Box sx={{ alignSelf: "center" }}>
              <span>{numberOfComments} comments </span>
              <span>{numberOfLikes} likes </span>
            </Box>
            <ProtectedComponent>
              <PostInteractions
                postId={id}
                isLikedByUser={isLikedByUser}
                isBookmarkedByUser={isBookmarkedByUser}
              />
            </ProtectedComponent>
          </Box>
        </Box>
      </Box>
      <Box
        sx={{
          height: { xs: "300px", sm: "auto" },
          width: { xs: "auto", sm: "30%" },
        }}
      >
        <PostFeedImg backgroundImage={captionImageUrl} />
      </Box>
    </Box>
    // <Card sx={{ width: "100%", maxWidth: "800px", wordBreak: "break-all" }}>
    //     <CardMedia
    //       sx={{maxHeight: "200px"}}
    //       component="img"
    //       image={imageCaptionUrl}
    //       alt={caption}
    //     />
    //     <CardContent>
    //       <div>
    //         <Typography gutterBottom variant="h5" component="div">
    //           {title}
    //         </Typography>
    //         <Stack direction="row" spacing={1}>
    //           <Chip
    //             avatar={<Avatar alt={username} src="https://analystprep.com/cfa-level-1-exam/wp-content/uploads/2016/09/person-flat.png" />}
    //             label={username}
    //             variant="outlined"
    //           />
    //           {interests.map((interest: TInterest) => {
    //             return <Chip label={interest.name} color="primary" variant="outlined" />
    //           })}
    //         </Stack>
    //         <Typography variant="body2" color="text.secondary">
    //           {caption}
    //         </Typography>
    //       </div>
    //     </CardContent>
    //     <CardActions>
    //       <Button size="small">Like</Button>
    //       <Button size="small" onClick={() => navigate(`/post/${id}`)}>Read</Button>
    //       <Button size="small">Read later</Button>
    //       <Button size="small">Share</Button>
    //     </CardActions>
    //   </Card>
  );
};
export default BlogCard;

import React from "react";
import ReadonlyEditor from "../../components/readonly-editor/ReadonlyEditor";
import { useGetPostQuery } from "../../features/api/bloggrApiSlice";
import { useParams } from "react-router-dom";
import Box from "@mui/material/Box";
import Typography from "@mui/material/Typography";
import { RawDraftContentState } from "draft-js";
import Chip from "@mui/material/Chip";
import Avatar from "@mui/material/Avatar";
import {
  Button,
  CircularProgress,
  Divider,
  Paper,
  TextField,
} from "@mui/material";
import UserCard from "../../components/user-card/user-card";
import Stack from "@mui/system/Stack";
import TInterest from "../../types/models/TInterest";
import PostInteractions from "../../components/post-interactions/post-interactions";
import ProfileImage from "../../components/user-card/profile-image";
import AvatarChip from "../../components/avatar-chip/avatar-chip";
import RenderInterests from "../../components/render-interests/render-interests";
import AddComment from "../../components/add-comment/add-comment";
import Comments from "../../components/comments/comments";
import ProtectedComponent from "../../components/protected-component/protected-component";
import { errorHandler } from "../../helpers/error-handler";
import { PostOptions } from "../../components/post-options/post-options";

export const View = () => {
  const { id } = useParams();

  const { data, error, isLoading } = useGetPostQuery(Number(id));
  if (isLoading) return <CircularProgress />;

  if (error) {
    errorHandler(error);
    return <p>Could not load content.</p>;
  }
  let formatedDate = "";
  if (data?.creationDate) {
    const date = Date.parse(data.creationDate);
    const options = { hour: "numeric", month: "short" };
    formatedDate = new Intl.DateTimeFormat("en-GB").format(date);
  }
  return (
    <>
      <Box
        sx={{
          display: "flex",
          gap: 4,
          justifyContent: { lg: "space-between", xs: "center" },
          flexWrap: { lg: "nowrap", xs: "wrap" },
          position: "relative",
          margin: "0 auto",
        }}
      >
        <Box sx={{ flexBasis: { lg: "70%" }, margin: "0 auto" }}>
          <Paper
            sx={{
              padding: 1.5,
              marginLeft: "auto",
            }}
          >
            <Box
              sx={{
                backgroundSize: "cover",
                backgroundImage: `url(${
                  data?.captionImageUrl ? data.captionImageUrl : ""
                })`,
                backgroundPosition: "center center",
                height: "300px",
                width: "100%",
              }}
              className="profile-container"
            ></Box>
            <Typography
              sx={{ wordBreak: "break-all" }}
              variant="h4"
              gutterBottom
            >
              {data?.title}
            </Typography>
            <Stack sx={{ flexWrap: "wrap", gap: 0.25 }} direction="row">
              <AvatarChip
                user={data?.user ? data.user : { userName: "deleted" }}
              />
              <RenderInterests
                interests={data?.interests ? data.interests : []}
              />
            </Stack>
            <Box sx={{ mt: 4, marginLeft: "auto", maxWidth: "800px" }}>
              <ReadonlyEditor content={data?.content} />
            </Box>
            <Typography>{formatedDate}</Typography>
            <Divider />
          </Paper>
          <Box
            sx={{
              display: "flex",
              justifyContent: "space-between",
              alignItems: "center",
            }}
          >
            <Box sx={{ display: "inline-flex" }}>
              <ProtectedComponent>
                <PostInteractions
                  postId={data?.id}
                  isLikedByUser={data?.isLikedByUser}
                  isBookmarkedByUser={data?.isBookmarkedByUser}
                />
              </ProtectedComponent>
              <Box sx={{ alignSelf: "center" }}>
                <span>{data?.numberOfComments} comments </span>
                <span>{data?.numberOfLikes} likes </span>
              </Box>
            </Box>
            <PostOptions post={data} />
          </Box>
          <Box sx={{ maxWidth: "800px", margin: "0 auto" }}>
            <AddComment post={data} />
            <Comments post={data} />
          </Box>
        </Box>
        <Divider
          sx={{ display: { xs: "none", xl: "block" } }}
          orientation="vertical"
          flexItem
        />
        <Box sx={{ flexBasis: { lg: "20%" } }}>
          <UserCard user={data?.user} />
        </Box>
      </Box>
    </>
  );
};

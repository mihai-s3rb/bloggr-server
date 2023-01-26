import React from "react";
import { FavoriteBorder, Favorite } from "@mui/icons-material";
import { Box, Checkbox } from "@mui/material";
import BookmarkBorderIcon from "@mui/icons-material/BookmarkBorder";
import BookmarkIcon from "@mui/icons-material/Bookmark";
import { useState } from "react";
import {
  useAddBookmarkMutation,
  useAddPostCommentMutation,
  useAddPostLikeMutation,
  useRemoveBookmarkMutation,
  useRemovePostLikeMutation,
} from "../../features/api/bloggrApiSlice";
import { enqueueSnackbar } from "notistack";
import { errorHandler } from "../../helpers/error-handler";

type PostInteractions = {
  postId?: number;
  isLikedByUser?: boolean;
  isBookmarkedByUser?: boolean;
};

const PostInteractions = ({
  postId,
  isLikedByUser,
  isBookmarkedByUser,
}: PostInteractions) => {
  const [checkedLike, setCheckedLike] = useState(
    isLikedByUser ? isLikedByUser : false
  );
  const [like] = useAddPostLikeMutation();
  const [unlike] = useRemovePostLikeMutation();

  const [checkedBookmark, setCheckedBookmark] = useState(
    isBookmarkedByUser ? isBookmarkedByUser : false
  );
  const [bookmark] = useAddBookmarkMutation();
  const [removeBookmark] = useRemoveBookmarkMutation();

  const handleLikeChange = (event: React.ChangeEvent<HTMLInputElement>) => {
    if (postId)
      if (checkedLike) {
        unlike(postId)
          .unwrap()
          .then(() => {
            enqueueSnackbar("Unliked post", { variant: "info" });
            setCheckedLike(false);
          })
          .catch((err) => errorHandler(err));
      } else {
        like(postId)
          .unwrap()
          .then(() => {
            enqueueSnackbar("Liked post", { variant: "info" });
            setCheckedLike(true);
          })
          .catch((err) => errorHandler(err));
      }
  };

  const handleBookmarkChange = (event: React.ChangeEvent<HTMLInputElement>) => {
    if (postId) {
      if (checkedBookmark) {
        removeBookmark(postId)
          .unwrap()
          .then(() => {
            enqueueSnackbar("Remove bookmarked post", { variant: "info" });
            setCheckedBookmark(false);
          })
          .catch((err) => errorHandler(err));
      } else {
        bookmark(postId)
          .unwrap()
          .then(() => {
            enqueueSnackbar("Bookmarked post", { variant: "info" });
            setCheckedBookmark(true);
          })
          .catch((err) => errorHandler(err));
      }
    }
  };
  return (
    <Box>
      <Checkbox
        checked={checkedLike}
        onChange={handleLikeChange}
        icon={<FavoriteBorder />}
        checkedIcon={<Favorite />}
      />
      <Checkbox
        checked={checkedBookmark}
        onChange={handleBookmarkChange}
        icon={<BookmarkBorderIcon />}
        checkedIcon={<BookmarkIcon />}
      />
    </Box>
  );
};

export default PostInteractions;

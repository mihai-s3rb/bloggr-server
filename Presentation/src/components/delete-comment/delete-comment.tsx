import { IconButton } from "@mui/material";
import { useAppSelector } from "../../features/hooks";
import TComment from "../../types/models/TComment";
import DeleteOutlineOutlinedIcon from "@mui/icons-material/DeleteOutlineOutlined";
import { useRemovePostCommentMutation } from "../../features/api/bloggrApiSlice";
import { enqueueSnackbar } from "notistack";
import { errorHandler } from "../../helpers/error-handler";

type DeleteComment = {
  comment: Partial<TComment>;
};

export const DeleteComment = ({ comment }: DeleteComment) => {
  const { isLoggedIn } = useAppSelector((state) => state.user);
  const userData = useAppSelector((state) => state.user);
  const userId = userData?.user?.id;

  const [deleteComment, deleteCommentData] = useRemovePostCommentMutation();

  const handleClick = () => {
    if (comment.postId && comment.id)
      deleteComment({ postId: comment.postId, commentId: comment.id })
        .unwrap()
        .then(() => {
          enqueueSnackbar("Comment deleted succesfully", {
            variant: "success",
          });
        })
        .catch((err) => errorHandler(err));
  };
  return (
    <>
      {isLoggedIn ? (
        comment.userId == userId ? (
          <IconButton
            onClick={handleClick}
            color="primary"
            aria-label="upload picture"
            component="label"
          >
            <DeleteOutlineOutlinedIcon />
          </IconButton>
        ) : null
      ) : null}
    </>
  );
};

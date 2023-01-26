import { Button, MenuItem } from "@mui/material";
import { useAppSelector } from "../../features/hooks";
import TPost from "../../types/models/TPost";
import { useState } from "react";
import EditIcon from "@mui/icons-material/Edit";
import DeleteIcon from "@mui/icons-material/Delete";
import KeyboardArrowDownIcon from "@mui/icons-material/KeyboardArrowDown";
import { StyledMenu } from "./styledMenu";
import { useRemovePostMutation } from "../../features/api/bloggrApiSlice";
import { enqueueSnackbar } from "notistack";
import { errorHandler } from "../../helpers/error-handler";
import { useNavigate } from "react-router-dom";

type PostOptions = {
  post?: Partial<TPost>;
};

export const PostOptions = ({ post }: PostOptions) => {
  const [anchorEl, setAnchorEl] = useState<null | HTMLElement>(null);
  const open = Boolean(anchorEl);
  const navigate = useNavigate();
  const [deletePost, deletePostData] = useRemovePostMutation();

  const handleClick = (event: React.MouseEvent<HTMLElement>) => {
    setAnchorEl(event.currentTarget);
  };
  const handleClose = () => {
    setAnchorEl(null);
  };

  const handleDelete = () => {
    if (post?.id)
      deletePost(post.id)
        .unwrap()
        .then(() => {
          enqueueSnackbar("Post deleted successfully", { variant: "success" });
          navigate("/", { replace: true });
        })
        .catch((err) => errorHandler(err));
    handleClose();
  };

  const handleUpdate = () => {
    handleClose();
    if (post) {
      navigate("/edit", { replace: true, state: { post } });
    }
  };

  const { isLoggedIn } = useAppSelector((state) => state.user);
  const userData = useAppSelector((state) => state.user);
  const userId = userData?.user?.id;
  return (
    <>
      {isLoggedIn ? (
        post?.userId == userId ? (
          <div>
            <Button
              id="demo-customized-button"
              aria-controls={open ? "demo-customized-menu" : undefined}
              aria-haspopup="true"
              aria-expanded={open ? "true" : undefined}
              variant="contained"
              disableElevation
              onClick={handleClick}
              endIcon={<KeyboardArrowDownIcon />}
            >
              Options
            </Button>
            <StyledMenu
              id="demo-customized-menu"
              MenuListProps={{
                "aria-labelledby": "demo-customized-button",
              }}
              anchorEl={anchorEl}
              open={open}
              onClose={handleClose}
            >
              <MenuItem onClick={handleUpdate} disableRipple>
                <EditIcon />
                Edit
              </MenuItem>
              <MenuItem onClick={handleDelete} disableRipple>
                <DeleteIcon />
                Delete
              </MenuItem>
            </StyledMenu>
          </div>
        ) : null
      ) : null}
    </>
  );
};

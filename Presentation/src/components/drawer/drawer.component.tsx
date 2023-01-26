import { Link, useNavigate } from "react-router-dom";
import {
  styled,
  Button,
  ButtonProps,
  Divider,
  List,
  ListItem,
  ListItemButton,
  ListItemIcon,
  ListItemText,
  Toolbar,
  Typography,
  Link as LinkAnchor,
} from "@mui/material";
import AccountCircleIcon from "@mui/icons-material/AccountCircle";
import { blue, green } from "@mui/material/colors";
import AddIcon from "@mui/icons-material/Add";
import AutoStoriesOutlinedIcon from "@mui/icons-material/AutoStoriesOutlined";
import ArticleOutlinedIcon from "@mui/icons-material/ArticleOutlined";
import CoPresentOutlinedIcon from "@mui/icons-material/CoPresentOutlined";
import SettingsOutlinedIcon from "@mui/icons-material/SettingsOutlined";
import QuizOutlinedIcon from "@mui/icons-material/QuizOutlined";
import AddCommentOutlinedIcon from "@mui/icons-material/AddCommentOutlined";
import InfoOutlinedIcon from "@mui/icons-material/InfoOutlined";
import { useAppSelector } from "../../features/hooks";
import BookmarksOutlinedIcon from "@mui/icons-material/BookmarksOutlined";
import ProtectedComponent from "../protected-component/protected-component";
import { enqueueSnackbar } from "notistack";

const ColorButton = styled(Button)<ButtonProps>(({ theme }) => ({
  backgroundColor: blue[500],
  "&:hover": {
    backgroundColor: blue[700],
  },
}));

export const DrawerContent = () => {
  const userData = useAppSelector((state) => state.user);
  const navigate = useNavigate();
  function enqueSnackbar(): void {
    throw new Error("Function not implemented.");
  }

  return (
    <div>
      <Toolbar />
      <List>
        <ProtectedComponent>
          <ListItem>
            <ColorButton
              onClick={() => navigate("/post")}
              variant="contained"
              startIcon={<AddIcon />}
            >
              <Typography color="white">New post</Typography>
            </ColorButton>
          </ListItem>
        </ProtectedComponent>
        <ListItem onClick={() => navigate("/")} disablePadding>
          <ListItemButton>
            <ListItemIcon>
              <AutoStoriesOutlinedIcon />
            </ListItemIcon>
            <ListItemText primary={"Feed"} />
          </ListItemButton>
        </ListItem>
        <ProtectedComponent>
          <ListItem
            onClick={() =>
              navigate(
                userData.isLoggedIn ? `/${userData.user?.userName}` : "/login"
              )
            }
            disablePadding
          >
            <ListItemButton>
              <ListItemIcon>
                <CoPresentOutlinedIcon />
              </ListItemIcon>
              <ListItemText primary={"My Blog"} />
            </ListItemButton>
          </ListItem>
        </ProtectedComponent>
        <ProtectedComponent>
          <ListItem onClick={() => navigate("/bookmarks")} disablePadding>
            <ListItemButton>
              <ListItemIcon>
                <BookmarksOutlinedIcon />
              </ListItemIcon>
              <ListItemText primary={"Bookmarks"} />
            </ListItemButton>
          </ListItem>
        </ProtectedComponent>
        {/* {mainItems.map(({ name, route, icon }, idx) => (
          <ListItem key={idx} onClick={() => navigate(route)} disablePadding>
            <ListItemButton>
              <ListItemIcon>{icon}</ListItemIcon>
              <ListItemText primary={name} />
            </ListItemButton>
          </ListItem>
        ))} */}
      </List>
      <Divider />

      <List>
        <ListItem disablePadding>
          <LinkAnchor
            underline="none"
            sx={{ width: "100%", color: "black" }}
            href="mailto:feedback@bloggr.com?cc=staff@bloggr.com&subject=Feedback regarding Bloggr App&body=..."
            onClick={() =>
              enqueueSnackbar("Opening your email app ✉️", { variant: "info" })
            }
          >
            <ListItemButton>
              <ListItemIcon>{<AddCommentOutlinedIcon />}</ListItemIcon>
              <ListItemText primary={"Send feedback"} />
            </ListItemButton>
          </LinkAnchor>
        </ListItem>
        <ListItem onClick={() => navigate("/about")} disablePadding>
          <ListItemButton>
            <ListItemIcon>{<InfoOutlinedIcon />}</ListItemIcon>
            <ListItemText primary={"About"} />
          </ListItemButton>
        </ListItem>
      </List>
    </div>
  );
};

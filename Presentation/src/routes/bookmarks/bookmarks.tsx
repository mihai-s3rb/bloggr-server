import { Box } from "@mui/material";
import ViewPosts from "../../components/view-posts/ViewPosts";
import { useAppSelector } from "../../features/hooks";

export const Bookmarks = () => {
  const userData = useAppSelector((state) => state.user);
  const userName = userData.user?.userName;
  return (
    <Box
      sx={{
        display: "flex",
        gap: 4,
        justifyContent: "center",
        position: "relative",
      }}
    >
      <ViewPosts isBookmarked={true} />
    </Box>
  );
};

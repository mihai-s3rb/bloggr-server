import { Box } from "@mui/material";
import ViewPosts from "../../components/view-posts/ViewPosts";

export const Home = () => {
  return (
    <>
      <Box
        sx={{
          display: "flex",
          gap: 4,
          justifyContent: "center",
          position: "relative",
        }}
      >
        <ViewPosts />
      </Box>
    </>
  );
};

import ToggleButton from "@mui/material/ToggleButton";
import ToggleButtonGroup from "@mui/material/ToggleButtonGroup";
import TInterest from "../../types/models/TInterest";
import Box from "@mui/material/Box";
import Typography from "@mui/material/Typography";
import { CircularProgress } from "@mui/material";
import { useGetInterestsQuery } from "../../features/api/bloggrApiSlice";
import { errorHandler } from "../../helpers/error-handler";

type ToggleInterest = {
  interests: string[];
  setInterests: (newInterests: string[]) => void;
};

const ToggleInterest = (props: ToggleInterest) => {
  const { interests, setInterests } = props;
  const { data, isLoading, isSuccess, error } = useGetInterestsQuery();

  const handleInterets = (
    event: React.MouseEvent<HTMLElement>,
    newInterests: string[]
  ) => {
    setInterests(newInterests);
  };
  if (isLoading) return <CircularProgress />;

  if (error) {
    errorHandler(error);
    return <p>Could not load content.</p>;
  }
  return (
    <>
      <Box
        className="custom-scrollbar"
        sx={{
          width: { xs: "100%", sm: "70%" },
          maxWidth: "600px",
          overflowX: "auto",
        }}
      >
        <ToggleButtonGroup
          size="small"
          color="primary"
          value={interests}
          onChange={handleInterets}
        >
          {data?.map((interest: TInterest) => {
            return (
              <ToggleButton
                sx={{ whiteSpace: "nowrap" }}
                value={interest.name}
                aria-label="bold"
              >
                <Typography>{interest.name}</Typography>
              </ToggleButton>
            );
          })}
        </ToggleButtonGroup>
      </Box>
    </>
  );
};

export default ToggleInterest;

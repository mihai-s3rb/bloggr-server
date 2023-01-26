import Chip from "@mui/material/Chip";
import TInterest from "../../types/models/TInterest";
import { redirect, useNavigate } from "react-router-dom";

type RenderInterests = {
  interests: TInterest[];
};
const RenderInterests = (props: RenderInterests) => {
  const navigate = useNavigate();
  const { interests } = props;

  const handleClick = (e: React.MouseEvent<HTMLElement>, name: string) => {
    e.stopPropagation();
    navigate(`/?interests=${name}`, { replace: true });
  };
  return (
    <>
      {interests.map((interest: TInterest) => {
        return (
          <Chip
            onClick={(e) => handleClick(e, interest.name)}
            label={interest.name}
            color="primary"
            variant="outlined"
          />
        );
      })}
    </>
  );
};

export default RenderInterests;

import Select from "react-select";
import { useGetInterestsQuery } from "../../features/api/bloggrApiSlice";
import { useEffect, useState } from "react";
import TInterest from "../../types/models/TInterest";
import { CircularProgress } from "@mui/material";
import { errorHandler } from "../../helpers/error-handler";

const options: TSelectInterest[] = [
  { value: 1, label: "cook" },
  { value: 2, label: "programming" },
];
export type TSelectInterest = {
  value: number;
  label: string;
};
const InterestsSelector = ({ ...field }) => {
  const { data, isLoading, isSuccess, error } = useGetInterestsQuery();

  const [options, setOptions] = useState<TSelectInterest[]>([]);

  useEffect(() => {
    if (isSuccess) {
      const options = data.map((interest: TInterest) => {
        return {
          value: interest.id,
          label: interest.name.toLowerCase(),
        };
      });
      setOptions(options);
    }
  }, [data]);
  if (isLoading) return <CircularProgress />;

  if (error) {
    errorHandler(error);
    return <p>Could not load content.</p>;
  }
  return (
    <Select
      options={options}
      isMulti
      className="interests-selector"
      classNamePrefix="select"
      placeholder="Select some interests"
      {...field}
    />
  );
};
export default InterestsSelector;

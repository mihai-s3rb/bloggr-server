import { Box, Button, Chip, CircularProgress, TextField } from "@mui/material";
import { useForm, SubmitHandler, Controller } from "react-hook-form";
import TInterest from "../../types/models/TInterest";
import { useAddUserInterestMutation } from "../../features/api/bloggrApiSlice";
import { enqueueSnackbar } from "notistack";
import { errorHandler } from "../../helpers/error-handler";

type InterestFormData = {
  name: string;
};

export const ManageInterests = () => {
  const {
    control: control2,
    reset,
    formState: { errors: errors2, isValid: isValid2, isDirty: isDirty2 },
    handleSubmit: handleFormSubmit2,
  } = useForm<InterestFormData>({
    mode: "onBlur",
    defaultValues: {
      name: "",
    },
  });
  const [create, { isLoading: isUpdating }] = useAddUserInterestMutation();

  const onSubmit: SubmitHandler<InterestFormData> = (data) => {
    create(data)
      .unwrap()
      .then(() => {
        enqueueSnackbar("Created successfully", { variant: "success" });
      })
      .catch((err) => errorHandler(err));
  };

  return (
    <form>
      <Box
        sx={{
          display: "flex",
          justifyContent: "space-between",
          alignItems: "start",
          p: 1,
        }}
      >
        <Controller
          name={"name"}
          control={control2}
          rules={{
            required: "Name is required.",
            minLength: { value: 1, message: "Name needs at least 1 letter." },
          }}
          render={({ field }) => (
            <TextField
              error={errors2.name ? true : false}
              helperText={errors2?.name?.message}
              id="outlined-required"
              label="Give it a title"
              size="small"
              {...field}
            />
          )}
        />
        <Box>
          {isUpdating ? (
            <CircularProgress />
          ) : (
            <Button
              disabled={!isDirty2 || !isValid2}
              onClick={handleFormSubmit2(onSubmit)}
              variant="contained"
            >
              Create
            </Button>
          )}
        </Box>
      </Box>
    </form>
  );
};

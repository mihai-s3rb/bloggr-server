import { Box, TextField, Button, CircularProgress } from "@mui/material";
import TComment from "../../types/models/TComment";
import TPost from "../../types/models/TPost";
import { useAddPostCommentMutation } from "../../features/api/bloggrApiSlice";
import { Controller, SubmitHandler, useForm } from "react-hook-form";
import { enqueueSnackbar } from "notistack";
import { errorHandler } from "../../helpers/error-handler";

type AddComment = {
  post: TPost | undefined;
};

const AddComment = (props: AddComment) => {
  const { id } = props.post ? props.post : { id: 0 };

  const {
    control,
    reset,
    formState: { isValid, isDirty, errors },
    handleSubmit: handleFormSubmit,
  } = useForm<Partial<TComment>>({
    mode: "onChange",
    defaultValues: {
      content: "",
    },
  });

  const [addComment, { isLoading: isUpdating }] = useAddPostCommentMutation();

  const onSubmit: SubmitHandler<Partial<TComment>> = (data) => {
    const body = {
      id: id,
      content: data.content,
    };
    addComment(body)
      .unwrap()
      .then((payload) => {
        enqueueSnackbar("Comment added successfully", { variant: "success" });
        reset();
      })
      .catch((err) => {
        errorHandler(err);
      });
  };

  return (
    <form onSubmit={handleFormSubmit(onSubmit)}>
      <Box>
        <Box>
          <Controller
            name={"content"}
            control={control}
            rules={{ required: "Content is required." }}
            render={({ field }) => (
              <TextField
                sx={{ width: "100%" }}
                error={errors.content ? true : false}
                helperText={errors?.content?.message}
                id="outlined-multiline-static"
                multiline
                rows={2}
                placeholder="Leave a comment"
                {...field}
              />
            )}
          />
        </Box>
        <Box>
          {isUpdating ? (
            <CircularProgress />
          ) : (
            <Button
              sx={{ m: 1 }}
              disabled={!isValid || !isDirty}
              variant="contained"
              type="submit"
            >
              Comment
            </Button>
          )}
        </Box>
      </Box>
    </form>
  );
};

export default AddComment;

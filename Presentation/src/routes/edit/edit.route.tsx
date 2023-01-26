import { Button, CircularProgress, TextField, Typography } from "@mui/material";
import CustomEditor from "../../components/editor/editor";
import { RawDraftContentState } from "draft-js";
import { useState } from "react";
import { useUpdatePostMutation } from "../../features/api/bloggrApiSlice";
import { Controller, SubmitHandler, useForm } from "react-hook-form";
import TPost from "../../types/models/TPost";
import InterestsSelector, {
  TSelectInterest,
} from "../../components/interests-selector/interests-selector";
import { useLocation, useNavigate } from "react-router-dom";
import { enqueueSnackbar } from "notistack";
import { errorHandler } from "../../helpers/error-handler";
import TInterest from "../../types/models/TInterest";

type EditFormData = {
  title: string;
  caption: string;
  captionImageUrl: string;
  interests: TSelectInterest[];
};

export const Edit = () => {
  const location = useLocation();
  const navigate = useNavigate();

  const post: TPost = location?.state?.post;

  const {
    control,
    reset,
    watch,
    formState: { errors, isValid, isDirty },
    handleSubmit: handleFormSubmit,
  } = useForm<EditFormData>({
    mode: "onBlur",
    defaultValues: {
      title: post?.title,
      caption: post?.caption,
      captionImageUrl: post?.captionImageUrl,
      interests: [
        ...post?.interests.map((interest: TInterest) => ({
          label: interest.name,
          value: interest.id,
        })),
      ],
    },
  });

  const [editPost, { isLoading: isUpdating }] = useUpdatePostMutation();
  const [rawState, setRawState] = useState<RawDraftContentState | undefined>();

  if (!Boolean(post)) return <p>Could not load post</p>;

  const onSubmit: SubmitHandler<EditFormData> = (data) => {
    const interests = data.interests.map(
      (el: { value: number; label: string }) => {
        return {
          id: el.value,
          name: el.label,
        };
      }
    );
    const body = {
      ...data,
      id: post.id,
      content: JSON.stringify(rawState),
      interests: interests,
    };
    console.log(body);
    editPost(body)
      .unwrap()
      .then((payload: TPost) => {
        enqueueSnackbar("Post edited succesfully! :D", { variant: "success" });
        navigate(`/post/${payload.id}`);
      })
      .catch((err) => errorHandler(err));
  };

  return (
    <>
      <Typography variant="h5" gutterBottom>
        Make it the best version 🤩
      </Typography>
      <form onSubmit={handleFormSubmit(onSubmit)}>
        <Controller
          name={"title"}
          control={control}
          rules={{
            required: "This field is required.",
            minLength: {
              value: 10,
              message: "Title is too short.",
            },
          }}
          render={({ field }) => (
            <TextField
              sx={{ width: "50%", marginBottom: "1rem" }}
              error={errors.title ? true : false}
              helperText={errors?.title?.message}
              id="outlined-required"
              label="Give it a title"
              {...field}
            />
          )}
        />

        <CustomEditor setRawState={setRawState} content={post.content} />

        <Controller
          name={"caption"}
          control={control}
          rules={{
            required: "This field is required.",
            minLength: {
              value: 60,
              message: "Caption too short.",
            },
          }}
          render={({ field }) => (
            <TextField
              multiline
              rows={2}
              error={errors.caption ? true : false}
              helperText={errors?.caption?.message}
              sx={{ width: "100%", "margin-bottom": "1rem" }}
              id="outlined-required"
              label="Short catch description"
              {...field}
            />
          )}
        />

        <Controller
          name={"captionImageUrl"}
          control={control}
          rules={{
            required: "This field is required.",
            pattern: {
              value:
                /(http(s)?:\/\/.)?(www\.)?[-a-zA-Z0-9@:%._\+~#=]{2,256}\.[a-z]{2,6}\b([-a-zA-Z0-9@:%_\+.~#?&//=]*)/,
              message: "Value is not an URL",
            },
          }}
          render={({ field }) => (
            <TextField
              error={errors.captionImageUrl ? true : false}
              helperText={errors?.captionImageUrl?.message}
              sx={{ width: "100%", "margin-bottom": "1rem" }}
              id="outlined-required"
              label="Thumbnail Url"
              {...field}
            />
          )}
        />

        <Controller
          name={"interests"}
          control={control}
          render={({ field }) => <InterestsSelector {...field} />}
        />
        {isUpdating ? (
          <CircularProgress />
        ) : (
          <Button
            disabled={!isDirty || !isValid}
            sx={{ mt: 1 }}
            type="submit"
            variant="contained"
          >
            Update
          </Button>
        )}
      </form>
    </>
  );
};

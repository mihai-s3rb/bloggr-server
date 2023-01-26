import {
  Avatar,
  Box,
  Button,
  Chip,
  CircularProgress,
  FormControl,
  IconButton,
  InputLabel,
  MenuItem,
  Select,
  TextField,
  Typography,
} from "@mui/material";
import { MuiChipsInput, MuiChipsInputChip } from "mui-chips-input";
import { useEffect, useState } from "react";
import TInterest from "../../types/models/TInterest";
import InterestsSelector, {
  TSelectInterest,
} from "../../components/interests-selector/interests-selector";
import { Controller, SubmitHandler, useForm } from "react-hook-form";
import { useAppSelector } from "../../features/hooks";
import { useUpdateUserMutation } from "../../features/api/bloggrApiSlice";
import { enqueueSnackbar } from "notistack";
import { errorHandler } from "../../helpers/error-handler";
import { store } from "../../store";
import { setUser } from "../../slices/user-slice";
import TUser from "../../types/models/TUser";

import "./profileRoute.scss";
import { PhotoCamera } from "@mui/icons-material";

type UpdateFormData = {
  email: string;
  firstName: string;
  lastName: string;
  bio: string;
  birthDate: string;
  interests: TSelectInterest[];
  profile?: FileList;
  background?: FileList;
};

export const Profile = () => {
  const userData = useAppSelector((state) => state.user);
  const {
    control,
    register,
    formState: { isDirty, isValid, errors },
    handleSubmit: handleFormSubmit,
  } = useForm<UpdateFormData>({
    mode: "onBlur",
    defaultValues: {
      ...userData.user,
      birthDate: userData?.user?.birthDate.slice(0, 10),
      interests: userData?.user?.interests.map((interest: TInterest) => {
        return {
          value: interest.id,
          label: interest.name,
        };
      }),
    },
  });
  const [update, { isLoading: isUpdating }] = useUpdateUserMutation();
  const onSubmit: SubmitHandler<UpdateFormData> = ({ interests, ...data }) => {
    const formData = new FormData();
    for (const [key, value] of Object.entries(data)) {
      console.log(key);
      if (typeof value != "object") formData.append(key, value);
    }
    if (data.profile && data.profile.length > 0) {
      formData.append("profile", data.profile[0]);
    }
    if (data.background && data.background.length > 0) {
      formData.append("background", data.background[0]);
    }

    formData.append(
      "interests",
      JSON.stringify(
        interests.map((interest: TSelectInterest) => {
          return {
            id: interest.value,
            name: interest.label,
          };
        })
      )
    );
    console.log(formData.get("interests"));
    update(formData)
      .unwrap()
      .then((payload: TUser) => {
        store.dispatch(setUser(payload));
        enqueueSnackbar("Updated data successfully", { variant: "success" });
      })
      .catch((err) => errorHandler(err));
  };
  const [images, setImages] = useState({ profile: "", background: "" });

  const handleImageChange = (event: React.ChangeEvent<HTMLInputElement>) => {
    if (event.target.files != null) {
      setImages((prevState) => {
        return {
          ...prevState,
          [event.target.name]: URL.createObjectURL(event.target.files![0]),
        };
      });
    }
  };

  useEffect(() => {
    console.log(images);
  }, [images]);

  return (
    <Box
      sx={{
        "& .MuiTextField-root": { width: "100%", mb: 1, mt: 1 },
      }}
    >
      <form onSubmit={handleFormSubmit(onSubmit)}>
        <Box sx={{ position: "relative", width: "100%", height: "300px" }}>
          <Box
            sx={{
              backgroundSize: "cover",
              backgroundImage: `url(${
                images.background
                  ? images.background
                  : userData?.user?.backgroundImageUrl
              })`,
              backgroundPosition: "center center",
              height: "100%",
              width: "100%",
            }}
            className="profile-container"
          >
            <Box className="overlay">
              <IconButton
                color="primary"
                aria-label="upload picture"
                component="label"
              >
                <input
                  {...register("background", {
                    onChange: (e) => handleImageChange(e),
                  })}
                  hidden
                  accept="image/*"
                  type="file"
                  name="background"
                />
                <PhotoCamera />
              </IconButton>
            </Box>
          </Box>
          <Box
            sx={{
              position: "absolute",
              top: "10px",
              left: "10px",
              width: "100px",
              height: "100px",
              borderRadius: "50%",
              backgroundSize: "cover",
              backgroundImage: `url(${
                images.profile
                  ? images.profile
                  : userData?.user?.profileImageUrl
              })`,
            }}
            className="profile-container"
          >
            <Box sx={{ borderRadius: "50%" }} className="overlay">
              <IconButton
                color="primary"
                aria-label="upload picture"
                component="label"
              >
                <input
                  {...register("profile", {
                    onChange: (e) => handleImageChange(e),
                  })}
                  hidden
                  accept="image/*"
                  type="file"
                  name="profile"
                />
                <PhotoCamera />
              </IconButton>
            </Box>
          </Box>
        </Box>
        <Typography variant="h4" sx={{ mt: 2, mb: 2, ml: 2 }}>
          Hello there,{" "}
          {`${userData?.user?.firstName} ${userData?.user?.lastName}`} 👋
        </Typography>
        <Controller
          name={"email"}
          control={control}
          rules={{
            required: "Email is required.",
            minLength: {
              value: 6,
              message: "Email not valid",
            },
            pattern: {
              value: /^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$/,
              message: "Email not valid",
            },
          }}
          render={({ field }) => (
            <TextField
              sx={{ width: "100%" }}
              error={errors.email ? true : false}
              helperText={errors?.email?.message}
              id="outlined-multiline-static"
              placeholder="Email"
              {...field}
            />
          )}
        />
        <Controller
          name={"firstName"}
          control={control}
          rules={{
            required: "This field is required.",
            minLength: {
              value: 3,
              message: "First name should be at least 1 letter.",
            },
          }}
          render={({ field }) => (
            <TextField
              sx={{ width: "100%" }}
              error={errors.firstName ? true : false}
              helperText={errors?.firstName?.message}
              id="outlined-multiline-static"
              placeholder="Firstname"
              {...field}
            />
          )}
        />
        <Controller
          name={"lastName"}
          control={control}
          rules={{
            required: "This field is required.",
            minLength: {
              value: 3,
              message: "Last name needs to be at least 3 chars long",
            },
          }}
          render={({ field }) => (
            <TextField
              sx={{ width: "100%" }}
              error={errors.lastName ? true : false}
              helperText={errors?.lastName?.message}
              id="outlined-multiline-static"
              placeholder="Lastname"
              {...field}
            />
          )}
        />
        <Controller
          name={"bio"}
          control={control}
          rules={{
            required: "This field is required.",
            minLength: {
              value: 10,
              message: "Bio needs to be at least 10 chars long",
            },
            maxLength: {
              value: 400,
              message: "Bio needs to have max 200 chars",
            },
          }}
          render={({ field }) => (
            <TextField
              sx={{ width: "100%" }}
              error={errors.bio ? true : false}
              helperText={errors?.bio?.message}
              id="outlined-multiline-static"
              placeholder="Bio"
              multiline
              rows={2}
              {...field}
            />
          )}
        />
        <Controller
          name={"birthDate"}
          control={control}
          rules={{
            required: "Birth date is required.",
            validate: (v) => {
              const date = new Date(v);
              const currentDate = new Date();
              return (
                currentDate.getFullYear() - 13 >= date.getFullYear() ||
                "You're too young"
              );
            },
          }}
          render={({ field }) => (
            <TextField
              id="date"
              label="Birthdate"
              type="date"
              error={errors.birthDate ? true : false}
              helperText={errors?.birthDate?.message}
              sx={{ width: 220 }}
              InputLabelProps={{
                shrink: true,
              }}
              {...field}
            />
          )}
        />
        <Typography>Interests:</Typography>
        <Controller
          name={"interests"}
          control={control}
          rules={{ required: "Interests are required." }}
          render={({ field }) => <InterestsSelector {...field} />}
        />
        <div>
          {isUpdating ? (
            <CircularProgress />
          ) : (
            <Button
              variant="contained"
              type="submit"
              disabled={!isDirty || !isValid}
            >
              Update
            </Button>
          )}
        </div>
      </form>
    </Box>
  );
};

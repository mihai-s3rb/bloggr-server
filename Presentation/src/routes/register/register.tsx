import {
  TextField,
  Button,
  Typography,
  TextFieldProps,
  CircularProgress,
} from "@mui/material";
import Box from "@mui/system/Box";
import { Controller, SubmitHandler, useForm } from "react-hook-form";
import { store } from "../../store";
import { setUser } from "../../slices/user-slice";
import Select from "react-select";
import { redirect, useNavigate } from "react-router-dom";

import { Dayjs } from "dayjs";
import InterestsSelector, {
  TSelectInterest,
} from "../../components/interests-selector/interests-selector";
import { useRegisterMutation } from "../../features/api/bloggrApiSlice";
import SetCredentials from "../../auth/handler";
import TInterest from "../../types/models/TInterest";
import TError from "../../types/models/TError";
import { useSnackbar } from "notistack";
import { errorHandler } from "../../helpers/error-handler";
export type TRegister = {
  username: string;
  email: string;
  password: string;
  firstName: string;
  lastName: string;
  bio: string;
  birthDate: string;
  interests?: TInterest[];
};

export type RegisterFromData = {
  username: string;
  email: string;
  password: string;
  firstName: string;
  lastName: string;
  bio: string;
  birthDate: string;
  interests?: TSelectInterest[];
};

const Register = () => {
  const {
    control,
    reset,
    formState: { isValid, isDirty, errors },
    handleSubmit: handleFormSubmit,
  } = useForm<RegisterFromData>({
    mode: "onChange",
    defaultValues: {
      username: "",
      email: "",
      password: "",
      firstName: "",
      lastName: "",
      bio: "",
      birthDate: "",
      interests: [],
    },
  });
  const navigate = useNavigate();
  const { enqueueSnackbar, closeSnackbar } = useSnackbar();
  const [register, { isLoading: isUpdating }] = useRegisterMutation();

  const onSubmit: SubmitHandler<RegisterFromData> = (data) => {
    let interests: TInterest[] = [];
    if (data.interests)
      interests = data?.interests?.map((interest: TSelectInterest) => {
        return {
          id: interest.value,
          name: interest.label,
        };
      });
    const body = {
      ...data,
      interests: interests,
    };
    console.log(body);
    register(body)
      .unwrap()
      .then((payload) => {
        SetCredentials(payload);
        console.log(payload);
        navigate("/");
      })
      .catch((err) => {
        console.log(err);
        errorHandler(err);
      });
  };
  return (
    <Box
      sx={{
        width: "100%",
        height: "100vh",
        display: "flex",
        textAlign: "center",
        justifyContent: "center",
        alignItems: "center",
        background: "linear-gradient(to left, #ffefba, #ffffff)",
      }}
    >
      <Box sx={{ maxWidth: "500px" }}>
        <Typography variant="h5">Nice to meet you</Typography>
        <form onSubmit={handleFormSubmit(onSubmit)}>
          <Controller
            name={"username"}
            control={control}
            rules={{
              required: "This field is required.",
              minLength: {
                value: 3,
                message: "Username needs to be at least 3 chars long",
              },
            }}
            render={({ field }) => (
              <TextField
                sx={{ width: "100%", maxWidth: "400px", mt: 1, mb: 1 }}
                error={errors.username ? true : false}
                helperText={errors?.username?.message}
                id="outlined-multiline-static"
                label="Username"
                {...field}
              />
            )}
          />
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
                sx={{ width: "100%", maxWidth: "400px", mt: 1, mb: 1 }}
                error={errors.email ? true : false}
                helperText={errors?.email?.message}
                id="outlined-multiline-static"
                label="Email"
                {...field}
              />
            )}
          />
          <Controller
            name={"password"}
            control={control}
            rules={{
              required: "This field is required.",
              minLength: {
                value: 6,
                message: "Password needs to be at least 6 chars long",
              },
              pattern: {
                value:
                  /^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[!@#$%^&*])[A-Za-z\d@$!%*?&]{8,}$/,
                message:
                  "Password needs at least one uppercase and lowercase letter, one number and one symbol.",
              },
            }}
            render={({ field }) => (
              <TextField
                sx={{ width: "100%", maxWidth: "400px", mt: 1, mb: 1 }}
                error={errors.password ? true : false}
                helperText={errors?.password?.message}
                id="outlined-multiline-static"
                label="Password"
                type="password"
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
                message: "First name too short.",
              },
            }}
            render={({ field }) => (
              <TextField
                sx={{ width: "100%", maxWidth: "400px", mt: 1, mb: 1 }}
                error={errors.firstName ? true : false}
                helperText={errors?.firstName?.message}
                id="outlined-multiline-static"
                label="Firstname"
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
                message: "Last name too short.",
              },
            }}
            render={({ field }) => (
              <TextField
                sx={{ width: "100%", maxWidth: "400px", mt: 1, mb: 1 }}
                error={errors.lastName ? true : false}
                helperText={errors?.lastName?.message}
                id="outlined-multiline-static"
                label="Lastname"
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
                message: "Bio too short.",
              },
              maxLength: {
                value: 200,
                message: "Bio too long.",
              },
            }}
            render={({ field }) => (
              <TextField
                sx={{ width: "100%", maxWidth: "400px", mt: 1, mb: 1 }}
                error={errors.bio ? true : false}
                helperText={errors?.bio?.message}
                id="outlined-multiline-static"
                label="Bio"
                multiline
                rows={4}
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
                sx={{ width: "100%", maxWidth: "400px", mt: 1, mb: 1 }}
                InputLabelProps={{
                  shrink: true,
                }}
                {...field}
              />
            )}
          />
          <Box sx={{ maxWidth: "400px", margin: "0 auto" }}>
            <Controller
              name={"interests"}
              control={control}
              render={({ field }) => <InterestsSelector {...field} />}
            />
          </Box>
          {isUpdating ? (
            <CircularProgress />
          ) : (
            <Button
              disabled={!isDirty || !isValid}
              variant="contained"
              type="submit"
            >
              Register
            </Button>
          )}
        </form>
      </Box>
    </Box>
  );
};

export default Register;

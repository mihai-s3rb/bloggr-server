import { TextField, Button, Typography, CircularProgress } from "@mui/material";
import Box from "@mui/system/Box";
import { Controller, SubmitHandler, useForm } from "react-hook-form";
import { store } from "../../store";
import { setUser } from "../../slices/user-slice";
import { redirect, useNavigate } from "react-router-dom";
import { useLoginMutation } from "../../features/api/bloggrApiSlice";
import SetCredentials from "../../auth/handler";
import { useSnackbar } from "notistack";
import { errorHandler } from "../../helpers/error-handler";

export type TLogin = {
  userName: string;
  password: string;
};

const Login = () => {
  const {
    control,
    reset,
    formState: { isValid, isDirty, errors },
    handleSubmit: handleFormSubmit,
  } = useForm<TLogin>({
    mode: "onChange",
    defaultValues: { userName: "", password: "" },
  });
  const navigate = useNavigate();
  const { enqueueSnackbar, closeSnackbar } = useSnackbar();
  const [login, { isLoading: isUpdating }] = useLoginMutation();

  const onSubmit: SubmitHandler<TLogin> = (data) => {
    login(data)
      .unwrap()
      .then((payload) => {
        SetCredentials(payload);
        enqueueSnackbar("Welcome back, log in succesful!", {
          variant: "success",
        });
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
        justifyContent: "center",
        alignItems: "center",
        background: "linear-gradient(to left, #ffefba, #ffffff)",
      }}
    >
      <Box sx={{ textAlign: "center" }}>
        <Typography variant="h5">Hello there, </Typography>
        <form onSubmit={handleFormSubmit(onSubmit)}>
          <Controller
            name={"userName"}
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
                error={errors.userName ? true : false}
                helperText={errors?.userName?.message}
                id="outlined-multiline-static"
                label={"Username"}
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
          <Typography>
            Don't have an account? Register{" "}
            <Button
              id="basic-button"
              aria-haspopup="true"
              type="button"
              onClick={() => navigate("/register")}
            >
              here
            </Button>
          </Typography>
          {isUpdating ? (
            <CircularProgress />
          ) : (
            <Button
              disabled={!isDirty || !isValid}
              variant="contained"
              type="submit"
            >
              Login
            </Button>
          )}
        </form>
      </Box>
    </Box>
  );
};

export default Login;

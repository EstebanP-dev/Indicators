import { SnackbarUtilities, loadAbort } from "../utilities";
import { useEffect, useState } from "react";
import { AccountInfo, Response } from "../models";
import { useNavigate } from "react-router-dom";
import {
  PrivateRoutes,
  PublicRoutes,
  endpoints,
  enviroment,
} from "../enviroments";
import { useDispatch } from "react-redux";
import { createAccountInfo } from "../redux/states/accountInfo";
import LockOutlinedIcon from "@mui/icons-material/LockOutlined";
import { useAxiosApi } from "../hooks";
import { AuthMessages, ExceptionMessages } from "../messaging";
import {
  Avatar,
  Box,
  Button,
  Checkbox,
  FormControlLabel,
  Grid,
  Link,
  Paper,
  TextField,
  Typography,
  useTheme,
} from "@mui/material";
import { Loading } from "../components";

const validateEmail = (email: string) => {
  var isValid: boolean = email !== "";

  return isValid;
};

const validatePassword = (password: string) => {
  var isValid: boolean = password !== "";

  return isValid;
};

const Login = () => {
  const theme = useTheme();
  const abortController = loadAbort();
  const dispatch = useDispatch();
  const navigate = useNavigate();
  const [username, setUsername] = useState("");
  const [password, setPassword] = useState("");
  const [loading, setLoading] = useState(false);
  const { callEndpoint, postService, getService } = useAxiosApi(
    abortController,
    dispatch,
    navigate
  );

  const handleSubmit = async () => {
    setLoading(true);
    try {
      if (validateEmail(username) && validatePassword(password)) {
        const result: Response<AccountInfo> = await postService<AccountInfo>(
          enviroment.api + endpoints.auth.login,
          {
            username: username,
            password: password,
          }
        );
        setLoading(false);

        if (result.data !== undefined) {
          SnackbarUtilities.success(AuthMessages.LOG_IN);
          dispatch(createAccountInfo(result.data));
          navigate(PrivateRoutes.HOME, {
            replace: true,
          });
        } else if (result.error !== undefined) {
          SnackbarUtilities.error(result.error.title);
        } else {
          console.log(result.error);
          SnackbarUtilities.error(ExceptionMessages.UNKNOWN);
        }
      } else {
        SnackbarUtilities.warning(AuthMessages.EMPTY_FIELDS);
      }
    } catch (exception: any) {
      setLoading(false);
      SnackbarUtilities.error(ExceptionMessages.UNKNOWN);
      console.log(`ERROR ${JSON.stringify(exception)}`);
    }
  };

  // const validateLogin = async () => {
  //   setLoading(true);
  //   try {
  //     let result = await getServiceTest<string>(endpoints.api.pingPong);
  //     setLoading(false);

  //     if (result.status === HttpStatusCode.Ok) {
  //       SnackbarUtilities.info(AuthMessages.ALREADY_LOG_IN);
  //       navigate(PrivateRoutes.HOME);
  //     }
  //   } catch (err) {
  //     setLoading(false);
  //     SnackbarUtilities.error(ExceptionMessages.UNKNOWN);
  //   }
  // };

  useEffect(() => {
    callEndpoint<string>(
      getService(endpoints.api.pingPong),
      undefined,
      undefined,
      () => {
        SnackbarUtilities.info(AuthMessages.ALREADY_LOG_IN);
        navigate(PrivateRoutes.HOME, {
          replace: true,
        });
      }
    );
  }, []);

  function Copyright(props: any) {
    return (
      <Typography
        variant="body2"
        color="text.secondary"
        align="center"
        {...props}
      >
        {"Copyright © "}
        <Link color="inherit" href="https://www.usbmed.edu.co/">
          Esteban Navia
        </Link>{" "}
        {new Date().getFullYear()}
        {"."}
      </Typography>
    );
  }

  return (
    <>
      {loading ? (
        <Loading canCancel={false} cancelTitle={undefined} message="Cargando" />
      ) : (
        <Grid container component="main" sx={{ height: "100vh" }}>
          <Grid
            item
            xs={false}
            sm={4}
            md={7}
            sx={{
              backgroundImage:
                "url(https://images.pexels.com/photos/14333207/pexels-photo-14333207.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1)",
              backgroundRepeat: "no-repeat",
              backgroundColor: theme.palette.primary.main,
              backgroundSize: "cover",
              backgroundPosition: "center",
            }}
          />
          <Grid
            item
            xs={12}
            sm={8}
            md={5}
            component={Paper}
            elevation={6}
            square
          >
            <Box
              sx={{
                my: 8,
                mx: 4,
                display: "flex",
                flexDirection: "column",
                alignItems: "center",
              }}
            >
              <Avatar sx={{ m: 1, bgcolor: "secondary.main" }}>
                <LockOutlinedIcon />
              </Avatar>
              <Typography component="h1" variant="h5">
                Sign in
              </Typography>
              <Box
                component="form"
                noValidate
                onSubmit={handleSubmit}
                sx={{ mt: 1 }}
              >
                <TextField
                  margin="normal"
                  required
                  fullWidth
                  id="username"
                  label="Correo electrónico"
                  name="username"
                  autoComplete="email"
                  color="secondary"
                  autoFocus
                  onChange={(e) => setUsername(e.target.value)}
                />
                <TextField
                  margin="normal"
                  required
                  fullWidth
                  color="secondary"
                  name="password"
                  label="Contraseña"
                  type="password"
                  id="password"
                  autoComplete="current-password"
                  onChange={(e) => setPassword(e.target.value)}
                  sx={{
                    "& input": {
                      "::-ms-reveal": {
                        filter: "invert(100%)",
                      },
                    },
                  }}
                />
                <FormControlLabel
                  control={<Checkbox value="remember" color="secondary" />}
                  label="Recuerdame"
                />
                <Button
                  type="submit"
                  fullWidth
                  variant="contained"
                  color="secondary"
                  sx={{ mt: 3, mb: 2, padding: "1rem" }}
                >
                  Iniciar Sesión
                </Button>
                <Grid container>
                  <Grid item xs>
                    <Link href="#" variant="body2" color="secondary">
                      ¿Olvidaste la contraseña?
                    </Link>
                  </Grid>
                  <Grid item>
                    {
                      "Si no tienes una cuenta, comunicate con un administrador."
                    }
                  </Grid>
                </Grid>
                <Copyright sx={{ mt: 5 }} />
              </Box>
            </Box>
          </Grid>
        </Grid>
      )}
    </>
  );
};

export default Login;

import "./login.scss"
import { SnackbarUtilities, loadAbort } from "../../utilities";
import { useEffect, useState } from "react";
import { AccountInfo, AuthMessages, ExceptionMessages, Response } from "../../models";
import { useNavigate } from "react-router-dom";
import { PrivateRoutes, endpoints, enviroment } from "../../enviroments";
import { useDispatch } from "react-redux";
import { createAccountInfo } from "../../redux/states/accountInfo";
import { Loading } from "../../components";
import { useAxiosApi } from "../../hooks";

const validateEmail = (email: string) => {
  var isValid: boolean = email !== "";

  return isValid;
}

const validatePassword = (password: string) => {
  var isValid: boolean = password !== "";

  return isValid;
}

const Login = () => {
  const dispatch = useDispatch();
  const navigate = useNavigate();
  const abortController: AbortController = loadAbort();
  const [username, setUsername] = useState('');
  const [password, setPassword] = useState('');
  const { loading, callEndpoint, cancelEndpoint, postService, getService } = useAxiosApi(abortController);

  const login = async () => {
    try {
      if (validateEmail(username) && validatePassword(password)){
        const result: Response<AccountInfo> = await callEndpoint(
          postService<AccountInfo>(
            enviroment.api + endpoints.auth.login,
            {
              username: username,
              password: password
            }
          )
        );
  
        if (result.data !== undefined) {
          SnackbarUtilities.success(AuthMessages.LOG_IN);
          dispatch(createAccountInfo(result.data));
          navigate(PrivateRoutes.HOME);
        }
        else if (result.error !== undefined) {
          SnackbarUtilities.error(result.error.title);
        }
        else{
          console.log(result.error);
          SnackbarUtilities.error(ExceptionMessages.UNKNOWN);
        }
      }
    }
    catch (exception: any) {
      SnackbarUtilities.error(ExceptionMessages.UNKNOWN);
      console.log(`ERROR ${JSON.stringify(exception)}`);
    }
  }

  useEffect(() => {
    callEndpoint(getService<string>(
      enviroment.api + endpoints.api.pingPong
    ))
    .then((result) => {
      if (result.status === 200 && result.data !== undefined) {
        SnackbarUtilities.info(AuthMessages.ALREADY_LOG_IN);
        navigate(PrivateRoutes.HOME);
      }
    });

    return () => {
      cancelEndpoint();
    }
  }, [])

  return (
    <>
      <div className="login">
        <div className="sideImage">
          <div className="signature">
            <h1>Juan Esteban Navia Perez</h1>
            <p>Ingenieria de Datos y Software</p>
          </div>
          <img src="medellin.jpg" alt="Medellin picture" />
        </div>
        <div className="information">
          <h1>USBMED</h1>
          <div className="form">
            <div className="title">
              <h3>Inicio de Sesión</h3>
              <p>¡Bienvenido! Ingresa tus credenciales para acceder.</p>
            </div>
            <div className="fields">
              <input
                type="email"
                placeholder="Correo electrónico"
                onChange={(e) => setUsername(e.target.value)} />
              <input
                type="password"
                placeholder="Contraseña"
                onChange={(p) => setPassword(p.target.value)} />
            </div>
            <div className="credentials">
              <div className="check">
                <input type="checkbox" />
                <p>Remember me</p>
              </div>
              <p className="forgotPassword">Olvidé mi Contraseña</p>
            </div>
            <div className="submit" onClick={login}>
              <button>Iniciar Sesión</button>
            </div>
          </div>
          <div className="informationFooter">
            <p>Si no tienes una cuenta, comunicate con un administrador.</p>
          </div>
        </div>
      </div>
      {
        loading
        ? <Loading canCancel={false} cancelTitle={undefined} message="Cargando"/>
        : <></>
      }
    </>
  )
}

export default Login;
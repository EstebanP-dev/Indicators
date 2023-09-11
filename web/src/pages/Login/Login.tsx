import "./login.scss"
import { SnackbarUtilities } from "../../utilities";
import { useEffect, useState } from "react";
import { AccountInfo } from "../../models";
import { useFetchAndLoad } from "../../hooks";
import { loginService } from "../../services";
import { useNavigate } from "react-router-dom";
import { PrivateRoutes } from "../../enviroments";
import { useDispatch, useSelector } from "react-redux";
import { createAccountInfo } from "../../redux/states/accountInfo";
import { AppStore } from "../../redux/store";
import { Loading } from "../../components";

const validateEmail = (email: string) => {
  var isValid: boolean = email !== "";

  return isValid;
}

const validatePassword = (password: string) => {
  var isValid: boolean = password !== "";

  return isValid;
}

const Login = () => {
  const accountInfoStored: AccountInfo = useSelector((store: AppStore) => store.accountInfo);
  const dispatch = useDispatch();
  const navigate = useNavigate();
  const { loading, callEndpoint } = useFetchAndLoad();
  const [accountInfo, setAccountInfo ] = useState<AccountInfo | undefined>(undefined);
  const [error, setError ] = useState<Error | undefined>(undefined);
  const [username, setUsername] = useState('');
  const [password, setPassword] = useState('');

  const login = async () => {
    try {
      if(validateEmail(username) && validatePassword(password)) {
        const result = await callEndpoint(loginService(username, password));
        setAccountInfo(result.data);
        setError(result.data);
  
        if (accountInfo !== undefined) {
          SnackbarUtilities.success("Sesión iniciada.");
          dispatch(createAccountInfo(accountInfo));
          navigate(PrivateRoutes.HOME);
        }
        else if (error !== undefined) {
          SnackbarUtilities.error(error.message);
        }
        else{
          console.log(accountInfo, error, result);
          SnackbarUtilities.error("Nothing happend.");
        }
      }
      else {
        SnackbarUtilities.warning('El email o la contraseña no pueden estar vacios.');
      }
    } catch (error) {
      SnackbarUtilities.error("Something was wrong. Try again later.");
      console.log(`ERROR ${JSON.stringify(error)}`);
    }
  }

  useEffect(() => {
    if (accountInfoStored.token !== ''){
      SnackbarUtilities.warning('Ya has iniciado sesión.');
      navigate(PrivateRoutes.HOME)
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
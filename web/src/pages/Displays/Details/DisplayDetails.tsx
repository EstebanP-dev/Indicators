import { useNavigate, useParams } from "react-router-dom";
import { Single } from "../../../components"
import { SnackbarUtilities, loadAbort } from "../../../utilities";
import { useAxiosApi } from "../../../hooks";
import { useDispatch } from "react-redux";
import { AuthMessages, Display, ErrorOr, ExceptionMessages } from "../../../models";
import { useEffect, useState } from "react";
import { PublicRoutes, endpoints, enviroment } from "../../../enviroments";
import { resetAccountInfo } from "../../../redux/states/accountInfo";

const DisplayDetails = () => {
  const abortController: AbortController = loadAbort();
  const dispatch = useDispatch();
  const navigate = useNavigate();
  const { id } = useParams();
  const [singleData, setSingleData] = useState<Display | undefined>(undefined);
  const [error, setError ] = useState<ErrorOr | undefined>(undefined);
  const { loading, callEndpoint, getService } = useAxiosApi(abortController);

  useEffect(() => {
    const loadSingleData = async () => {
      try {
        let result = await callEndpoint(getService<Display>(
          enviroment.api + endpoints.displays.getUserById(Number.parseInt(id ?? '0'))
        ));

        if (result.status === 401)
        {
          SnackbarUtilities.info(AuthMessages.EXPIRE_SESION);
          dispatch(resetAccountInfo());
          navigate(PublicRoutes.LOGIN, {
            replace: true
          });
        }
        else if (result.error !== undefined) {
          SnackbarUtilities.error(result.error.title);
        }
        else if (result.data !== undefined) {
          setSingleData(result.data);
        }
        else {
          console.log(JSON.stringify(result));
          SnackbarUtilities.error(ExceptionMessages.UNKNOWN);
        }
      }
      catch (err: any) {
        console.log(err);
        throw err;
      }
    }

    loadSingleData()
    .catch((_) => {
      setError(JSON.parse(JSON.stringify({
        status: 500,
        title: ExceptionMessages.UNKNOWN
      })));

    });
  }, [])


  return (
    <div className="details">
      <Single id={id ?? ''} title={singleData?.name ?? ''} info={
        {
          "Id": singleData?.id ?? '',
          "Nombre": singleData?.name ?? ''
        }
      }/>
    </div>
  )
}

export default DisplayDetails
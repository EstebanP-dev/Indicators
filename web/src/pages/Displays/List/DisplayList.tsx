import { useEffect, useState } from "react";
import "./displaylist.scss"
import { Add, DataTable, Loading } from "../../../components";
import { GridColDef } from "@mui/x-data-grid";
import { Display, Pagination, ErrorOr, AuthMessages, ExceptionMessages, Response } from "../../../models";
import { useDispatch } from "react-redux";
import { useNavigate } from "react-router-dom";
import { PublicRoutes, endpoints, enviroment } from "../../../enviroments";
import { resetAccountInfo } from "../../../redux/states/accountInfo";
import { SnackbarUtilities, loadAbort } from "../../../utilities";
import { useAxiosApi } from "../../../hooks";

const pageSize: number = 100;

const columns: GridColDef[] =
[
  {
    field: "id",
    headerName: "#",
    width: 100,
  },
  {
    field: "name",
    headerName: "Nomber",
    width: 1000,
    type: "string"
  },
];

const DisplayList = () => {
  const page: number = 0;
  const dispatch = useDispatch();
  const navigate = useNavigate();
  const abortController: AbortController = loadAbort();
  const [open, setOpen] = useState(false);
  const [pagination, setPagination ] = useState<Pagination<Display> | undefined>(undefined);
  const [error, setError ] = useState<ErrorOr | undefined>(undefined);
  const { loading, callEndpoint, getService } = useAxiosApi(abortController);

  useEffect(() => {
    const loadPagination = async () => {
      try {
        let result: Response<Pagination<Display>> = await callEndpoint(getService<Pagination<Display>>(
          enviroment.api + endpoints.displays.getUsersPagination(page, pageSize, null)
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
          setError(result.error);
        }
        else if (result.data !== undefined) {
          setPagination(result.data);
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

    loadPagination()
    .catch((_) => {
      setError(JSON.parse(JSON.stringify({
        status: 500,
        title: ExceptionMessages.UNKNOWN
      })));

    });
  }, []);

  return (
    <>
      {
        loading
        ? ( 
          // <Loading canCancel={false} cancelTitle={undefined} message="Cargando" />
          <span>Cargando...</span>
        )
        : (
          <>
            {
              pagination === undefined ? (
                <div><h3>{error?.title}</h3></div>
              ) : (
                <>
                  <div className="list" >
                      <div className="info">
                          <h1>Representaciones Visuales</h1>
                          <button onClick={() => setOpen(true)}>Nueva Representación Visual</button>
                      </div>
                      <DataTable
                        columns={columns}
                        rows = {pagination?.response}
                        page = {pagination?.currentPage}
                        slug = "displays"
                        pageSize = {pagination?.pageSize}
                        totalPages = {pagination?.totalPages}
                      />
                      {open && <Add setOpen={setOpen} slug="displays" columns={columns}/>}
                  </div>
                </>
              )
            }
          </>
        )
      }
    </>
  );
}

export default DisplayList
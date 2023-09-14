import { useEffect, useState } from "react";
import { Add, DataTable, Header } from "../components";
import { GridColDef } from "@mui/x-data-grid";
import { Display, Pagination, ErrorOr, Response } from "../models";
import { useDispatch } from "react-redux";
import { useNavigate } from "react-router-dom";
import { PublicRoutes, endpoints, enviroment } from "../enviroments";
import { resetAccountInfo } from "../redux/states/accountInfo";
import { SnackbarUtilities, loadAbort } from "../utilities";
import { useAxiosApi } from "../hooks";
import { AuthMessages, ExceptionMessages } from "../messaging";
import { hideLoading, showLoading } from "../redux/states/loadingData";
import { Alert, Box } from "@mui/material";

const pageSize: number = 100;

const columns: GridColDef[] =
[
  {
    field: "id",
    headerName: "#",
    flex: .5,
  },
  {
    field: "name",
    headerName: "Nombre",
    flex: 3,
    type: "string",
    editable: true
  },
];

const Displays = () => {
  const page: number = 0;
  const dispatch = useDispatch();
  const navigate = useNavigate();
  const abortController: AbortController = loadAbort();
  const [open, setOpen] = useState(false);
  const [pagination, setPagination ] = useState<Pagination<Display> | undefined>(undefined);
  const [error, setError ] = useState<ErrorOr | undefined>(undefined);
  const [refresh, setRefresh] = useState(false);
  const { callEndpoint, getService } = useAxiosApi(abortController);

  useEffect(() => {
    const loadPagination = async () => {
      dispatch(showLoading());
      try {
        let result: Response<Pagination<Display>> = await callEndpoint(getService<Pagination<Display>>(
          enviroment.api + endpoints.displays.pagination(page, pageSize, null)
        ));

        dispatch(hideLoading());
  
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
        dispatch(hideLoading());
        console.log(err);
        SnackbarUtilities.error(ExceptionMessages.UNKNOWN);
  
        throw err;
      }
    }

    loadPagination()
    .then((res) => console.log(res))
    .catch((_) => {
      setError(JSON.parse(JSON.stringify({
        status: 500,
        title: ExceptionMessages.UNKNOWN
      })));
    });
  }, [open, refresh]);

  return (
    <Box m="1.5rem 2.5rem">
      <Header title="Representaciones Visuales" subtitle="Tipos de visualizaciones." />
      <Box mt="40px" height="75vph">
        {
          pagination === undefined
          ? (
            <Alert severity="error">{error?.title}</Alert>
          )
          : (
            <DataTable
              columns={columns}
              rows = {pagination?.response}
              page = {pagination?.currentPage}
              slug = "displays"
              pageSize = {pagination?.pageSize}
              totalPages = {pagination?.totalPages}
              setRefresh={setRefresh}
            />
          )
        }
      </Box>
      {open && <Add setOpen={setOpen} slug="displays" columns={columns}/>}
    </Box>
  );

  // <>
  //     {
  //       pagination === undefined ? (
  //         <div><h3>{error?.title}</h3></div>
  //       ) : (
  //         <>
  //           <div className="list" >
  //               <div className="info">
  //                   <h1>Representaciones Visuales</h1>
  //                   <button onClick={() => setOpen(true)}>Nueva Representación Visual</button>
  //               </div>
  //               <DataTable
  //                 columns={columns}
  //                 rows = {pagination?.response}
  //                 page = {pagination?.currentPage}
  //                 slug = "displays"
  //                 pageSize = {pagination?.pageSize}
  //                 totalPages = {pagination?.totalPages}
  //                 setRefresh={setRefresh}
  //               />
  //               {open && <Add setOpen={setOpen} slug="displays" columns={columns}/>}
  //           </div>
  //         </>
  //       )
  //     }
  //   </>
}

export default Displays
import { useEffect, useState } from "react";
import "./displaylist.scss"
import { Add, DataTable } from "../../../components";
import { GridColDef } from "@mui/x-data-grid";
import { useFetchAndLoad } from "../../../hooks";
import { getDisplaysPagination } from "../../../services";
import { AccountInfo, Display, Pagination, ErrorOr, AuthMessages, ExceptionMessages } from "../../../models";
import { useDispatch, useSelector } from "react-redux";
import { AppStore } from "../../../redux/store";
import { useNavigate } from "react-router-dom";
import { PublicRoutes } from "../../../enviroments";
import { resetAccountInfo } from "../../../redux/states/accountInfo";
import { SnackbarUtilities } from "../../../utilities";

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
  const page: number = 0
  const accountInfo: AccountInfo = useSelector((store: AppStore) => store.accountInfo);
  const dispatch = useDispatch();
  const navigate = useNavigate();
  const [open, setOpen] = useState(false);
  const { loading, callEndpoint } = useFetchAndLoad();
  const [pagination, setPagination ] = useState<Pagination<Display> | undefined>(undefined);
  const [error, setError ] = useState<ErrorOr | undefined>(undefined);

  useEffect(() => {
    callEndpoint(getDisplaysPagination(page, pageSize, accountInfo.token))
    .then((res) => {
      setPagination(res.data)
    })
    .catch((exception) => {
      setError(JSON.parse(JSON.stringify(exception?.response?.data)));
      
      if (error !== undefined) {
        SnackbarUtilities.error(error.title);
      }
      else if (exception?.response?.status === 401) {
        SnackbarUtilities.info(AuthMessages.EXPIRE_SESION);
        dispatch(resetAccountInfo())
        navigate(PublicRoutes.LOGIN, {
          replace: true
        });
      }else {
        SnackbarUtilities.error(ExceptionMessages.UNKNOWN);
      }
      console.log(JSON.stringify(error));
    });
  }, []);

  return (
    <>
      {
        loading ? (
          <div><h3>LOADING...</h3></div>
        ) : (
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
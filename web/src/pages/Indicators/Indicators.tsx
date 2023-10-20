import { GridColDef } from "@mui/x-data-grid";
import { useEffect, useState } from "react";
import { AccountInfo, ErrorOr, IndicatorPaginationResponse, Pagination } from "../../models";
import { useAxiosApi, usePagination } from "../../hooks";
import { Config, endpoints } from "../../enviroments";
import { useDispatch, useSelector } from "react-redux";
import { useNavigate } from "react-router-dom";
import { AppStore } from "../../redux/store";
import { loadAbort } from "../../utilities";
import { Body, DataTable } from "../../components";
import { Alert, Box } from "@mui/material";

const columns: GridColDef[] = [
  {
    field: "id",
    headerName: "#",
    flex: 0.5,
  },
  {
    field: "code",
    headerName: "Código",
    flex: 1,
  },
  {
    field: "name",
    headerName: "Nombre",
    flex: 1,
  },
];

const addColumns: GridColDef[] = [
  {
    field: "code",
    headerName: "Código",
    type: "string"
  },
  {
    field: "name",
    headerName: "Nombre",
    type: "string"
  },
  {
    field: "objective",
    headerName: "Objetivo",
    type: "string"
  },
  {
    field: "scope",
    headerName: "Alcance",
    type: "string"
  },
  {
    field: "formula",
    headerName: "Formula",
    type: "string"
  },
  {
    field: "goal",
    headerName: "Meta",
    type: "string"
  },
  {
    field: "indicatorTypeId",
    headerName: "Tipo de Indicador",
    type: "select"
  },
  {
    field: "measurementUnitId",
    headerName: "Unidad de Medición",
    type: "select"
  },
  {
    field: "meaningId",
    headerName: "Sentido",
    type: "select"
  },
  {
    field: "frequencyId",
    headerName: "Frecuencia",
    type: "select"
  },
  {
    field: "displays",
    headerName: "Representaciones Visuales",
    type: "multipleSelect"
  },
  {
    field: "variables",
    headerName: "Variables",
    type: "multipleForm"
  },
  {
    field: "sources",
    headerName: "Fuentes",
    type: "multipleSelect"
  },
  {
    field: "actors",
    headerName: "Actores",
    type: "multipleSelect"
  },
];

const SLUG = "Indicators";

export const Indicators = () => {
  const accountInfo: AccountInfo = useSelector((store: AppStore) => store.accountInfo);
  const dispatch = useDispatch();
  const navigate = useNavigate();
  const abortController = loadAbort();
  const [pagination, setPagination] = useState<Pagination<IndicatorsPaginationResponse> | undefined>(undefined);
  const [error, setError] = useState<ErrorOr | undefined>(undefined);
  const [refresh, setRefresh] = useState(false);
  const { callEndpoint, getService } = useAxiosApi(
    abortController,
    navigate,
    dispatch
  );
  const { pushQuery } = usePagination();
  const [page, setPage] = useState<number>(Config.PAGINATION.DEFAULT_PAGE);
  const [rows, setRows] = useState<number>(Config.PAGINATION.DEFAULT_ROWS);
  const [totalPages, setTotalPages] = useState<number>(Config.PAGINATION.DEFAULT_TOTALPAGES);

  const fetchData = () => {
    callEndpoint<Pagination<IndicatorPaginationResponse>>(
      getService(
        endpoints.api
          .pagination(
            SLUG.toLowerCase(),
            page,
            rows,
            null)
      ),
      setPagination,
      setError,
      undefined,
      (result) => {
        setTotalPages(result.totalPages ?? Config.PAGINATION.DEFAULT_TOTALPAGES);
      }
    )
  }

  useEffect(() => {
    fetchData();
    pushQuery(page, rows);
  }, [page, rows]);

  useEffect(() => {
    setRefresh(false);
    fetchData();
    return () => {
      abortController.abort();
    };
  }, [refresh]);
  
  return (
    <Body
      isEditing={false}
      title="Indicadores"
      slug={SLUG.toLowerCase()}
      showAdd={true}
      setRefresh={setRefresh}
      columns={addColumns}
    >
      <Box mt="40px" height="75vph">
        {pagination === undefined ? (
          <Alert severity="error">{error?.title}</Alert>
        ) : (
          <DataTable
            columns={columns}
            rows={pagination?.response}
            page={page}
            slug={SLUG.toLowerCase()}
            pageSize={rows}
            totalRows={pagination?.totalRows}
            totalPages={totalPages}
            rowsValues={Config.PAGINATION.ROWS_VALUES}
            setRefresh={setRefresh}
            setPage={setPage}
            setPageSize={setRows}
            editOutList={true}
          />
        )}
      </Box>
    </Body>
  );
}

export default Indicators
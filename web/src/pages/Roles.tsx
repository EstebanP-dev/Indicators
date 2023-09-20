import { useEffect, useState } from "react";
import { Body, DataTable } from "../components";
import { GridColDef } from "@mui/x-data-grid";
import { Role, Pagination, ErrorOr } from "../models";
import { useDispatch } from "react-redux";
import { useNavigate } from "react-router-dom";
import { Config, endpoints } from "../enviroments";
import { useAxiosApi, usePagination } from "../hooks";
import { Alert, Box } from "@mui/material";
import { loadAbort } from "../utilities";

const columns: GridColDef[] = [
  {
    field: "id",
    headerName: "#",
    flex: 0.5,
  },
  {
    field: "name",
    headerName: "Nombre",
    flex: 3,
    type: "string",
    editable: true,
  },
];

const SLUG = "Roles";

const Roles = () => {
  const dispatch = useDispatch();
  const navigate = useNavigate();
  const abortController = loadAbort();
  const [pagination, setPagination] = useState<Pagination<Role> | undefined>(
    undefined
  );
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

  const fetchData = async () => {
    await callEndpoint<Pagination<Role>>(
      getService(
        endpoints.api.pagination(SLUG.toLowerCase(), page, rows, null)
      ),
      setPagination,
      setError,
      undefined,
      (result) => {
        setTotalPages(result.totalPages ?? Config.PAGINATION.DEFAULT_TOTALPAGES);
      }
    );
  };

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
      title="Representaciones Visuales"
      subtitle="Tipos de visualización de datos."
      slug={SLUG.toLowerCase()}
      showAdd={true}
      setRefresh={setRefresh}
      columns={columns}
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
          />
        )}
      </Box>
    </Body>
  );
};

export default Roles;

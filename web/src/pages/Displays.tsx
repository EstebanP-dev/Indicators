import { useEffect, useState } from "react";
import { Body, DataTable } from "../components";
import { GridColDef } from "@mui/x-data-grid";
import { Display, Pagination, ErrorOr } from "../models";
import { useDispatch } from "react-redux";
import { useNavigate, useParams } from "react-router-dom";
import { endpoints } from "../enviroments";
import { useAxiosApi } from "../hooks";
import { Alert, Box } from "@mui/material";
import { loadAbort } from "../utilities";

const pageSize: number = 100;

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

const SLUG = "Displays";

const Displays = () => {
  const dispatch = useDispatch();
  const navigate = useNavigate();
  const abortController = loadAbort();
  const [pagination, setPagination] = useState<Pagination<Display> | undefined>(
    undefined
  );
  const [error, setError] = useState<ErrorOr | undefined>(undefined);
  const [refresh, setRefresh] = useState(false);
  const { callEndpoint, getService } = useAxiosApi(
    abortController,
    dispatch,
    navigate
  );
  const [page, setPage] = useState(0);
  const [rows, setRows] = useState(100);

  useEffect(() => {
    window.history.pushState(
      null,
      "",
      `?page=${page}&size=${rows}`
    )
    callEndpoint<Pagination<Display>>(
      getService(
        endpoints.api.pagination(SLUG.toLowerCase(), page, rows, null)
      ),
      setPagination,
      setError,
      undefined,
      undefined
    );
  }, [refresh, page, rows]);

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
            page={pagination?.currentPage}
            slug={SLUG.toLowerCase()}
            pageSize={pagination?.pageSize}
            totalPages={pagination?.totalPages}
            setRefresh={setRefresh}
          />
        )}
      </Box>
    </Body>
  );
};

export default Displays;
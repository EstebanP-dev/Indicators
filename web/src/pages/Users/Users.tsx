import { useEffect, useState } from 'react';
import { Body, DataTable } from '../../components';
import { GridColDef } from '@mui/x-data-grid';
import {
  Pagination,
  ErrorOr,
  AccountInfo,
  UserPaginationResponse,
} from '../../models';
import { useDispatch, useSelector } from 'react-redux';
import { useNavigate } from 'react-router-dom';
import { Config, endpoints } from '../../enviroments';
import { useAxiosApi, usePagination } from '../../hooks';
import { Alert, Box } from '@mui/material';
import { loadAbort } from '../../utilities';
import { AppStore } from '../../redux/store';
import { userAdapter } from '../../adapters';

const columns: GridColDef[] = [
  {
    field: 'id',
    headerName: 'Correo Electronico',
    flex: 3,
  },
  {
    field: 'isVerified',
    headerName: 'Verificado',
    flex: 0.5,
    type: 'boolean',
  },
];

const addColumns: GridColDef[] = [
  {
    field: 'email',
    headerName: 'Correo Electronico',
    type: 'string',
  },
  {
    field: 'password',
    headerName: 'Contraseña',
    type: 'string',
  },
  {
    field: 'roles',
    headerName: 'Roles',
    type: 'multipleSelect',
  },
];

const SLUG = 'Users';

const Users = () => {
  const accountInfo: AccountInfo = useSelector(
    (store: AppStore) => store.accountInfo
  );
  const dispatch = useDispatch();
  const navigate = useNavigate();
  const abortController = loadAbort();
  const { userPaginationAdapter } = userAdapter;
  const [pagination, setPagination] = useState<Pagination<any> | undefined>(
    undefined
  );
  const [error, setError] = useState<ErrorOr | undefined>(undefined);
  const [refresh, setRefresh] = useState(false);
  const { callEndpoint, getService } = useAxiosApi(
    abortController,
    navigate,
    dispatch
  );
  const { createUserFromAddAdapter } = userAdapter;
  const { pushQuery } = usePagination();
  const [page, setPage] = useState<number>(Config.PAGINATION.DEFAULT_PAGE);
  const [rows, setRows] = useState<number>(Config.PAGINATION.DEFAULT_ROWS);
  const [totalPages, setTotalPages] = useState<number>(
    Config.PAGINATION.DEFAULT_TOTALPAGES
  );

  const fetchData = async () => {
    callEndpoint<Pagination<UserPaginationResponse>>(
      getService(
        endpoints.api.pagination(
          SLUG.toLowerCase(),
          page,
          rows,
          accountInfo.user.email
        )
      ),
      setPagination,
      setError,
      undefined,
      (result) => {
        setTotalPages(
          result.totalPages ?? Config.PAGINATION.DEFAULT_TOTALPAGES
        );
        setPagination(userPaginationAdapter(result));
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
      isEditing={false}
      title='Usuarios'
      slug={SLUG.toLowerCase()}
      showAdd={true}
      setRefresh={setRefresh}
      columns={addColumns}
      selectionDataUrl='/roles/all'
      addAdapterFuction={(data) => createUserFromAddAdapter(data)}
    >
      <Box mt='40px' height='75vph'>
        {pagination === undefined ? (
          <Alert severity='error'>{error?.title}</Alert>
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
};

export default Users;

import { useDispatch, useSelector } from "react-redux";
import { SnackbarUtilities, loadAbort, urlUtility } from "../../utilities";
import { AppStore } from "../../redux/store";
import { useAxiosApi } from "../../hooks";
import { endpoints } from "../../enviroments";
import { useNavigate } from "react-router-dom";
import {
  DataGridMessages,
} from "../../messaging";
import { useEffect, useState } from "react";
import { Box, IconButton } from "@mui/material";
import { Save, Delete, Visibility } from "@mui/icons-material";

const DataActions = ({
  params,
  slug,
  rowId,
  setRowId,
  before,
  setBefore,
  setRefresh,
  editOutList = false,
}: any) => {
  const loading: boolean = useSelector((store: AppStore) => store.loadingData);
  const abortController = loadAbort();
  const dispatch = useDispatch();
  const navigate = useNavigate();
  const [success, setSuccess] = useState(false);
  const { callEndpoint, putService, deleteService } = useAxiosApi(
    abortController,
    dispatch,
    navigate
  );
  const { urlEnconde } = urlUtility;

  const handleDelete = async () => {
    callEndpoint<any>(
      deleteService(endpoints.api.id(slug, params.row.id)),
      undefined,
      undefined,
      undefined,
      () => {
        setRowId(null);
        setBefore(null);
        SnackbarUtilities.success(DataGridMessages.DELETE_SUCCESS);
        setSuccess(true);
        setRefresh(true);
      }
    );
  };

  const handleUpdate = () => {
    if (canEditTheRow()) {
      callEndpoint<any>(
        putService(endpoints.api.id(slug, params.row.id), params.row),
        undefined,
        undefined,
        undefined,
        () => {
          setRowId(null);
          setBefore(null);
          SnackbarUtilities.success(DataGridMessages.UPDATE_SUCCESS);
          setSuccess(true);
          setRefresh(true);
        }
      );
    }
  };

  const handleLink = () => {
    let id: string = urlEnconde(params.row.id);

    console.log(id);

    navigate(
      `/${slug}/${id}`
    )
  }

  function canEditTheRow(): boolean {
    return (
      params.id === rowId &&
      !loading &&
      before !== null &&
      before !== params.row
    );
  }

  useEffect(() => {
    if (rowId === params.id && success) setSuccess(false);
  }, [rowId]);

  return (
    <Box display="flex" flexDirection="row" gap=".5rem">
      {
        editOutList ? (
          <IconButton
            color="success"
            onClick={handleLink}
          >
            <Visibility />
          </IconButton>
        ) : (
          <IconButton
            color="success"
            disabled={!canEditTheRow()}
            onClick={handleUpdate}
          >
            <Save />
          </IconButton>
        )
      }
      <IconButton color="error" onClick={handleDelete}>
        <Delete />
      </IconButton>
    </Box>
  );
};

export default DataActions;

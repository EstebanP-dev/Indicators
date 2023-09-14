import { useDispatch, useSelector } from "react-redux";
import { SnackbarUtilities, loadAbort } from "../../utilities";
import { AppStore } from "../../redux/store";
import { hideLoading, showLoading } from "../../redux/states/loadingData";
import { useAxiosApi } from "../../hooks";
import { PublicRoutes, enviroment } from "../../enviroments";
import { useNavigate } from "react-router-dom";
import { resetAccountInfo } from "../../redux/states/accountInfo";
import { AuthMessages, DataGridMessages, ExceptionMessages } from "../../messaging";
import { useEffect, useState } from "react";

const DataActions = ({ params, slug, rowId, setRowId, before, setBefore, setRefresh }: any) => {
    const loading: boolean = useSelector((store: AppStore) => store.loadingData);;
    const dispatch = useDispatch();
    const navigate = useNavigate();
    const abortController: AbortController = loadAbort();
    const [success, setSuccess] = useState(false);
    const { callEndpoint, putService, deleteService } = useAxiosApi(abortController);
    
    const handleDelete = async () =>
    {
        dispatch(showLoading());
        try {
            let result = await callEndpoint(deleteService(
                enviroment.api + `/${slug}/${params.row.id}`
            ));

            dispatch(hideLoading());

            if (result.status === 401) {
                SnackbarUtilities.info(AuthMessages.EXPIRE_SESION);
                dispatch(resetAccountInfo());
                navigate(PublicRoutes.LOGIN, {
                    replace: true
                });
            }
            else if (!!result.error) {
                SnackbarUtilities.error(result.error.title);
            }
            else if (result.data !== undefined) {
                setRowId(null);
                setBefore(null);
                SnackbarUtilities.success(DataGridMessages.DELETE_SUCCESS);
                setSuccess(true);
                setRefresh(true);
            }
            else {
                SnackbarUtilities.error(ExceptionMessages.UNKNOWN);
                console.log(result);
            }
        }
        catch (err) {
            dispatch(hideLoading());
            setRowId(null);
            setBefore(null);
            console.log(err);
            SnackbarUtilities.error(ExceptionMessages.UNKNOWN);
        }
        finally {
            setRowId(null);
            setBefore(null);
        }
    }

    const handleUpdate = async () => {
        dispatch(showLoading());
        try {
            if (canEditTheRow()) {
                let result = await callEndpoint(putService(
                    enviroment.api + `/${slug}/${params.row.id}`,
                    params.row
                ));

                dispatch(hideLoading());

                if (result.status === 401) {
                    SnackbarUtilities.info(AuthMessages.EXPIRE_SESION);
                    dispatch(resetAccountInfo());
                    navigate(PublicRoutes.LOGIN, {
                        replace: true
                    });
                }
                else if (!!result.error) {
                    SnackbarUtilities.error(result.error.title);
                }
                else if (result.data !== undefined) {
                    setRowId(null);
                    setBefore(null);
                    SnackbarUtilities.success(DataGridMessages.UPDATE_SUCCESS);
                    setSuccess(true);
                }
                else {
                    SnackbarUtilities.error(ExceptionMessages.UNKNOWN);
                    console.log(result);
                }
            }
        }
        catch (err) {
            dispatch(hideLoading());
            console.log(err);
            SnackbarUtilities.error(ExceptionMessages.UNKNOWN);
            setRowId(null);
            setBefore(null);
        }
    }

    function canEditTheRow(): boolean {
        return (params.id === rowId && !loading)
        && (before !== null && before !== params.row)
    }

    useEffect(() => {
        if (rowId === params.id && success) setSuccess(false)
    }, [rowId]);

    return (
        <>
            <div className="action">
                <div className="save" onClick={handleUpdate}>
                    <img src={
                        canEditTheRow()
                        ? "save.svg"
                        : "save-disable.svg"
                    } alt="save icon" />
                </div>
                <div className="delete" onClick={handleDelete}>
                    <img src="delete.svg" alt="delete icon" />
                </div>
            </div>
        </>
    )
}

export default DataActions

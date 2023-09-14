import { GridColDef } from "@mui/x-data-grid"
import "./add.scss"
import React, { useState } from "react"
import { SnackbarUtilities, loadAbort } from "../../utilities";
import { useAxiosApi } from "../../hooks";
import { PublicRoutes, enviroment } from "../../enviroments";
import { useDispatch } from "react-redux";
import { useNavigate } from "react-router-dom";
import { hideLoading, showLoading } from "../../redux/states/loadingData";
import { resetAccountInfo } from "../../redux/states/accountInfo";
import { AuthMessages, ExceptionMessages } from "../../messaging";

type Props =
{
    slug: string,
    columns: GridColDef[],
    setOpen: React.Dispatch<React.SetStateAction<boolean>>;
}

const Add = (props: Props) => {
    const dispatch = useDispatch();
    const navigate = useNavigate();
    const abortController: AbortController = loadAbort();
    const { callEndpoint, postService } = useAxiosApi(abortController)
    const [data, setData] = useState<any>({})

    const handleSubmit = async (e: React.FormEvent<HTMLFormElement>) => {
        e.preventDefault();
        dispatch(showLoading());
        
        try {
            let result = await callEndpoint(postService(
                enviroment.api + `/${props.slug}`,
                data
            ));
            dispatch(hideLoading());

            if (result.status === 401) {
                dispatch(resetAccountInfo())
                SnackbarUtilities.info(AuthMessages.EXPIRE_SESION);
                navigate(PublicRoutes.LOGIN, {
                    replace: true
                });
            }
            else if (!!result.error) {
                SnackbarUtilities.error(result.error.title);
            }
            else if (result.data !== undefined) {
                SnackbarUtilities.success
            }
            else{
                SnackbarUtilities.error(ExceptionMessages.UNKNOWN);
                console.log(result);
            }
        }
        catch (err) {
            dispatch(hideLoading());
            console.log(err);
            SnackbarUtilities.error(ExceptionMessages.UNKNOWN);
        }
        finally {
            setData({});
            props.setOpen(false);
        }
    }

  return (
    <div className="add">
        <div className="modal">
            <span className="close" onClick={() => props.setOpen(false)}>
                X
            </span>
            <h1>Agregar nuevo {props.slug}</h1>
            <form onSubmit={handleSubmit}>
                {props.columns
                    .filter((item) =>
                        item.field !== "id" &&
                        item.field !== "img")
                    .map((column) => (
                        <div className="item" key={column.field}>
                            <label key={'label-' + column.field}>{column.headerName}</label>
                            <input
                                key={'input-' + column.field}
                                type={column.type}
                                placeholder={column.field}
                                onChange={(e) => {
                                    setData({
                                        ...data,
                                        [column.field]: e?.target?.value
                                    });
                                }}
                            />
                        </div>
                    ))
                }
                <button>Send</button>
            </form>
        </div>
    </div>
  )
}

export default Add
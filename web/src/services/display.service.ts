import axios from "axios";
import { loadAbort } from "../utilities"
import { AccountInfo, Display, Pagination } from "../models";
import { endpoints, enviroment } from "../enviroments";
import { AppStore } from "../redux/store";
import { useSelector } from "react-redux";

export const getDisplaysPagination = (page: number, rows: number) => {
    const accountInfo: AccountInfo = useSelector((store: AppStore) => store.accountInfo);
    const controller = loadAbort();

    return {
        call: axios.create({
            baseURL: enviroment.api + endpoints.displays.getUsersPagination(page, rows, null),
            headers: {
                'Content-Type': 'application/json',
                'Accept': 'application/json',
                'Authorization': `Bearer ${accountInfo.token}`
            },
            method: "GET"
        }
        ),
        controller: controller
    }
}
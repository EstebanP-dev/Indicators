import axios from "axios";
import { loadAbort } from "../utilities"
import { Display, Pagination } from "../models";
import { endpoints, enviroment } from "../enviroments";

export const getDisplaysPagination = (page: number, rows: number, token: string) => {
    const controller = loadAbort();
    const url = enviroment.api + endpoints.displays.getUsersPagination(page, rows, null);

    return {
        call: axios.get<Pagination<Display>>(
            url,
            {
                signal: controller.signal,
                headers: {
                    'Content-Type': 'application/json',
                    'Accept': 'application/json',
                    'Authorization': `Bearer ${token}`
                }
            }
        ),
        controller: controller
    }
}
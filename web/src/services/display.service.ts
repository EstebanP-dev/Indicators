import axios from "axios";
import { loadAbort } from "../utilities"
import { Display, Pagination } from "../models";
import { endpoints, enviroment } from "../enviroments";

export const getDisplaysPaginationService = (page: number, rows: number, token: string) => {
    const controller = loadAbort();
    const url = enviroment.api + endpoints.api.pagination("displays", page, rows, null);

    return {
        call: axios.get<Pagination<Display>>(
            url,
            {
                signal: controller.signal,
                
            }
        ),
        controller: controller
    }
}
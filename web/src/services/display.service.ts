import axios from "axios";
import { loadAbort } from "../utilities"
import { Display, Pagination } from "../models";
import { endpoints, enviroment } from "../enviroments";

export const getDisplaysPagination = (page: number, rows: number) =>{
    const controller = loadAbort();

    return {
        call: axios.get<Pagination<Display>>(
            enviroment.api + endpoints.displays.getUsersPagination(page, rows, null),
            {
                signal: controller.signal
            }),
        controller: controller
    }
}
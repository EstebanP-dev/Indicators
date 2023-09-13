import axios from "axios";
import { endpoints, enviroment } from "../enviroments";
import { loadAbort } from "../utilities";

export const getPingPongService = (token: string) => {
    const controller = loadAbort();
    const url = enviroment.api + endpoints.api.pingPong;

    return {
        call: axios.get(
            url,
            {
                signal: controller.signal,
                headers: {
                    'Content-Type': 'application/json',
                    'Accept': 'application/json',
                    'Authorization': `Bearer ${token}`
                }
            }
        )
    }
}
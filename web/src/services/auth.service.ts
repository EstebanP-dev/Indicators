import axios from "axios";
import { loadAbort } from "../utilities";
import { endpoints, enviroment } from "../enviroments";

export const loginService = (username: string, password: string) => {
    const controller = loadAbort();

    return {
        call: axios.post(
            enviroment.api + endpoints.auth.login,
            {
                username: username,
                password: password
            }
        ),
        controller: controller
    }
}
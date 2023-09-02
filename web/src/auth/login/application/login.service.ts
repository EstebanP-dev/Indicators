import { IResult, endpoints, enviroment } from "../../../shared";
import { Credentials } from "../domain";

const url = enviroment.api + endpoints.auth.login;

export const loginService = (credentials : Credentials) : Promise<IResult<string>> =>
{
    return fetch(url,
        {
            method: "POST",
            body: JSON.stringify(credentials)
        })
    .then(response => response.json());
}
import { endpoints, enviroment, parameterizedString } from "../../shared";

const url = enviroment.api + endpoints.users.getUserByEmail;

export const getUserByEmailService = (email: string) =>
{
    return fetch(parameterizedString(url, email)).then((response) => response.json());
}
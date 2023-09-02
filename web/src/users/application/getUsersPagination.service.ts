import { UserPaginationResponse } from ".";
import { IResult, endpoints, enviroment } from "../../shared";
import { IPagination } from "../../shared/domain/primitives/IPagination";

export const getUsersPaginationService = (page: number, rows: number) : Promise<IResult<IPagination<UserPaginationResponse>>> =>
{
    return fetch(enviroment.api + endpoints.users.getUsersPagination(page, rows))
        .then((response) => response.json());
}
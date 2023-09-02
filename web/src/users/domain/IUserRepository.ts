import { IResult } from "../../shared/domain/primitives/IResult";
import { UserResponse } from "./UserReponse";

export interface IUserRepository {
    getByEmail(email: string) : Promise<IResult<UserResponse>>;
}
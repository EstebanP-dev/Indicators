import { enviroment } from "../../shared/domain/enviroment";
import { IResult } from "../../shared/domain/primitives/IResult";
import { IUserRepository } from "../domain/IUserRepository";
import { UserResponse } from "../domain/UserReponse";

export class UserRepository implements IUserRepository {
    async getByEmail(email: string): Promise<IResult<UserResponse>> {

        const result: IResult<UserResponse> = await fetch(enviroment.api + "/users/" + email, {
            method: "GET"
        })
        .then((response) => response.json())
        .then((data) => JSON.parse(data))
        .catch((error) => console.log(error));

        return result;
    }

}
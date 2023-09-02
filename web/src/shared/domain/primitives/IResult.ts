import { IError } from ".";

export interface IResult<Value> {
    code: string,
    isSuccess: boolean,
    isFailure: boolean,
    errors: IError[] | undefined,
    value: Value | undefined,
}
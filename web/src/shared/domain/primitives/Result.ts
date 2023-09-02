import { IError, IResult } from ".";

export default class Result<Value> implements IResult<Value>
{
    code!: string;
    isSuccess!: boolean;
    isFailure!: boolean;
    errors: IError[] | undefined;
    value: Value | undefined;

    constructor(code: string, isSuccess: boolean, value?: Value, errors?: IError[])
    {
        this.code = code;
        this.isSuccess = isSuccess;
        this.isFailure = !isSuccess;
        this.value = value;
        this.errors = errors;
    }

    static None<Value>(): Result<Value>
    {
        return new Result<Value>("", false, undefined, undefined);
    }

    static Success<Value>(value: Value): IResult<Value>
    {
        return new Result<Value>("200", true, value, undefined);
    }

    static Failure<Value>(errors: IError[]): IResult<Value>
    {
        return new Result<Value>("404", false, undefined, errors);
    }
}
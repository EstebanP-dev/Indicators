import { IError } from ".";

export default class Error implements IError
{
    code!: string;
    message!: string;

    constructor(code: string, message: string)
    {
        this.code = code;
        this.message = message;
    }

    static None() : IError
    {
        return new Error("", "");
    }
}
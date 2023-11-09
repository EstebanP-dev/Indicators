import { ErrorOr } from ".";

export class Response<T> {
    status: number;
    data: T | undefined;
    error: ErrorOr | undefined;

    public constructor(status: number, data: T | undefined, error: ErrorOr | undefined) {
        this.status = status;
        this.data = data;
        this.error = error;
    }
}
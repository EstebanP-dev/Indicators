import { Role } from ".";

export interface User {
    email: string;
    roles: Role[]
}
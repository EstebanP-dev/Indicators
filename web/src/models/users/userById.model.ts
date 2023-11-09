import { Role } from "..";

export type UserById = {
    email: string;
    roles: Role[];
}
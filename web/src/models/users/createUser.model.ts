export type CreateUserRequest = {
    email: string,
    password: string,
    roles: number[],
}
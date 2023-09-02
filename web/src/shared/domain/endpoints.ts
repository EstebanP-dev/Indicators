export const endpoints =
{
    auth:
    {
        login: "/auth/login"
    },
    users:
    {
        getUsersPagination: (page: number, rows: number) => `/users?page=${page}&rows=${rows}`,
        getUserByEmail: '/users/%s',
    }
}
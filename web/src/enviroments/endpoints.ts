export const endpoints =
{
    api:
    {
        pingPong: "/ping"
    },
    auth:
    {
        login: "/auth/login"
    },
    users:
    {
        getUsersPagination: (page: number, rows: number) => `/users?page=${page}&rows=${rows}`,
        getUserById: '/users/%s',
    },
    displays:
    {
        getUsersPagination: (page: number, rows: number, exclude: string | null) =>
            `/displays?page=${page}&rows=${rows}${exclude === null
                ? ''
                : '&exclude=' + exclude}`,
        getUserById: (id: number) => `/displays/${id}`,
    }
}
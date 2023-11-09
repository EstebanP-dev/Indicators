export const endpoints =
{
    api:
    {
        pingPong: "/ping",
        pagination: (slug: string, page: number, rows: number, exclude: string | null) =>
            `/${slug}?page=${page}&rows=${rows}${exclude === null
                ? ''
                : '&exclude=' + exclude}`,
        id: (slug: string, id: number) => `/${slug}/${id}`,
        slug: (slug: string) => `/${slug}`
    },
    auth:
    {
        login: "/auth/login"
    },
}
export const createDisplayAdapter = (display: any) => ({
    id: display.data.id,
    name: display.data.name
})

export const createDisplayPaginationAdapter = (pagination: any) => ({
    totalPages: pagination.data?.totalPages,
    currentPage: pagination.data?.currentPage,
    pageSize: pagination.data?.pageSize,
    response: pagination.data?.response,
})
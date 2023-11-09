export const createErrorAdapter = (error: any) => ({
    status: error.data?.status,
    message: error.data?.title,
})
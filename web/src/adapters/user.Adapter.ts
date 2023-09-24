import { CreateUserRequest, Pagination, UserPaginationResponse } from "../models";

const createUserFromAddAdapter = (data: any): CreateUserRequest => {
    return {
        email: data.id,
        password: data.password,
        roles: data.multiple
    };
}

const userPaginationAdapter = (pagination: Pagination<UserPaginationResponse>) => {
    return {
        totalRows: pagination.totalRows,
        totalPages: pagination.totalPages,
        currentPage: pagination.currentPage,
        pageSize: pagination.pageSize,
        response: pagination.response.map((user) => {
            return {
                id: user.email,
                isVerified: user.isVerified
            }
        }),
    };
}

export default {
    createUserFromAddAdapter,
    userPaginationAdapter
}
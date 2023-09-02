export interface IPagination<Value>
{
    totalPages: number,
    currentPage: number,
    pageSize: number,
    response: Value[]
}
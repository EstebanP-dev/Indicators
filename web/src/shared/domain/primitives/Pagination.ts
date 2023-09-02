import { IPagination } from ".";

export default class Pagination<Value> implements IPagination<Value>
{
    totalPages!: number;
    currentPage!: number;
    pageSize!: number;
    response!: Value[];

    constructor(totalPages: number, currentPage: number, pageSize: number, response: Value[])
    {
        this.totalPages = totalPages;
        this.currentPage = currentPage;
        this.pageSize = pageSize;
        this.response = response;
    }
}
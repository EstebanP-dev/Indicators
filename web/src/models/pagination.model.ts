export interface Pagination<TValue> {
    totalRows: number,
    totalPages: number;
    currentPage: number;
    pageSize: number;
    response: TValue[];
}
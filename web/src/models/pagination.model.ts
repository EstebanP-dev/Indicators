export interface Pagination<TValue> {
    totalPages: number;
    currentPage: number;
    pageSize: number;
    response: TValue[];
}
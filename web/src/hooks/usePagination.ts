import { Config } from "../enviroments";

const usePagination = () => {
    const isValidToChangePaginationValues = (
        page: number,
        rows: number,
        currentPage: number,
        currentRows: number,
        totalPages: number,
    ) => {
    return (page !== currentPage || rows !== currentRows) &&
        (page >= Config.PAGINATION.MINIMUM_PAGE && rows >= Config.PAGINATION.MINIMUM_ROWS) &&
        page <= (totalPages - 1) &&
        Config.PAGINATION.ROWS_VALUES.includes(rows)
    }

    const setUpPagination = (
        page: number,
        rows: number,
        currentPage: number,
        currentRows: number,
        totalPages: number,
        setPage: React.Dispatch<React.SetStateAction<number>>,
        setRows: React.Dispatch<React.SetStateAction<number>>,
    ) => {
        if (!isValidToChangePaginationValues(page, rows, currentPage, currentRows, totalPages)) return;
        setPage(page);
        setRows(rows);
    };

    const pushQuery = (page: number, rows: number) => {
        window.history.pushState(null, "", `?page=${page}&rows=${rows}`);
    };


    return {
        setUpPagination,
        pushQuery
    }
}

export default usePagination;
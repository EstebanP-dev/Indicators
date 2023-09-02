import { useEffect, useState } from "react";
import { Error, IResult, IPagination } from "../../../shared";
import { UserPaginationResponse, getUsersPaginationService } from "../../application";

export function useUsers (rows: number)
{
    const [totalPages, setTotalPages] = useState(0);
    const [page, setPage] = useState(0);
    const [canIncrese, setCanIncrese] = useState(true);
    const [result, setResult] = useState(
    {
        data: [] as UserPaginationResponse[],
        loading: true,
        failure: false,
        errors: [ Error.None() ]
    });

    const resetPage = () => setPage(0);
    const incresePage = () => setPage(canIncrese ? page + 1 : page);
    const getUsers = async () =>
    {
        if (!canIncrese)
        {
            return;
        }

        try
        {
            const serviceResult: IResult<IPagination<UserPaginationResponse>> = await getUsersPaginationService(page, rows);

            if (serviceResult.isSuccess)
            {
                let pagination = serviceResult.value!;
                
                setTotalPages(pagination.totalPages)
                setCanIncrese(page < (totalPages - 1));
                incresePage();
                
                setResult((prevState) =>
                (
                    {
                        ...prevState,
                        data: pagination?.response ?? [],
                        totalPages: totalPages
                    }
                ));

                console.log(page, "||", totalPages, "||")
                console.log(pagination)
            }
            else
            {
                setResult((prevState) =>
                (
                    {
                        ...prevState,
                        failure: true,
                        errors: serviceResult.errors ?? []
                    }
                ));
            }
        }
        catch (error)
        {
            let resultError: Error = new Error("500", JSON.stringify(error));

            setResult((prevState) =>
            (
                {
                    ...prevState,
                    failure: true,
                    errors: [ resultError ]
                }
            ));
        }
        finally
        {
            setResult((prevState) =>
            (
                {
                    ... prevState,
                    loading: false
                }
            ));
        }
    };

    const refresh = () => getUsers();

    useEffect(() =>
    {
        getUsers();
    }, [rows]);

    return {
        result,
        page,
        totalPages,
        canIncrese,
        resetPage,
        refresh
    }

}
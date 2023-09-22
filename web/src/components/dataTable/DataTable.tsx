import { DataGrid, GridColDef, GridRowIdGetter } from "@mui/x-data-grid";
import DataActions from "./DataActions";
import { useState } from "react";
import { useTheme } from "@mui/material";
import { usePagination } from "../../hooks";

type Props =
{
    columns: GridColDef[],
    rows: object[],
    page: number,
    pageSize: number,
    totalRows: number,
    totalPages: number,
    rowsValues: number[],
    slug: string,
    setRefresh: React.Dispatch<React.SetStateAction<boolean>>,
    setPage: React.Dispatch<React.SetStateAction<number>>,
    setPageSize: React.Dispatch<React.SetStateAction<number>>,
    editOnList?: boolean,
    getRowId?: GridRowIdGetter<any> | undefined,
}

const DataTable = (props: Props) =>
{
    const theme = useTheme();
    const [rowId, setRowId] = useState<any>(null);
    const [before, setBefore] = useState<any>(null);
    const { setUpPagination } = usePagination();

    const actionColumn: GridColDef =
    {
        field: "action",
        headerName: "Acciones",
        flex: .5,
        renderCell: (params) =>
        {
            return (
                <DataActions {...{params, ...props, rowId, setRowId, before, setBefore}}/>
            )
        }
    }

  return (
    <DataGrid
        rows={props.rows}
        columns={[...props.columns, actionColumn]}
        getRowId={props.getRowId}
        initialState=
        {
            {
                pagination:
                {
                    paginationModel:
                    {
                        page: props.page,
                        pageSize: props.pageSize,
                    }
                },
                sorting: {
                    sortModel: [{field:"id",sort:'asc'}],
                }
            }
        }
        onCellEditStart={(params) => setBefore(params.row)}
        onCellEditStop={(params) => setRowId(params.id)}
        rowCount={props.totalRows}
        pageSizeOptions={props.rowsValues}
        onPaginationModelChange={(model, _) => {
            setUpPagination(
                model.page,
                model.pageSize,
                props.page,
                props.pageSize,
                props.totalPages,
                props.setPage,
                props.setPageSize
            );
        }}
        paginationMode="server"
        checkboxSelection
        disableRowSelectionOnClick
        disableColumnFilter
        disableDensitySelector
        disableColumnSelector
        disableVirtualization
        disableColumnMenu
        disableEval
        sx={{
            "& .MuiDataGrid-root": {
                border: "none"
            },
            "& .MuiDataGrid-cell": {
                borderBottom: "none"
            },
            "& .MuiDataGrid-columnHeaders": {
                backgroundColor: theme.palette.background.paper,
                color: theme.palette.secondary.light,
                borderBottom: "none"
            },
            "& .MuiDataGrid-virtualScroller": {
                backgroundColor: theme.palette.background.paper,
            },
            "& .MuiDataGrid-footerContainer": {
                backgroundColor: theme.palette.background.paper,
                color: theme.palette.secondary.light,
                borderTop: "none"
            },
            "& .MuiDataGrid-toolbarContainer ": {
                flexDirection: "row-reverse",
                color: `${theme.palette.secondary.main} !i`
            },
            "& .MuiButton-text": {
                color: `${theme.palette.secondary.main} !i`
            }
        }}
        />
  )
}

export default DataTable
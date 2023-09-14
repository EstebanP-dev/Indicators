import { DataGrid, GridColDef, GridToolbar } from "@mui/x-data-grid";
import "./dataTable.scss"
import DataActions from "./DataActions";
import { useState } from "react";
import { Box, useTheme } from "@mui/material";
import { FlexBetween } from "..";

type Props =
{
    columns: GridColDef[],
    rows: object[],
    page: number,
    pageSize: number,
    totalPages: number,
    slug: string,
    setRefresh: React.Dispatch<boolean>,
}

const DataTable = (props: Props) =>
{
    const theme = useTheme();
    const [rowId, setRowId] = useState<any>(null);
    const [before, setBefore] = useState<any>(null);

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
        initialState=
        {
            {
                pagination:
                {
                    paginationModel:
                    {
                        page: props.page + 1,
                        pageSize: props.pageSize,
                    }
                },
            }
        }
        slots=
        {
            {
                toolbar: GridToolbar
            }
        }
        slotProps=
        {
            {
                toolbar:
                {
                    showQuickFilter: true,
                    quickFilterProps:
                    {
                        debounceMs: 500
                    }
                }
            }
        }
        onCellEditStart={(params) => setBefore(params.row)}
        onCellEditStop={(params) => setRowId(params.id)}
        rowCount={props.pageSize * props.totalPages}
        pageSizeOptions={[props.pageSize]}
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
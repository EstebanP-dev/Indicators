import { DataGrid, GridColDef, GridToolbar } from "@mui/x-data-grid";
import "./dataTable.scss"
import DataActions from "./DataActions";
import { useState } from "react";

type Props =
{
    columns: GridColDef[],
    rows: object[],
    page: number,
    pageSize: number,
    totalPages: number,
    slug: string,
}

const DataTable = (props: Props) =>
{
    const [rowId, setRowId] = useState<any>(null);
    const [before, setBefore] = useState<any>(null);

    const actionColumn: GridColDef =
    {
        field: "action",
        headerName: "Acciones",
        width: 200,
        renderCell: (params) =>
        {
            return (
                <DataActions {...{params, ...props, rowId, setRowId, before, setBefore}}/>
            )
        }
    }

  return (
    <DataGrid
        className="dataGrid"
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
        />
  )
}

export default DataTable
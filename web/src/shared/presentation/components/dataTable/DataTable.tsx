import { DataGrid, GridCallbackDetails, GridColDef, GridPaginationModel, GridToolbar } from "@mui/x-data-grid";
import "./dataTable.scss"
import { Link } from "react-router-dom";

type Props =
{
    columns: GridColDef[],
    rows: object[],
    page: number,
    pageSize: number,
    totalPages: number,
    slug: string,
    onPaginationModeChanged: (model: GridPaginationModel, details: GridCallbackDetails) => void,
}

const DataTable = (props: Props) =>
{
    const handleDelete = (id: number) =>
    {
        console.log(id + " has been deleted")
    }

    const actionColumn: GridColDef =
    {
        field: "action",
        headerName: "Acciones",
        width: 200,
        renderCell: (params) =>
        {
            return (
                <div className="action">
                    <Link to={`/${props.slug}/${params.row.email}`}>
                        <img src="view.svg" alt="edit icon" />
                    </Link>
                    <div className="delete" onClick={() => handleDelete(params.row.id)}>
                        <img src="delete.svg" alt="delete icon" />
                    </div>
                </div>
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
        rowCount={props.pageSize * props.totalPages}
        pageSizeOptions={[props.pageSize]}
        paginationMode="server"
        onPaginationModelChange={props.onPaginationModeChanged}
        checkboxSelection
        disableRowSelectionOnClick
        disableColumnFilter
        disableDensitySelector
        disableColumnSelector
        />
  )
}

export default DataTable
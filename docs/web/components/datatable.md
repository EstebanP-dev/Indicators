# Data Table

Este componenete es usado en todas las vistas para listar la información en la vista principal de cada modulo.

```ts
type Props =
{
    columns: GridColDef[],
    rows: object[],
    page: number,
    pageSize: number,
    totalPages: number,
    slug: string
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
                    <Link to={`/${props.slug}/${params.row.id}`}>
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
        checkboxSelection
        disableRowSelectionOnClick
        disableColumnFilter
        disableDensitySelector
        disableColumnSelector
        />
  )
}

export default DataTable
```

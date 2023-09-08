import { GridColDef } from "@mui/x-data-grid";
import "./users.scss";
import { useState } from "react";
import { Add, DataTable } from "../../components";

const pageSize: number = 100;

// const columns: GridColDef[] =
// [
//   {
//     field: "id",
//     headerName: "#",
//     width: 100,
//   },
//   {
//     field: "avatar",
//     headerName: "Avatar",
//     width: 100,
//     renderCell: (params) =>
//     {
//       return <img src={params.row.img || "noavatar.png"} alt="avatar picture" />
//     }
//   },
//   {
//     field: "email",
//     headerName: "Correo electrónico",
//     width: 1000,
//     type: "string"
//   },
//   {
//     field: "validate",
//     headerName: 'Validado',
//     width: 100,
//     type: "boolean"
//   }
// ];

// const MapResult = () =>
// {
//   const { result, refresh, page, totalPages } = useUsers(pageSize);

//   if (result.failure)
//   {
//     return result.errors.map((error: Error) =>
//     {
//       return (
//         <span className="error" key={error.name}>{error.message}</span>
//       )
//     });
//   }

//   let count: number = 1;

//   let rows = result.data.map((user) =>
//   {
//     let rowValue =
//     {
//       id: count,
//       email: user.email,
//       validate: true
//     }

//     count++;
//     return rowValue;
//   })

//   return <DataTable
//     columns={columns}
//     rows = {rows}
//     page = {page}
//     slug = "users"
//     pageSize = {pageSize}
//     totalPages = {totalPages}
//     onPaginationModeChanged = {refresh}
//   />;
// }

export const Users = () => {

  
  const [open, setOpen] = useState(false);

  return (
    <div className="users">
      <div className="info">
        <h1>Usuarios</h1>
        <button onClick={() => setOpen(true)}>Nuevo Usuario</button>
      </div>
      {/* {MapResult()}
      {open && <Add setOpen={setOpen} slug="users" columns={columns}/>} */}
    </div>
  )
}

export default Users
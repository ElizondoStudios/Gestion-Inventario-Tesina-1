import type { DTOUsuario } from "DTOs/Usuarios";
import { useEffect, useState } from "react";
import { toast } from "react-toastify";
import { api } from "services/api";
import { DataGrid, type GridColDef } from "@mui/x-data-grid";
import Paper from "@mui/material/Paper";
import Switch from "@mui/material/Switch";
import { auth } from "services/auth";

export default function usuarios() {
  // Const
  const idUsuario= auth.getUserId();

  // State
  const [usuarios, setUsuarios] = useState<DTOUsuario[]>([]);

  // Effects
  useEffect(() => {
    GetUsuarios();
  }, []);

  // API Calls
  const GetUsuarios = () => {
    api
      .UsuariosGetUsuarios()
      .then((data) => {
        setUsuarios(data);
      })
      .catch((error) => {
        toast.error("Ocurrió un error al obtener los usuarios");
      });
  };

  const InhabilitarUsuario = (usuario: DTOUsuario) => {
    api
      .UsuariosInhabilitarUsuario(usuario.IDUsuario)
      .then((data) => {
        toast.success("Se inhabilitó el usuario");
        GetUsuarios();
      })
      .catch((error) => {
        toast.error("Hubo un error al inhabilitar el usuario");
      });
  };
  const HabilitarUsuario = (usuario: DTOUsuario) => {
    api
      .UsuariosHabilitarUsuario(usuario.IDUsuario)
      .then((data) => {
        toast.success("Se habilitó el usuario");
        GetUsuarios();
      })
      .catch((error) => {
        toast.error("Hubo un error al habilitar el usuario");
      });
  };

  // Datagrid
  const columns: GridColDef[] = [
    { field: "IDUsuario", headerName: "ID", width: 70 },
    { field: "Nombre", headerName: "Nombre", flex: 1 },
    { field: "Correo", headerName: "Correo", flex: 1 },
    {
      field: "Activo",
      headerName: "Activo",
      flex: 1,
      renderCell: (cell) => (
        <Switch
          checked={cell.row.Activo}
          onChange={(_) => {
            cell.row.Activo
              ? InhabilitarUsuario(cell.row)
              : HabilitarUsuario(cell.row);
          }}
          disabled={idUsuario == cell.row.IDUsuario}
        ></Switch>
      ),
    },
    {
      field: "DescripcionPerfilPuesto",
      headerName: "Perfil de Puesto",
      flex: 1,
    },
  ];

  const paginationModel = { page: 0, pageSize: 10 };

  return (
    <div className="w-full h-full py-4">
      <div className="card bg-base-100">
        <div className="card-body">
          <DataGrid
            rows={usuarios}
            columns={columns}
            initialState={{ pagination: { paginationModel } }}
            pageSizeOptions={[5, 10]}
            rowSelection={false}
            getRowId={(row: DTOUsuario) => row.IDUsuario}
            sx={{ border: 0 }}
          />
        </div>
      </div>
    </div>
  );
}

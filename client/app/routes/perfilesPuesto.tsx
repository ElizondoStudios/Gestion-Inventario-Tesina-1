import type { DTOUsuario } from "DTOs/Usuarios";
import type { DTOPerfilPuesto } from "DTOs/PerfilesPuesto";
import { useEffect, useState } from "react";
import { toast } from "react-toastify";
import { api } from "services/api";
import { DataGrid, type GridColDef } from "@mui/x-data-grid";
import Switch from "@mui/material/Switch";
import { auth } from "services/auth";
import ActionButton from "~/components/ActionButton";
import Modal from "@mui/material/Modal";
import { useForm } from "react-hook-form";
import { zodResolver } from "@hookform/resolvers/zod";
import {
  crearUsuarioSchema,
  type CrearUsuarioFormData,
} from "~/form schemas/crearUsuarioSchema";
import {
  editarUsuarioSchema,
  type EditarUsuarioFormData,
} from "~/form schemas/editarUsuarioSchema";
import { useDispatch } from "react-redux";
import { changeCurrentPage } from "services/slices/currentPageSlice";

export default function perfilesPuesto() {
  // State
  const [perfilesPuesto, setPerfilesPuesto] = useState<DTOPerfilPuesto[]>([]);
  const [verModalCrear, setVerModalCrear] = useState<boolean>(false);
  const [verModalEditar, setVerModalEditar] = useState<boolean>(false);
  // Redux
  const dispatch = useDispatch();

  // Effects
  useEffect(() => {
    dispatch(changeCurrentPage("Perfiles de Puesto"));
    GetPerfilesPuesto();
  }, []);

  // API Calls
  const GetPerfilesPuesto = () => {
    api
      .PerfilesPuestoGetPerfilesPuesto()
      .then((data) => {
        setPerfilesPuesto(data);
      })
      .catch((error) => {
        toast.error("Ocurrió un error al obtener los perfiles de puesto");
      });
  };
  const InhabilitarPerfilPuesto = (perfilPuesto: DTOPerfilPuesto) => {
    api
      .PerfilesPuestoInhabilitarPerfilPuesto(perfilPuesto.IDPerfilPuesto)
      .then((data) => {
        toast.success("Se inhabilitó el perfil de puesto");
        GetPerfilesPuesto();
      })
      .catch((error) => {
        toast.error("Hubo un error al inhabilitar el usuario");
      });
  };
  const HabilitarPerfilPuesto = (perfilPuesto: DTOPerfilPuesto) => {
    api
      .PerfilesPuestoHabilitarPerfilPuesto(perfilPuesto.IDPerfilPuesto)
      .then((data) => {
        toast.success("Se habilitó el perfil de puesto");
        GetPerfilesPuesto();
      })
      .catch((error) => {
        toast.error("Hubo un error al habilitar el usuario");
      });
  };

  // Actions
  const abrirModalCrear = () => {
    setVerModalCrear(true);
  };
  const cerrarModalCrear = () => {
    setVerModalCrear(false);
  };
  const abrirModalEditar = (usuario: DTOUsuario) => {
    // resetEditarUsuario({
    //   IDUsuario: usuario.IDUsuario,
    //   Nombre: usuario.Nombre,
    //   Correo: usuario.Correo,
    //   IDPerfilPuesto: `${usuario.IDPerfilPuesto}`,
    // });
    setVerModalEditar(true);
  };
  const cerrarModalEditar = () => {
    setVerModalEditar(false);
  };

  // Datagrid
  const columns: GridColDef[] = [
    { field: "IDPerfilPuesto", headerName: "ID", width: 70 },
    { field: "Descripcion", headerName: "Descripción", flex: 1 },
    {
      field: "Activo",
      headerName: "Activo",
      flex: 1,
      renderCell: (cell) => (
        <Switch
          checked={cell.row.Activo}
          onChange={(_) => {
            cell.row.Activo
              ? InhabilitarPerfilPuesto(cell.row)
              : HabilitarPerfilPuesto(cell.row);
          }}
          disabled={1 == cell.row.IDPerfilPuesto}
        ></Switch>
      ),
    },
    {
      field: "acciones",
      headerName: "Acciones",
      width: 100,
      renderCell: (cell) => (
        <ActionButton
          icon="edit"
          text="Editar"
          action={() => {
            abrirModalEditar(cell.row);
          }}
          disabled={!cell.row.Activo}
        />
      ),
    },
  ];

  const paginationModel = { page: 0, pageSize: 10 };
  return (
    <>
      {/* Componente principal */}
      <div className="w-full h-full py-4">
        <div className="card bg-base-100">
          <div className="card-body">
            <div className="w-full flex justify-end">
              <ActionButton icon="add" text="Crear" action={abrirModalCrear} />
            </div>
            <DataGrid
              rows={perfilesPuesto}
              columns={columns}
              initialState={{ pagination: { paginationModel } }}
              pageSizeOptions={[5, 10]}
              rowSelection={false}
              getRowId={(row: DTOPerfilPuesto) => row.IDPerfilPuesto}
              sx={{ border: 0 }}
            />
          </div>
        </div>
      </div>
    </>
  );
}

import type { DTOUsuario } from "DTOs/Usuarios";
import type { DTOPerfilPuesto } from "DTOs/PerfilesPuesto";
import { useEffect, useState } from "react";
import { toast } from "react-toastify";
import { api } from "services/api";
import { DataGrid, type GridColDef } from "@mui/x-data-grid";
import Switch from "@mui/material/Switch";
import ActionButton from "~/components/ActionButton";
import Modal from "@mui/material/Modal";
import { useForm } from "react-hook-form";
import { zodResolver } from "@hookform/resolvers/zod";
import { useDispatch } from "react-redux";
import { changeCurrentPage } from "services/slices/currentPageSlice";
import {
  crearPerfilPuestoSchema,
  type CrearPerfilPuestoFormData,
} from "~/form schemas/crearPerfilPuestoSchema";
import {
  editarPerfilPuestoSchema,
  type EditarPerfilPuestoFormData,
} from "~/form schemas/editarPerfilPuestoSchema";

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
  const abrirModalEditar = (perfilPuesto: DTOPerfilPuesto) => {
    resetEditarPerfilPuesto({
      IDPerfilPuesto: perfilPuesto.IDPerfilPuesto,
      Descripcion: perfilPuesto.Descripcion
    })
    setVerModalEditar(true);
  };
  const cerrarModalEditar = () => {
    setVerModalEditar(false);
  };

  // Datagrid
  const columns: GridColDef[] = [
    { field: "IDPerfilPuesto", headerName: "ID", width: 70 },
    { field: "Descripcion", headerName: "Descripción", flex: 1, minWidth: 150 },
    {
      field: "Activo",
      headerName: "Activo",
      flex: 1,
      minWidth: 100,
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

  // Crear PerfilPuesto Form
  const {
    register: registerCrearPerfilPuesto,
    handleSubmit: handleSubmitCrearPerfilPuesto,
    formState: { errors: errorsCrearPerfilPuesto },
  } = useForm<CrearPerfilPuestoFormData>({
    resolver: zodResolver(crearPerfilPuestoSchema),
  });

  const onSubmitCrearPerfilPuesto = (formData: CrearPerfilPuestoFormData) => {
    api
      .PerfilesPuestoCrearPerfilPuesto(formData)
      .then(() => {
        toast.success("Se creó el usuario de forma correcta");
        GetPerfilesPuesto();
        cerrarModalCrear();
      })
      .catch(() => {
        toast.error("Hubo un error al crear el usuario");
      });
  };

  // Editar PerfilPuesto Form
  const {
    register: registerEditarPerfilPuesto,
    handleSubmit: handleSubmitEditarPerfilPuesto,
    formState: { errors: errorsEditarPerfilPuesto },
    reset: resetEditarPerfilPuesto,
  } = useForm<EditarPerfilPuestoFormData>({
    resolver: zodResolver(editarPerfilPuestoSchema),
  });

  const onSubmitEditarPerfilPuesto = (formData: EditarPerfilPuestoFormData) => {
    api
      .PerfilesPuestoActualizarPerfilPuesto(formData)
      .then(() => {
        toast.success("Se creó el usuario de forma correcta");
        GetPerfilesPuesto();
        cerrarModalEditar();
      })
      .catch(() => {
        toast.error("Hubo un error al crear el usuario");
      });
  };

  const paginationModel = { page: 0, pageSize: 10 };
  return (
    <>
      {/* Componente principal */}
      <div className="w-full h-full py-4">
        <div className="card bg-base-100">
          <div className="card-body">
            <h1 className="card-title">Perfiles de Puesto</h1>
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
      {/* Modal Crear */}
      <Modal
        open={verModalCrear}
        onClose={cerrarModalCrear}
        className="flex items-start justify-center py-10"
      >
        <div className="card w-4/5 bg-base-100">
          <div className="card-body">
            <h2 className="card-title">Crear Perfil de Puesto</h2>
            <form
              className="w-full grid grid-cols-2 gap-4"
              onSubmit={handleSubmitCrearPerfilPuesto(
                onSubmitCrearPerfilPuesto,
              )}
            >
              <div className="col-span-2">
                <label>Descripción</label>
                <input
                  {...registerCrearPerfilPuesto("Descripcion")}
                  type="text"
                  className=" w-full input"
                  placeholder="Descripción"
                />
                {errorsCrearPerfilPuesto.Descripcion && (
                  <p className="text-sm text-error">
                    {errorsCrearPerfilPuesto.Descripcion.message}
                  </p>
                )}
              </div>
              <button type="submit" className="btn btn-primary col-span-2">
                Crear Perfil de Puesto
              </button>
            </form>
          </div>
        </div>
      </Modal>
      {/* Modal Editar */}
      <Modal
        open={verModalEditar}
        onClose={cerrarModalEditar}
        className="flex items-start justify-center py-10"
      >
        <div className="card w-4/5 bg-base-100">
          <div className="card-body">
            <h2 className="card-title">Editar Perfil de Puesto</h2>
            <form
              className="w-full grid grid-cols-2 gap-4"
              onSubmit={handleSubmitEditarPerfilPuesto(
                onSubmitEditarPerfilPuesto,
              )}
            >
              <div className="col-span-2">
                <label>Descripción</label>
                <input
                  {...registerEditarPerfilPuesto("Descripcion")}
                  type="text"
                  className=" w-full input"
                  placeholder="Descripción"
                />
                {errorsEditarPerfilPuesto.Descripcion && (
                  <p className="text-sm text-error">
                    {errorsEditarPerfilPuesto.Descripcion.message}
                  </p>
                )}
              </div>
              <button type="submit" className="btn btn-primary col-span-2">
                Editar Perfil de Puesto
              </button>
            </form>
          </div>
        </div>
      </Modal>
    </>
  );
}

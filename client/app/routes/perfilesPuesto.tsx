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
import type { DTOModulo, DTOModulosAcceso, DTONivel } from "DTOs/ModulosAcceso";
import { crearAccesoSchema, type CrearAccesoFormData } from "~/form schemas/crearAccesoSchema";

export default function perfilesPuesto() {
  // State
  const [perfilesPuesto, setPerfilesPuesto] = useState<DTOPerfilPuesto[]>([]);
  const [perfilPuestoSeleccionado, setPerfilPuestoSeleccionado] =
    useState<DTOPerfilPuesto>();
  const [modulos, setModulos] = useState<DTOModulo[]>([]);
  const [modulosAccesoPerfilPuesto, setModulosAccesoPerfilPuesto] = useState<DTOModulosAcceso[]>([]);
  const [niveles, setNiveles] = useState<DTONivel[]>([]);
  const [verModalCrear, setVerModalCrear] = useState<boolean>(false);
  const [verModalEditar, setVerModalEditar] = useState<boolean>(false);
  const [verModalAccesos, setVerModalAccesos] = useState<boolean>(false);
  // Redux
  const dispatch = useDispatch();

  // Effects
  useEffect(() => {
    dispatch(changeCurrentPage("Perfiles de Puesto"));
    GetPerfilesPuesto();
    GetModulos();
    GetNiveles();
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
  const GetModulos = () => {
    api
      .GetModulos()
      .then((data) => {
        setModulos(data);
      })
      .catch((error) => {
        toast.error("Ocurrió un error al obtener los modulos");
      });
  };
  const GetNiveles = () => {
    api
      .GetNiveles()
      .then((data) => {
        setNiveles(data);
      })
      .catch((error) => {
        toast.error("Ocurrió un error al obtener los niveles");
      });
  };
  const GetModulosAccesoPerfilPuesto = (IDPerfilPuesto: number) => {
    api
      .GetModulosAccesoPerfilPuesto(IDPerfilPuesto)
      .then((data) => {
        setModulosAccesoPerfilPuesto(data);
      })
      .catch((error) => {
        toast.error("Ocurrió un error al obtener los niveles");
      });
  };
  const EliminarAccesoModulo = (IDModuloAcceso: number) => {
    api
      .EliminarAccesoModulo(IDModuloAcceso)
      .then(() => {
        toast.success("Se eliminó el acceso")
        cerrarModalAccesos();
      })
      .catch(() => {
        toast.error("Ocurrió un error al eliminar el acceso")
      })
  }

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
      Descripcion: perfilPuesto.Descripcion,
    });
    setVerModalEditar(true);
  };
  const cerrarModalEditar = () => {
    setVerModalEditar(false);
  };
  const abrirModalAccesos = (perfilPuesto: DTOPerfilPuesto) => {
    setPerfilPuestoSeleccionado(perfilPuesto);
    GetModulosAccesoPerfilPuesto(perfilPuesto.IDPerfilPuesto);
    resetCrearAcceso({IDPerfilPuesto: perfilPuesto.IDPerfilPuesto})    
    setVerModalAccesos(true);
  };
  const cerrarModalAccesos = () => {
    setVerModalAccesos(false);
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
      width: 220,
      renderCell: (cell) => (
        <>
          <ActionButton
            icon="key"
            text="Accesos"
            action={() => {
              abrirModalAccesos(cell.row);
            }}
            disabled={!cell.row.Activo}
          />
          <ActionButton
            icon="edit"
            text="Editar"
            action={() => {
              abrirModalEditar(cell.row);
            }}
            disabled={!cell.row.Activo}
          />
        </>
      ),
    },
  ];
  const columnsModulosAccesoPerfilesPuesto: GridColDef[] = [
    { field: "DescripcionNivelAcceso", headerName: "Nivel", flex: 1, minWidth: 150 },
    { field: "NombreModulo", headerName: "Módulo", flex: 1, minWidth: 150 },
    {
      field: "acciones",
      headerName: "Acciones",
      width: 220,
      renderCell: (cell) => (
        <>
          <ActionButton
            icon="delete"
            text="Eliminar"
            action={() => {
              EliminarAccesoModulo(cell.row.IDModuloAcceso)
            }}
          />
        </>
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

  // Crear acceso
  const {
    register: registerCrearAcceso,
    handleSubmit: handleSubmitCrearAcceso,
    formState: { errors: errorsCrearAcceso },
    reset: resetCrearAcceso,
  } = useForm<CrearAccesoFormData>({
    resolver: zodResolver(crearAccesoSchema),
  });

  const onSubmitCrearAcceso = (formData: CrearAccesoFormData) => {
    api
      .RegistrarAccesoModulo(formData)
      .then(() => {
        toast.success("Se registró el acceso");
        GetModulosAccesoPerfilPuesto(formData.IDPerfilPuesto);
      })
      .catch(() => {
        toast.error("Hubo un error al registrar el acceso");
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
      {/* Modal Accesos */}
      <Modal
        open={verModalAccesos}
        onClose={cerrarModalAccesos}
        className="flex items-start justify-center py-10"
      >
        <div className="card w-4/5 bg-base-100">
          <div className="card-body">
            <h2 className="card-title">
              Editar Accesos del Perfil de Puesto:
              {` ${perfilPuestoSeleccionado?.Descripcion}`}
            </h2>
            <form className="w-full grid grid-cols-2 gap-4" onSubmit={handleSubmitCrearAcceso(onSubmitCrearAcceso)}>
              <div className="col-span-2">
                <h3>Agregar Accesos</h3>
              </div>
              <div className="col-span-2 lg:col-span-1">
                <label>Modulo</label>
                <select
                  {...registerCrearAcceso("IDModulo", { valueAsNumber: true })}
                  className="w-full select"
                >
                  {modulos.map((modulo) => (
                    <option key={modulo.IDModulo} value={modulo.IDModulo}>
                      {modulo.Nombre}
                    </option>
                  ))}
                </select>
                {errorsCrearAcceso.IDModulo && (
                  <p className="text-sm text-error">
                    {errorsCrearAcceso.IDModulo.message}
                  </p>
                )}
              </div>
              <div className="col-span-2 lg:col-span-1">
                <label>Nivel</label>
                <select
                  {...registerCrearAcceso("IDNivelAcceso", { valueAsNumber: true })}
                  className="w-full select"
                >
                  {niveles.map((nivel) => (
                    <option key={nivel.NivelAcceso} value={nivel.NivelAcceso}>
                      {nivel.Descripcion}
                    </option>
                  ))}
                </select>
                {errorsCrearAcceso.IDNivelAcceso && (
                  <p className="text-sm text-error">
                    {errorsCrearAcceso.IDNivelAcceso.message}
                  </p>
                )}
              </div>
              <button type="submit" className="btn btn-primary col-span-2">
                Agregar Acceso
              </button>
            </form>
            <div className="flex flex-col items-start justify-center gap-4 mt-4">
              <h3>Accesos Agregados</h3>
              <div className="w-full">
                <DataGrid
                  rows={modulosAccesoPerfilPuesto}
                  columns={columnsModulosAccesoPerfilesPuesto}
                  initialState={{ pagination: { paginationModel } }}
                  pageSizeOptions={[5, 10]}
                  rowSelection={false}
                  getRowId={(row: DTOModulosAcceso) => row.IDModuloAcceso}
                  sx={{ border: 0 }}
                />
              </div>
            </div>
          </div>
        </div>
      </Modal>
    </>
  );
}

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
import { crearUsuarioSchema, type CrearUsuarioFormData } from "~/form schemas/crearUsuarioSchema";

export default function usuarios() {
  // State
  const [usuarios, setUsuarios] = useState<DTOUsuario[]>([]);
  const [idUsuario, setIdUsuario] = useState<number>(0);
  const [perfilesPuesto, setPerfilesPuesto] = useState<DTOPerfilPuesto[]>([]);
  const [verModalCrear, setVerModalCrear] = useState<boolean>(false);

  // Effects
  useEffect(() => {
    setIdUsuario(parseInt(auth.getUserId() ?? "0"));
    GetUsuarios();
    GetPerfilesPuesto();
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

  // Actions
  const abrirModalCrear = () => {
    setVerModalCrear(true);
  };
  const cerrarModalCrear = () => {
    setVerModalCrear(false);
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
    {
      field: "acciones",
      headerName: "Acciones",
      width: 100,
      renderCell: (cell) => (
        <ActionButton icon="edit" text="Editar" action={() => {}} />
      ),
    },
  ];

  const paginationModel = { page: 0, pageSize: 10 };

  // Crear Usuario Form
  const {
    register: registerCrearUsuario,
    handleSubmit: handleSubmitCrearUsuario,
    formState: { errors },
  } = useForm<CrearUsuarioFormData>({
    resolver: zodResolver(crearUsuarioSchema),
  });

  const onSubmit = (formData: CrearUsuarioFormData) => {
    api.UsuariosCrearUsuario({...formData, IDPerfilPuesto: parseInt(formData.IDPerfilPuesto)})
    .then(() => {
      toast.success("Se creó el usuario de forma correcta")
      GetUsuarios();
      cerrarModalCrear();
    })
    .catch(() => {
      toast.error("Hubo un error al crear el usuario")
    })
  };

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
      {/* Modal Crear */}
      <Modal
        open={verModalCrear}
        onClose={cerrarModalCrear}
        className="flex items-start justify-center py-10"
      >
        <div className="card w-4/5 bg-base-100">
          <div className="card-body">
            <h2 className="card-title">Crear Usuario</h2>
            <form className="w-full grid grid-cols-2 gap-4" onSubmit={handleSubmitCrearUsuario(onSubmit)}>
            <div className="col-span-2">
              <label>Nombre</label>
              <input {...registerCrearUsuario("Nombre")} type="text" className=" w-full input" placeholder="Nombre Apellido" />
              {errors.Nombre && (
                <p className='text-sm text-error'>{errors.Nombre.message}</p>
              )}
            </div>
            <div className="col-span-2 lg:col-span-1">
              <label>Correo</label>
              <input {...registerCrearUsuario("Correo")} type="email" className=" w-full input" placeholder="tucorreo@correo.com" />
              {errors.Correo && (
                <p className='text-sm text-error'>{errors.Correo.message}</p>
              )}
            </div>
            <div className="col-span-2 lg:col-span-1">
              <label>Contraseña</label>
              <input {...registerCrearUsuario("Contrasenia")} type="text" className=" w-full input" placeholder="******" />
              {errors.Contrasenia && (
                <p className='text-sm text-error'>{errors.Contrasenia.message}</p>
              )}
            </div>
            <div className="col-span-2">
              <label>Perfil de Puesto</label>
              <select className="w-full select" {...registerCrearUsuario("IDPerfilPuesto")}>
                {
                  perfilesPuesto.map(perfilPuesto => (
                    <option key={`perfilPuesto-${perfilPuesto.IDPerfilPuesto}`} value={perfilPuesto.IDPerfilPuesto}>{perfilPuesto.Descripcion}</option>
                  ))
                }
              </select>
              {errors.IDPerfilPuesto && (
                <p className='text-sm text-error'>{errors.IDPerfilPuesto.message}</p>
              )}
            </div>
            <button type="submit" className='btn btn-primary col-span-2'>Crear Usuario</button>
          </form>
          </div>
        </div>
      </Modal>
    </>
  );
}

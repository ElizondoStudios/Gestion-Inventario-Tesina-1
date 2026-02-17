import type { DTOSucursal } from "DTOs/Sucursales";
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
  crearSucursalSchema,
  type CrearSucursalFormData,
} from "~/form schemas/crearSucursalSchema";
import {
  editarSucursalSchema,
  type EditarSucursalFormData,
} from "~/form schemas/editarSucursalSchema";

export default function sucursales() {
  // State
  const [sucursales, setSucursales] = useState<DTOSucursal[]>([]);
  const [verModalCrear, setVerModalCrear] = useState<boolean>(false);
  const [verModalEditar, setVerModalEditar] = useState<boolean>(false);
  // Redux
  const dispatch = useDispatch();

  // Effects
  useEffect(() => {
    dispatch(changeCurrentPage("Sucursales"));
    GetSucursales();
  }, []);

  // API Calls
  const GetSucursales = () => {
    api
      .SucursalesGetSucursales()
      .then((data) => {
        setSucursales(data);
      })
      .catch((error) => {
        toast.error("Ocurrió un error al obtener las sucursales");
      });
  };
  const InhabilitarSucursal = (sucursal: DTOSucursal) => {
    api
      .SucursalesInhabilitarSucursal(sucursal.IDSucursal)
      .then((data) => {
        toast.success("Se inhabilitó la sucursal");
        GetSucursales();
      })
      .catch((error) => {
        toast.error("Hubo un error al inhabilitar la sucursal");
      });
  };
  const HabilitarSucursal = (sucursal: DTOSucursal) => {
    api
      .SucursalesHabilitarSucursal(sucursal.IDSucursal)
      .then((data) => {
        toast.success("Se habilitó la sucursal");
        GetSucursales();
      })
      .catch((error) => {
        toast.error("Hubo un error al habilitar la sucursal");
      });
  };

  // Actions
  const abrirModalCrear = () => {
    setVerModalCrear(true);
  };
  const cerrarModalCrear = () => {
    setVerModalCrear(false);
  };
  const abrirModalEditar = (sucursal: DTOSucursal) => {
    resetEditarSucursal({
      IDSucursal: sucursal.IDSucursal,
      Nombre: sucursal.Nombre,
      Direccion: sucursal.Direccion,
    });
    setVerModalEditar(true);
  };
  const cerrarModalEditar = () => {
    setVerModalEditar(false);
  };

  // Datagrid
  const columns: GridColDef[] = [
    { field: "IDSucursal", headerName: "ID", width: 70 },
    { field: "Nombre", headerName: "Nombre", flex: 1 },
    { field: "Direccion", headerName: "Dirección", flex: 1 },
    {
      field: "Activo",
      headerName: "Activo",
      flex: 1,
      renderCell: (cell) => (
        <Switch
          checked={cell.row.Activo}
          onChange={(_) => {
            cell.row.Activo
              ? InhabilitarSucursal(cell.row)
              : HabilitarSucursal(cell.row);
          }}
          disabled={1 == cell.row.IDSucursal}
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

  // Crear Sucursal Form
  const {
    register: registerCrearSucursal,
    handleSubmit: handleSubmitCrearSucursal,
    formState: { errors: errorsCrearSucursal },
  } = useForm<CrearSucursalFormData>({
    resolver: zodResolver(crearSucursalSchema),
  });

  const onSubmitCrearSucursal = (formData: CrearSucursalFormData) => {
    api
      .SucursalesCrearSucursal(formData)
      .then(() => {
        toast.success("Se creó la sucursal de forma correcta");
        GetSucursales();
        cerrarModalCrear();
      })
      .catch(() => {
        toast.error("Hubo un error al crear la sucursal");
      });
  };

  // Editar Sucursal Form
  const {
    register: registerEditarSucursal,
    handleSubmit: handleSubmitEditarSucursal,
    formState: { errors: errorsEditarSucursal },
    reset: resetEditarSucursal,
  } = useForm<EditarSucursalFormData>({
    resolver: zodResolver(editarSucursalSchema),
  });

  const onSubmitEditarSucursal = (formData: EditarSucursalFormData) => {
    api
      .SucursalesActualizarSucursal(formData)
      .then(() => {
        toast.success("Se actualizó la sucursal de forma correcta");
        GetSucursales();
        cerrarModalEditar();
      })
      .catch(() => {
        toast.error("Hubo un error al actualizar la sucursal");
      });
  };

  const paginationModel = { page: 0, pageSize: 10 };
  return (
    <>
      {/* Componente principal */}
      <div className="w-full h-full py-4">
        <div className="card bg-base-100">
          <div className="card-body">
            <h1 className="card-title">Sucursales</h1>
            <div className="w-full flex justify-end">
              <ActionButton icon="add" text="Crear" action={abrirModalCrear} />
            </div>
            <DataGrid
              rows={sucursales}
              columns={columns}
              initialState={{ pagination: { paginationModel } }}
              pageSizeOptions={[5, 10]}
              rowSelection={false}
              getRowId={(row: DTOSucursal) => row.IDSucursal}
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
            <h2 className="card-title">Crear Sucursal</h2>
            <form
              className="w-full grid grid-cols-2 gap-4"
              onSubmit={handleSubmitCrearSucursal(onSubmitCrearSucursal)}
            >
              <div>
                <label>Nombre</label>
                <input
                  {...registerCrearSucursal("Nombre")}
                  type="text"
                  className="w-full input"
                  placeholder="Nombre"
                />
                {errorsCrearSucursal.Nombre && (
                  <p className="text-sm text-error">
                    {errorsCrearSucursal.Nombre.message}
                  </p>
                )}
              </div>
              <div>
                <label>Dirección</label>
                <input
                  {...registerCrearSucursal("Direccion")}
                  type="text"
                  className="w-full input"
                  placeholder="Dirección"
                />
                {errorsCrearSucursal.Direccion && (
                  <p className="text-sm text-error">
                    {errorsCrearSucursal.Direccion.message}
                  </p>
                )}
              </div>
              <button type="submit" className="btn btn-primary col-span-2">
                Crear Sucursal
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
            <h2 className="card-title">Editar Sucursal</h2>
            <form
              className="w-full grid grid-cols-2 gap-4"
              onSubmit={handleSubmitEditarSucursal(onSubmitEditarSucursal)}
            >
              <div>
                <label>Nombre</label>
                <input
                  {...registerEditarSucursal("Nombre")}
                  type="text"
                  className="w-full input"
                  placeholder="Nombre"
                />
                {errorsEditarSucursal.Nombre && (
                  <p className="text-sm text-error">
                    {errorsEditarSucursal.Nombre.message}
                  </p>
                )}
              </div>
              <div>
                <label>Dirección</label>
                <input
                  {...registerEditarSucursal("Direccion")}
                  type="text"
                  className="w-full input"
                  placeholder="Dirección"
                />
                {errorsEditarSucursal.Direccion && (
                  <p className="text-sm text-error">
                    {errorsEditarSucursal.Direccion.message}
                  </p>
                )}
              </div>
              <button type="submit" className="btn btn-primary col-span-2">
                Editar Sucursal
              </button>
            </form>
          </div>
        </div>
      </Modal>
    </>
  );
}

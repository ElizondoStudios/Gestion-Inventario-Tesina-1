import type { DTOInventario } from "DTOs/Inventario";
import type { DTOUnidad } from "DTOs/Unidades";
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
  crearInventarioSchema,
  type CrearInventarioFormData,
} from "~/form schemas/crearInventarioSchema";
import {
  editarInventarioSchema,
  type EditarInventarioFormData,
} from "~/form schemas/editarInventarioSchema";

export default function inventario() {
  // State
  const [inventario, setInventario] = useState<DTOInventario[]>([]);
  const [unidades, setUnidades] = useState<DTOUnidad[]>([]);
  const [verModalCrear, setVerModalCrear] = useState<boolean>(false);
  const [verModalEditar, setVerModalEditar] = useState<boolean>(false);
  // Redux
  const dispatch = useDispatch();

  // Effects
  useEffect(() => {
    dispatch(changeCurrentPage("Inventario"));
    GetInventario();
    GetUnidades();
  }, []);

  // API Calls
  const GetInventario = () => {
    api
      .InventarioGetInventario()
      .then((data) => {
        setInventario(data);
      })
      .catch((error) => {
        toast.error("Ocurrió un error al obtener el inventario");
      });
  };
  const GetUnidades = () => {
    api
      .UnidadesGetUnidades()
      .then((data) => {
        setUnidades(data.filter((u) => u.Activo));
      })
      .catch((error) => {
        toast.error("Ocurrió un error al obtener las unidades");
      });
  };
  const InhabilitarProducto = (producto: DTOInventario) => {
    api
      .InventarioInhabilitarProducto(producto.NoParte)
      .then((data) => {
        toast.success("Se inhabilitó el producto");
        GetInventario();
      })
      .catch((error) => {
        toast.error("Hubo un error al inhabilitar el producto");
      });
  };
  const HabilitarProducto = (producto: DTOInventario) => {
    api
      .InventarioHabilitarProducto(producto.NoParte)
      .then((data) => {
        toast.success("Se habilitó el producto");
        GetInventario();
      })
      .catch((error) => {
        toast.error("Hubo un error al habilitar el producto");
      });
  };

  // Actions
  const abrirModalCrear = () => {
    setVerModalCrear(true);
  };
  const cerrarModalCrear = () => {
    setVerModalCrear(false);
  };
  const abrirModalEditar = (producto: DTOInventario) => {
    resetEditarInventario({
      NoParte: producto.NoParte,
      NombreProducto: producto.NombreProducto,
      DescripcionProducto: producto.DescripcionProducto,
      Precio: producto.Precio,
      Costo: producto.Costo,
      IDUnidad: producto.IDUnidad,
    });
    setVerModalEditar(true);
  };
  const cerrarModalEditar = () => {
    setVerModalEditar(false);
  };

  // Datagrid
  const columns: GridColDef[] = [
    { field: "NoParte", headerName: "No. Parte", width: 120 },
    { field: "NombreProducto", headerName: "Nombre", flex: 1 },
    { field: "DescripcionProducto", headerName: "Descripción", flex: 1 },
    {
      field: "Precio",
      headerName: "Precio",
      width: 120,
      renderCell: (cell) => `$${cell.row.Precio.toFixed(2)}`,
    },
    {
      field: "Costo",
      headerName: "Costo",
      width: 120,
      renderCell: (cell) => `$${cell.row.Costo.toFixed(2)}`,
    },
    { field: "DescripcionUnidad", headerName: "Unidad", width: 120 },
    {
      field: "Activo",
      headerName: "Activo",
      width: 100,
      renderCell: (cell) => (
        <Switch
          checked={cell.row.Activo}
          onChange={(_) => {
            cell.row.Activo
              ? InhabilitarProducto(cell.row)
              : HabilitarProducto(cell.row);
          }}
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

  // Crear Inventario Form
  const {
    register: registerCrearInventario,
    handleSubmit: handleSubmitCrearInventario,
    formState: { errors: errorsCrearInventario },
  } = useForm<CrearInventarioFormData>({
    resolver: zodResolver(crearInventarioSchema),
  });

  const onSubmitCrearInventario = (formData: CrearInventarioFormData) => {
    api
      .InventarioCrearProducto(formData)
      .then(() => {
        toast.success("Se creó el producto de forma correcta");
        GetInventario();
        cerrarModalCrear();
      })
      .catch(() => {
        toast.error("Hubo un error al crear el producto");
      });
  };

  // Editar Inventario Form
  const {
    register: registerEditarInventario,
    handleSubmit: handleSubmitEditarInventario,
    formState: { errors: errorsEditarInventario },
    reset: resetEditarInventario,
  } = useForm<EditarInventarioFormData>({
    resolver: zodResolver(editarInventarioSchema),
  });

  const onSubmitEditarInventario = (formData: EditarInventarioFormData) => {
    api
      .InventarioActualizarProducto(formData)
      .then(() => {
        toast.success("Se actualizó el producto de forma correcta");
        GetInventario();
        cerrarModalEditar();
      })
      .catch(() => {
        toast.error("Hubo un error al actualizar el producto");
      });
  };

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
              rows={inventario}
              columns={columns}
              initialState={{ pagination: { paginationModel } }}
              pageSizeOptions={[5, 10]}
              rowSelection={false}
              getRowId={(row: DTOInventario) => row.NoParte}
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
            <h2 className="card-title">Crear Producto</h2>
            <form
              className="w-full grid grid-cols-6 gap-4"
              onSubmit={handleSubmitCrearInventario(onSubmitCrearInventario)}
            >
              <div className="col-span-6 lg:col-span-3">
                <label>No. Parte</label>
                <input
                  {...registerCrearInventario("NoParte")}
                  type="text"
                  className="w-full input"
                  placeholder="No. Parte"
                />
                {errorsCrearInventario.NoParte && (
                  <p className="text-sm text-error">
                    {errorsCrearInventario.NoParte.message}
                  </p>
                )}
              </div>
              <div className="col-span-6 lg:col-span-3">
                <label>Nombre</label>
                <input
                  {...registerCrearInventario("NombreProducto")}
                  type="text"
                  className="w-full input"
                  placeholder="Nombre del producto"
                />
                {errorsCrearInventario.NombreProducto && (
                  <p className="text-sm text-error">
                    {errorsCrearInventario.NombreProducto.message}
                  </p>
                )}
              </div>
              <div className="col-span-6">
                <label>Descripción</label>
                <input
                  {...registerCrearInventario("DescripcionProducto")}
                  type="text"
                  className="w-full input"
                  placeholder="Descripción del producto"
                />
                {errorsCrearInventario.DescripcionProducto && (
                  <p className="text-sm text-error">
                    {errorsCrearInventario.DescripcionProducto.message}
                  </p>
                )}
              </div>
              <div className="col-span-6 lg:col-span-2">
                <label>Precio</label>
                <input
                  {...registerCrearInventario("Precio", { valueAsNumber: true })}
                  type="number"
                  step="0.01"
                  className="w-full input"
                  placeholder="Precio"
                />
                {errorsCrearInventario.Precio && (
                  <p className="text-sm text-error">
                    {errorsCrearInventario.Precio.message}
                  </p>
                )}
              </div>
              <div className="col-span-6 lg:col-span-2">
                <label>Costo</label>
                <input
                  {...registerCrearInventario("Costo", { valueAsNumber: true })}
                  type="number"
                  step="0.01"
                  className="w-full input"
                  placeholder="Costo"
                />
                {errorsCrearInventario.Costo && (
                  <p className="text-sm text-error">
                    {errorsCrearInventario.Costo.message}
                  </p>
                )}
              </div>
              <div className="col-span-6 lg:col-span-2">
                <label>Unidad</label>
                <select
                  {...registerCrearInventario("IDUnidad", { valueAsNumber: true })}
                  className="w-full select"
                >
                  {unidades.map((unidad) => (
                    <option key={unidad.IDUnidad} value={unidad.IDUnidad}>
                      {unidad.Descripcion} ({unidad.Abreviacion})
                    </option>
                  ))}
                </select>
                {errorsCrearInventario.IDUnidad && (
                  <p className="text-sm text-error">
                    {errorsCrearInventario.IDUnidad.message}
                  </p>
                )}
              </div>
              <button type="submit" className="btn btn-primary col-span-6">
                Crear Producto
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
            <h2 className="card-title">Editar Producto</h2>
            <form
              className="w-full grid grid-cols-6 gap-4"
              onSubmit={handleSubmitEditarInventario(onSubmitEditarInventario)}
            >
              <div className="col-span-6 lg:col-span-3">
                <label>No. Parte</label>
                <input
                  {...registerEditarInventario("NoParte")}
                  type="text"
                  className="w-full input"
                  placeholder="No. Parte"
                />
                {errorsEditarInventario.NoParte && (
                  <p className="text-sm text-error">
                    {errorsEditarInventario.NoParte.message}
                  </p>
                )}
              </div>
              <div className="col-span-6 lg:col-span-3">
                <label>Nombre</label>
                <input
                  {...registerEditarInventario("NombreProducto")}
                  type="text"
                  className="w-full input"
                  placeholder="Nombre del producto"
                />
                {errorsEditarInventario.NombreProducto && (
                  <p className="text-sm text-error">
                    {errorsEditarInventario.NombreProducto.message}
                  </p>
                )}
              </div>
              <div className="col-span-6">
                <label>Descripción</label>
                <input
                  {...registerEditarInventario("DescripcionProducto")}
                  type="text"
                  className="w-full input"
                  placeholder="Descripción del producto"
                />
                {errorsEditarInventario.DescripcionProducto && (
                  <p className="text-sm text-error">
                    {errorsEditarInventario.DescripcionProducto.message}
                  </p>
                )}
              </div>
              <div className="col-span-6 lg:col-span-2">
                <label>Precio</label>
                <input
                  {...registerEditarInventario("Precio", { valueAsNumber: true })}
                  type="number"
                  step="0.01"
                  className="w-full input"
                  placeholder="Precio"
                />
                {errorsEditarInventario.Precio && (
                  <p className="text-sm text-error">
                    {errorsEditarInventario.Precio.message}
                  </p>
                )}
              </div>
              <div className="col-span-6 lg:col-span-2">
                <label>Costo</label>
                <input
                  {...registerEditarInventario("Costo", { valueAsNumber: true })}
                  type="number"
                  step="0.01"
                  className="w-full input"
                  placeholder="Costo"
                />
                {errorsEditarInventario.Costo && (
                  <p className="text-sm text-error">
                    {errorsEditarInventario.Costo.message}
                  </p>
                )}
              </div>
              <div className="col-span-6 lg:col-span-2">
                <label>Unidad</label>
                <select
                  {...registerEditarInventario("IDUnidad", { valueAsNumber: true })}
                  className="w-full select"
                >
                  {unidades.map((unidad) => (
                    <option key={unidad.IDUnidad} value={unidad.IDUnidad}>
                      {unidad.Descripcion} ({unidad.Abreviacion})
                    </option>
                  ))}
                </select>
                {errorsEditarInventario.IDUnidad && (
                  <p className="text-sm text-error">
                    {errorsEditarInventario.IDUnidad.message}
                  </p>
                )}
              </div>
              <button type="submit" className="btn btn-primary col-span-6">
                Editar Producto
              </button>
            </form>
          </div>
        </div>
      </Modal>
    </>
  );
}

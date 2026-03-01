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
import {
  generarMovimientoSchema,
  type GenerarMovimientoFormData,
} from "~/form schemas/generarMovimientoSchema";
import type { DTOSucursal } from "DTOs/Sucursales";
import type { DTOLogInventario, DTOTipoMovimiento } from "DTOs/LogInventario";
import type { DTOSucursalInventario } from "DTOs/SucursalesInventario";
import { auth } from "services/auth";

export default function inventario() {
  // State
  const [inventario, setInventario] = useState<DTOInventario[]>([]);
  const [unidades, setUnidades] = useState<DTOUnidad[]>([]);
  const [sucursales, setSucursales] = useState<DTOSucursal[]>([]);
  const [sucursalSeleccionada, setSucursalSeleccionada] =
    useState<DTOSucursal>();
  const [tiposMovimiento, setTiposMovimiento] = useState<DTOTipoMovimiento[]>(
    [],
  );
  const [tipoMovimientoSeleccionado, setTipoMovimientoSeleccionado] =
    useState<DTOTipoMovimiento>();
  const [inventarioSucursal, setInventarioSucursal] = useState<
    DTOSucursalInventario[]
  >([]);
  const [inventarioSucursalSeleccionado, setInventarioSucursalSeleccionado] =
    useState<DTOSucursalInventario>();
  const [verModalCrear, setVerModalCrear] = useState<boolean>(false);
  const [verModalEditar, setVerModalEditar] = useState<boolean>(false);
  const [logInventarioPorSucursal, setLogInventarioPorSucursal] = useState<DTOLogInventario[]>([]);
  const [sucursalSeleccionadaLog, setSucursalSeleccionadaLog] = useState<DTOSucursal>();
  const [inventarioVisualizacionSucursal, setInventarioVisualizacionSucursal] = useState<DTOSucursalInventario[]>([]);
  const [sucursalSeleccionadaVisualizacion, setSucursalSeleccionadaVisualizacion] = useState<number>(0);
  // Redux
  const dispatch = useDispatch();

  // Effects
  useEffect(() => {
    dispatch(changeCurrentPage("Inventario"));
    GetInventario();
    GetUnidades();
    GetSucursales();
    GetTiposMovimiento();
    GetLogPorSucursal(0);
    GetInventarioVisualizacionPorSucursal(0);

    // Inicializar generar movimiento
    resetGenerarMovimiento({
      IDUsuario: parseInt(auth.getUserId() ?? "0"),
      IDSucursal: 0,
      IDTipoMovimiento: 0,
      NoParte: "0",
      Cantidad: 0,
    });
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
  const GetLogPorSucursal = (IDSucursal: number) => {
    api
      .GetLogPorSucursal(IDSucursal)
      .then((data) => {
        setLogInventarioPorSucursal(data);
      })
      .catch((error) => {
        toast.error("No se pudo cargar el log por sucursal");
      })
  };
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
  const GetTiposMovimiento = () => {
    api
      .LogInventarioGetTiposMovimiento()
      .then((data) => {
        setTiposMovimiento(data);
      })
      .catch((error) => {
        toast.error("Ocurrió un error al obtener los tipos de movimiento");
      });
  };
  const GetInventarioPorSucursal = (IDSucursal: number) => {
    api
      .SucursalesInventarioGetInventarioPorSucursal(IDSucursal)
      .then((data) => {
        setInventarioSucursal(data);
      })
      .catch((error) => {
        toast.error("Ocurrió un error al obtener los tipos de movimiento");
      });
  };
  const GetInventarioVisualizacionPorSucursal = (IDSucursal: number) => {
    const request =
      IDSucursal === 0
        ? api.SucursalesInventarioGetSucursalesInventario()
        : api.SucursalesInventarioGetInventarioPorSucursal(IDSucursal);
    request
      .then((data) => {
        setInventarioVisualizacionSucursal(data);
      })
      .catch(() => {
        toast.error("Ocurrió un error al obtener el inventario de la sucursal");
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
  const sucursalSeleccionadaChange = (IDSucursal: number) => {
    setSucursalSeleccionada(
      sucursales.find((s) => s.IDSucursal === IDSucursal),
    );
  };
  const sucursalSeleccionadaLogChange = (IDSucursal: number) => {
    setSucursalSeleccionadaLog(
      sucursales.find((s) => s.IDSucursal === IDSucursal),
    );
  };
  const tipoMovimientoSeleccionadoChange = (
    IDTipoMovimientoInventario: number,
  ) => {
    setTipoMovimientoSeleccionado(
      tiposMovimiento.find(
        (tm) => tm.IDTipoMovimientoInventario === IDTipoMovimientoInventario,
      ),
    );
  };
  const inventarioSucursalSeleccionadoChange = (NoParte: string) => {
    setInventarioSucursalSeleccionado(
      inventarioSucursal.find((is) => is.NoParte === NoParte),
    );
  };

  // Datagrid
  const columns: GridColDef[] = [
    { field: "NoParte", headerName: "No. Parte", width: 120 },
    { field: "NombreProducto", headerName: "Nombre", flex: 1, minWidth: 200 },
    {
      field: "DescripcionProducto",
      headerName: "Descripción",
      flex: 1,
      minWidth: 200,
    },
    {
      field: "Precio",
      headerName: "Precio",
      width: 100,
      renderCell: (cell) => `$${cell.row.Precio.toFixed(2)}`,
    },
    {
      field: "Costo",
      headerName: "Costo",
      width: 100,
      renderCell: (cell) => `$${cell.row.Costo.toFixed(2)}`,
    },
    { field: "DescripcionUnidad", headerName: "Unidad", width: 100 },
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
      width: 110,
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
  
  const columnsInventarioSucursal: GridColDef[] = [
    { field: "NoParte", headerName: "No. Parte", width: 130 },
    { field: "NombreProducto", headerName: "Nombre", flex: 1, minWidth: 180 },
    { field: "NombreSucursal", headerName: "Sucursal", flex: 1, minWidth: 150 },
    { field: "Unidad", headerName: "Unidad", width: 100 },
    {
      field: "Existencia",
      headerName: "Existencia",
      width: 120,
      renderCell: (cell) => (
        <span
          className={
            cell.row.Existencia <= cell.row.UmbralExistencia
              ? "text-error font-semibold"
              : ""
          }
        >
          {cell.row.Existencia}
        </span>
      ),
    },
    {
      field: "UmbralExistencia",
      headerName: "Umbral",
      width: 100,
    },
  ];

  const columnsLogSucursal: GridColDef[] = [
    { field: "IDLogInventario", headerName: "ID", width: 100 },
    { field: "NoParte", headerName: "No. Parte", width: 120 },
    { field: "Fecha", headerName: "Fecha", flex: 1, renderCell: (cell) => (`${new Date(cell.row.Fecha).toLocaleDateString()}`) },
    { field: "Cantidad", headerName: "Cantidad", flex: 1 },    
    { field: "DescripcionTipoMovimiento", headerName: "Tipo Movimiento", flex: 1 },    
    { field: "Sucursal", headerName: "Sucursal", flex: 1 },    
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

  // Generar Movimiento Form
  const {
    register: registerGenerarMovimiento,
    handleSubmit: handleSubmitGenerarMovimiento,
    formState: { errors: errorsGenerarMovimiento },
    reset: resetGenerarMovimiento,
  } = useForm<GenerarMovimientoFormData>({
    resolver: zodResolver(generarMovimientoSchema),
  });

  const onSubmitGenerarMovimiento = (formData: GenerarMovimientoFormData) => {
    api
      .LogInventarioCrearLogInventario(formData)
      .then(() => {
        toast.success("Se generó el movimiento de inventario");
        GetLogPorSucursal(sucursalSeleccionadaLog?.IDSucursal ?? 0);
        setInventarioSucursalSeleccionado(undefined);
        setSucursalSeleccionada(undefined);
        setTipoMovimientoSeleccionado(undefined);
        resetGenerarMovimiento({
          IDUsuario: formData.IDUsuario,
          IDSucursal: 0,
          IDTipoMovimiento: 0,
          NoParte: "0",
          Cantidad: 0,
        });
      })
      .catch(() => {
        toast.error("Hubo un error al generar el movimiento de inventario");
      });
  };

  const paginationModel = { page: 0, pageSize: 10 };
  return (
    <>
      {/* Componente principal */}
      <div className="w-full h-full py-4 grid grid-cols-1 gap-4">
        <div className="card bg-base-100">
          <div className="card-body">
            <h1 className="card-title">Generar Movimiento</h1>
            <form
              className="w-full grid grid-cols-2 gap-4"
              onSubmit={handleSubmitGenerarMovimiento(
                onSubmitGenerarMovimiento,
              )}
            >
              <div className="col-span-2 lg:col-span-1">
                <label>Sucursal</label>
                <select
                  {...registerGenerarMovimiento("IDSucursal", {
                    valueAsNumber: true,
                    onChange: (event) => {
                      const IDSucursal = parseInt(event.target.value);
                      sucursalSeleccionadaChange(IDSucursal);
                      GetInventarioPorSucursal(IDSucursal);
                    },
                  })}
                  defaultValue={0}
                  className="w-full select"
                >
                  <option value={0} disabled>
                    Seleccionar opción...
                  </option>
                  {sucursales.map((sucursal) => (
                    <option
                      key={sucursal.IDSucursal}
                      value={sucursal.IDSucursal}
                    >
                      {sucursal.Nombre}
                    </option>
                  ))}
                </select>
                {errorsGenerarMovimiento.IDSucursal && (
                  <p className="text-sm text-error">
                    {errorsGenerarMovimiento.IDSucursal.message}
                  </p>
                )}
              </div>
              <div className="col-span-2 lg:col-span-1">
                <label>Tipo de Movimiento</label>
                <select
                  {...registerGenerarMovimiento("IDTipoMovimiento", {
                    valueAsNumber: true,
                    onChange: (event) => {
                      tipoMovimientoSeleccionadoChange(
                        parseInt(event.target.value),
                      );
                    },
                  })}
                  className="w-full select"
                  defaultValue={0}
                >
                  <option value={0} disabled>
                    Seleccionar opción...
                  </option>
                  {tiposMovimiento.map((tipoMovimiento) => (
                    <option
                      key={tipoMovimiento.IDTipoMovimientoInventario}
                      value={tipoMovimiento.IDTipoMovimientoInventario}
                    >
                      {tipoMovimiento.Descripcion}
                    </option>
                  ))}
                </select>
                {errorsGenerarMovimiento.IDTipoMovimiento && (
                  <p className="text-sm text-error">
                    {errorsGenerarMovimiento.IDTipoMovimiento.message}
                  </p>
                )}
              </div>
              <div className="col-span-2 lg:col-span-1">
                <label>Producto</label>
                {tipoMovimientoSeleccionado?.EntradaSalida === true ? (
                  <select
                    {...registerGenerarMovimiento("NoParte")}
                    className="w-full select"
                    disabled={
                      !sucursalSeleccionada || !tipoMovimientoSeleccionado
                    }
                    defaultValue={0}
                  >
                    <option value={0} disabled>
                      Seleccionar opción...
                    </option>
                    {inventario.map((producto) => (
                      <option key={producto.NoParte} value={producto.NoParte}>
                        {producto.NoParte} | {producto.NombreProducto}
                      </option>
                    ))}
                  </select>
                ) : (
                  <select
                    {...registerGenerarMovimiento("NoParte", {
                      onChange: (event) => {
                        inventarioSucursalSeleccionadoChange(
                          event.target.value,
                        );
                      },
                    })}
                    className="w-full select"
                    disabled={
                      !sucursalSeleccionada || !tipoMovimientoSeleccionado
                    }
                    defaultValue={0}
                  >
                    <option value={0} disabled>
                      Seleccionar opción...
                    </option>
                    {inventarioSucursal.map((producto) => (
                      <option key={producto.NoParte} value={producto.NoParte}>
                        {producto.NoParte} | {producto.NombreProducto}
                      </option>
                    ))}
                  </select>
                )}
                {errorsGenerarMovimiento.NoParte && (
                  <p className="text-sm text-error">
                    {errorsGenerarMovimiento.NoParte.message}
                  </p>
                )}
              </div>
              <div className="col-span-2 lg:col-span-1">
                <label>Cantidad</label>
                {tipoMovimientoSeleccionado?.EntradaSalida === true ? (
                  <input
                    {...registerGenerarMovimiento("Cantidad", {
                      valueAsNumber: true,
                    })}
                    type="number"
                    className=" w-full input"
                    placeholder="1.11"
                  />
                ) : (
                  <>
                    <input
                      {...registerGenerarMovimiento("Cantidad", {
                        valueAsNumber: true,
                      })}
                      type="number"
                      className=" w-full input"
                      placeholder="1.11"
                      max={inventarioSucursalSeleccionado?.Existencia}
                    />
                    {inventarioSucursalSeleccionado && (
                      <p className="text-sm text-warning">
                        {inventarioSucursalSeleccionado?.Existencia}
                      </p>
                    )}
                  </>
                )}
                {errorsGenerarMovimiento.Cantidad && (
                  <p className="text-sm text-error">
                    {errorsGenerarMovimiento.Cantidad.message}
                  </p>
                )}
              </div>
              <button type="submit" className="btn btn-primary col-span-2">
                Generar Movimiento
              </button>
            </form>
          </div>
        </div>
        <div className="card bg-base-100">
          <div className="card-body">
            <h1 className="card-title">Productos Inventario</h1>
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
        <div className="card bg-base-100">
          <div className="card-body">
            <h1 className="card-title">Inventario por Sucursal</h1>
            <div className="flex justify-end">
              <div className="w-2/5 min-w-50">
                <label>Sucursal</label>
                <select
                  onChange={(event) => {
                    const id = Number(event.target.value);
                    setSucursalSeleccionadaVisualizacion(id);
                    GetInventarioVisualizacionPorSucursal(id);
                  }}
                  value={sucursalSeleccionadaVisualizacion}
                  className="w-full select"
                >
                  <option value={0}>Todas</option>
                  {sucursales.map((sucursal) => (
                    <option
                      key={sucursal.IDSucursal}
                      value={sucursal.IDSucursal}
                    >
                      {sucursal.Nombre}
                    </option>
                  ))}
                </select>
              </div>
            </div>
            <DataGrid
              rows={inventarioVisualizacionSucursal}
              columns={columnsInventarioSucursal}
              initialState={{ pagination: { paginationModel } }}
              pageSizeOptions={[5, 10]}
              rowSelection={false}
              getRowId={(row: DTOSucursalInventario) => row.IDSucursalInventario}
              sx={{ border: 0 }}
            />
          </div>
        </div>
        <div className="card bg-base-100">
          <div className="card-body">
            <h1 className="card-title">Log Inventario</h1>
            <div className="flex justify-end">
              <div className="w-2/5 min-w-50">
                <label>Sucursal</label>
                  <select
                    onChange={
                      (event) => {
                        GetLogPorSucursal(Number(event.target.value));
                        sucursalSeleccionadaLogChange(Number(event.target.value))
                      }
                    }
                    defaultValue={0}
                    value={sucursalSeleccionada?.IDSucursal}
                    className="w-full select"
                  >
                    <option value={0}>
                      Todas
                    </option>
                    {sucursales.map((sucursal) => (
                      <option
                        key={sucursal.IDSucursal}
                        value={sucursal.IDSucursal}
                      >
                        {sucursal.Nombre}
                      </option>
                    ))}
                  </select>
              </div>
            </div>
            <DataGrid
              rows={logInventarioPorSucursal}
              columns={columnsLogSucursal}
              initialState={{ pagination: { paginationModel } }}
              pageSizeOptions={[5, 10]}
              rowSelection={false}
              getRowId={(row: DTOLogInventario) => row.IDLogInventario}
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
                  {...registerCrearInventario("Precio", {
                    valueAsNumber: true,
                  })}
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
                  {...registerCrearInventario("IDUnidad", {
                    valueAsNumber: true,
                  })}
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
                  {...registerEditarInventario("Precio", {
                    valueAsNumber: true,
                  })}
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
                  {...registerEditarInventario("Costo", {
                    valueAsNumber: true,
                  })}
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
                  {...registerEditarInventario("IDUnidad", {
                    valueAsNumber: true,
                  })}
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

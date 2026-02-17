import type {
  DTOAlertasInventarioInicio,
  DTOMovimientosRecientesInicio,
  DTOTotalesInicio,
  DTOVentasVsComprasInicio,
} from "DTOs/Inicio";
import { useEffect, useState } from "react";
import { useDispatch } from "react-redux";
import { toast } from "react-toastify";
import { api } from "services/api";
import { changeCurrentPage } from "services/slices/currentPageSlice";
import { formatCurrency } from "~/util/format";

export default function inicio() {
  // State
  const [totales, setTotales] = useState<DTOTotalesInicio>();
  const [movimientosRecientes, setMovimientosRecientes] =
    useState<DTOMovimientosRecientesInicio[]>();
  const [ventasVsCompras, setVentasVsCompras] =
    useState<DTOVentasVsComprasInicio>();
  const [alertasInventario, setAlertasInventario] =
    useState<DTOAlertasInventarioInicio[]>();

  // Redux
  const dispatch = useDispatch();
  
  // Cargar datos
  useEffect(() => {
    dispatch(changeCurrentPage("Inicio"))
    // Totales
    api
      .InicioGetTotales()
      .then((data) => {
        setTotales(data);
      })
      .catch(() => {
        toast.error("Ocurrió un error al obtener los totales");
      });
    // Movimientos Recientes
    api
      .InicioGetMovimientosRecientes()
      .then((data) => {
        setMovimientosRecientes(data);
      })
      .catch(() => {
        toast.error("Ocurrió un error al obtener los movimientos recientes");
      });
    // Ventas vs Compras
    api
      .InicioGetVentasVsCompras()
      .then((data) => {
        setVentasVsCompras(data);
      })
      .catch(() => {
        toast.error("Ocurrió un error al obtener la información de ventas vs compras");
      });
    // Alertas Inventario
    api
      .InicioGetAlertasInventario()
      .then((data) => {
        setAlertasInventario(data);
      })
      .catch(() => {
        toast.error("Ocurrió un error al obtener las alertas de inventario");
      });
  }, []);

  return (
    <div className="w-full h-full py-4">
      {/* Totales */}
      <div className="w-full grid grid-cols-3 gap-4">
        {/* Total ventas */}
        <div className="col-span-3 lg:col-span-1 card bg-base-100 shadow">
          <div className="card-body flex flex-row justify-between">
            <div className="flex flex-col">
              <span>Total Ventas</span>
              <span className="card-title">
                {formatCurrency(totales?.Ventas)}
              </span>
            </div>
            <div className="bg-primary text-primary-content rounded-xl flex justify-center items-center h-fit aspect-square p-2">
              <i className="material-symbols-outlined">sell</i>
            </div>
          </div>
        </div>
        {/* Total Compras */}
        <div className="col-span-3 lg:col-span-1 card bg-base-100 shadow">
          <div className="card-body flex flex-row justify-between">
            <div className="flex flex-col">
              <span>Total Compras</span>
              <span className="card-title">
                {formatCurrency(totales?.Compras)}
              </span>
            </div>
            <div className="bg-primary text-primary-content rounded-xl flex justify-center items-center h-fit aspect-square p-2">
              <i className="material-symbols-outlined">wallet</i>
            </div>
          </div>
        </div>
        {/* Total Merma */}
        <div className="col-span-3 lg:col-span-1 card bg-base-100 shadow">
          <div className="card-body flex flex-row justify-between">
            <div className="flex flex-col">
              <span>Total Merma</span>
              <span className="card-title">
                {formatCurrency(totales?.Merma)}
              </span>
            </div>
            <div className="bg-primary text-primary-content rounded-xl flex justify-center items-center h-fit aspect-square p-2">
              <i className="material-symbols-outlined">money_off</i>
            </div>
          </div>
        </div>
      </div>
      {/* Movimientos recientes - Ventas vs Compras */}
      <div className="w-full grid grid-cols-5 gap-4 mt-4">
        <div className="col-span-5 lg:col-span-2 card bg-base-100 shadow">
          <div className="card-body">
            <h1 className="card-title">Movimientos Recientes</h1>
          </div>
        </div>
        <div className="col-span-5 lg:col-span-3 card bg-base-100 shadow">
          <div className="card-body">
            <h1 className="card-title">Ventas vs Compras</h1>
          </div>
        </div>
      </div>
      {/* Alertas Inventario */}
      <div className="w-full mt-4">
        <div className="w-full card bg-base-100 shadow">
          <div className="card-body">
            <h1 className="card-title">Alertas Inventario</h1>
          </div>
        </div>
      </div>
    </div>
  );
}

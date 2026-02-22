import { useEffect, useState } from "react";
import "./Sidebar.css";
import { NavLink } from "react-router";
import { api } from "services/api";
import type { DTOModulosAcceso } from "DTOs/ModulosAcceso";
import { auth } from "services/auth";
import { toast } from "react-toastify";

type modulosCategorias = {
  NombreModuloCategoria: string;
  IDModuloCategoria: number;
  Modulos: DTOModulosAcceso[];
}[];

export default function Sidebar() {
  const APP_NAME = import.meta.env.VITE_APP_NAME;

  // State
  const [modulosCategorias, setModulosCategorias] = useState<modulosCategorias>(
    [],
  );

  // Effect
  useEffect(() => {
    const idUsuario = parseInt(auth.getUserId() ?? "0");
    api
      .GetModulosAccesoUsuario(idUsuario)
      .then((data) => {
        const categorias: modulosCategorias = [];
        data.forEach((ma) => {
          const categoria = categorias.find(
            (categoria) => categoria.IDModuloCategoria === ma.IDModuloCategoria,
          );
          if (categoria) {
            categoria.Modulos.push(ma);
          } else {
            categorias.push({
              IDModuloCategoria: ma.IDModuloCategoria,
              NombreModuloCategoria: ma.NombreModuloCategoria,
              Modulos: [ma],
            });
          }
        });
        setModulosCategorias(categorias);
      })
      .catch((error) => {
        toast.error("Hubo un error al cargar los módulos con acceso");
      });
  }, []);

  // Util
  const formatRoute = (nombre: string) => {
    return nombre.toLocaleLowerCase().replaceAll(" ", "-");
  };

  return (
    <nav className="flex flex-col p-5 gap-5 bg-base-200 min-h-full w-68">
      <div className="w-full flex items-center justify-center gap-4 px-4 py-6 border-b font-bold">
        <i className="material-symbols-outlined w-6">dashboard</i>
        <span>{APP_NAME}</span>
      </div>
      <ul>
        <li>
          <NavLink
            to="/dashboard/inicio"
            className={({ isActive }) =>
              isActive ? "sidebar-item active" : "sidebar-item"
            }
          >
            <div className="sidebar-item--icon">
              <i className="material-symbols-outlined">home</i>
            </div>
            <span className="sidebar-item--text">Inicio</span>
          </NavLink>
        </li>
      </ul>
      {modulosCategorias.map((mc) => (
        <div key={mc.IDModuloCategoria}>
          <span className="font-bold">{mc.NombreModuloCategoria}</span>
          <ul>
            {mc.Modulos.map((ma) => (
              <li key={ma.IDModuloAcceso}>
                <NavLink
                  to={`/dashboard/${formatRoute(ma.NombreModulo)}`}
                  className={({ isActive }) =>
                    isActive ? "sidebar-item active" : "sidebar-item"
                  }
                >
                  <div className="sidebar-item--icon">
                    <i className="material-symbols-outlined">
                      {ma.IconoModulo}
                    </i>
                  </div>
                  <span className="sidebar-item--text">{ma.NombreModulo}</span>
                </NavLink>
              </li>
            ))}
          </ul>
        </div>
      ))}
      <ul></ul>
    </nav>
  );
}

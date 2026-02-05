import React from "react";
import "./Sidebar.css";
import { Link } from "react-router";
import { NavLink } from "react-router";

export default function Sidebar() {
  const APP_NAME = import.meta.env.VITE_APP_NAME;

  return (
    <nav className="w-full h-full flex flex-col p-5 gap-5">
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
      <span className="font-bold">ADMINISTRACIÓN</span>
      <ul>
        <li>
          <NavLink
            to="/dashboard/usuarios"
            className={({ isActive }) =>
              isActive ? "sidebar-item active" : "sidebar-item"
            }
          >
            <div className="sidebar-item--icon">
              <i className="material-symbols-outlined">group</i>
            </div>
            <span className="sidebar-item--text">Usuarios</span>
          </NavLink>
        </li>
        <li>
          <NavLink
            to="/dashboard/perfiles-puesto"
            className={({ isActive }) =>
              isActive ? "sidebar-item active" : "sidebar-item"
            }
          >
            <div className="sidebar-item--icon">
              <i className="material-symbols-outlined">
                supervised_user_circle
              </i>
            </div>
            <span className="sidebar-item--text">Perfiles de Puesto</span>
          </NavLink>
        </li>
      </ul>
      <span className="font-bold">INVENTARIO</span>
      <ul>
        <li>
          <NavLink
            to="/dashboard/inventario"
            className={({ isActive }) =>
              isActive ? "sidebar-item active" : "sidebar-item"
            }
          >
            <div className="sidebar-item--icon">
              <i className="material-symbols-outlined">inventory</i>
            </div>
            <span className="sidebar-item--text">Inventario</span>
          </NavLink>
        </li>
        <li>
          <NavLink
            to="/dashboard/sucursales"
            className={({ isActive }) =>
              isActive ? "sidebar-item active" : "sidebar-item"
            }
          >
            <div className="sidebar-item--icon">
              <i className="material-symbols-outlined">warehouse</i>
            </div>
            <span className="sidebar-item--text">Sucursales</span>
          </NavLink>
        </li>
      </ul>
    </nav>
  );
}

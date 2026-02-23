import axios from "axios";
import {
  showLoader,
  hideLoader,
  incrementCount,
  decrementCount,
} from "./slices/loadingSlice";
import { auth } from "./auth";
import { store } from "./store";
import type { DTOIniciarSesion, DTOSesion } from "DTOs/Login";
import type { DTOUsuario, DTOActualizarUsuario, DTOCrearUsuario } from "DTOs/Usuarios";
import type {
  DTOAlertasInventarioInicio,
  DTOMovimientosRecientesInicio,
  DTOTotalesInicio,
  DTOVentasVsComprasInicio,
} from "DTOs/Inicio";
import type { DTOActualizarPerfilPuesto, DTOCrearPerfilPuesto, DTOPerfilPuesto } from "DTOs/PerfilesPuesto";
import type { DTOActualizarSucursal, DTOCrearSucursal, DTOSucursal } from "DTOs/Sucursales";
import type { DTOActualizarInventario, DTOCrearInventario, DTOInventario } from "DTOs/Inventario";
import type { DTOUnidad } from "DTOs/Unidades";
import type { DTOModulo, DTOModulosAcceso, DTONivel, DTORegistrarAccesoModulo } from "DTOs/ModulosAcceso";
import type { DTOCrearUsuarioSucursal, DTOUsuarioSucursal } from "DTOs/UsuarioSucursal";

const API_URL = import.meta.env.VITE_API_URL;
const apiClient = axios.create({
  baseURL: API_URL,
  timeout: 60000,
});

const authExcludedURLs = ["/Login/IniciarSesion"];

// Lista de URLs que no deben mostrar el loader
const noLoaderURLs: string[] = [];

// Extender la interfaz de configuración de Axios para incluir la propiedad skipLoader
declare module "axios" {
  export interface AxiosRequestConfig {
    skipLoader?: boolean;
  }
}

// Interceptor para incluir el token en las solicitudes
apiClient.interceptors.request.use(
  async (config) => {
    // Agregar el token a las requests que no están en la lista de excluidas
    if (!authExcludedURLs.some((url) => url === config.url)) {
      const token = auth.getToken();
      if (token) {
        config.headers["Authorization"] = `Bearer ${token}`;
      }
    }
    // Agregar header de ngrok
    config.headers["ngrok-skip-browser-warning"] = `true`;

    // Verificar si se debe mostrar el loader
    const shouldSkipLoader =
      config.skipLoader === true ||
      noLoaderURLs.some((url) => config.url?.includes(url));

    // Mostrar carga solo si no está en la lista de exclusión
    if (!shouldSkipLoader) {
      store.dispatch(showLoader());
      store.dispatch(incrementCount());
    }

    return config;
  },
  (error) => {
    return Promise.reject(error);
  },
);

// Interceptor para manejar la respuesta y actualizar el token renovado
apiClient.interceptors.response.use(
  async (response) => {
    // Verificar si el servidor envió un token renovado en los encabezados
    const renewedToken = response.headers["renewed-token"];
    if (renewedToken) {
      auth.setToken(renewedToken);
    }

    // Verificar si se debe ocultar el loader
    const shouldSkipLoader =
      response.config.skipLoader === true ||
      noLoaderURLs.some((url) => response.config.url?.includes(url));

    // Ocultar carga solo si no está en la lista de exclusión
    if (!shouldSkipLoader) {
      store.dispatch(decrementCount());
      const count = store.getState().loading.requestCount;
      if (count == 0) {
        store.dispatch(hideLoader());
      }
    }
    return response;
  },
  async (error) => {
    //Verificar si el error es un 401 Unauthorized o si no hay repuesta por la política de CORS sin auth
    if (
      error.response?.status === 401 &&
      !window.location.href.includes("/login")
    ) {
      // Cerrar la sesión
      window.location.href = "/login";

      return Promise.reject({
        message: "Sesión expirada. Por favor, inicie sesión nuevamente.",
        originalError: error,
      });
    }

    // Verificar si se debe ocultar el loader en caso de error
    const shouldSkipLoader =
      error.config?.skipLoader === true ||
      noLoaderURLs.some((url) => error.config?.url?.includes(url));

    // Manejo del resto de los errores
    if (!shouldSkipLoader) {
      store.dispatch(decrementCount());
      const count = store.getState().loading.requestCount;
      if (count == 0) {
        store.dispatch(hideLoader());
      }
    }
    return Promise.reject(error);
  },
);

export const api = {
  // Login
  Login: async (data: DTOIniciarSesion): Promise<DTOSesion> => {
    try {
      return apiClient
        .request({
          url: `${API_URL}/Login/IniciarSesion`,
          method: "post",
          data: data,
        })
        .then((res) => res.data);
    } catch (error: any) {
      console.error(error.message);
      throw error;
    }
  },
  // Inicio
  InicioGetAlertasInventario: async (): Promise<
    DTOAlertasInventarioInicio[]
  > => {
    try {
      return apiClient
        .request({
          url: `${API_URL}/Inicio/GetAlertasInventario`,
          method: "get",
        })
        .then((res) => res.data);
    } catch (error: any) {
      console.error(error.message);
      throw error;
    }
  },
  InicioGetTotales: async (): Promise<DTOTotalesInicio> => {
    try {
      return apiClient
        .request({
          url: `${API_URL}/Inicio/GetTotales`,
          method: "get",
        })
        .then((res) => res.data);
    } catch (error: any) {
      console.error(error.message);
      throw error;
    }
  },
  InicioGetMovimientosRecientes: async (): Promise<
    DTOMovimientosRecientesInicio[]
  > => {
    try {
      return apiClient
        .request({
          url: `${API_URL}/Inicio/GetMovimientosRecientes`,
          method: "get",
        })
        .then((res) => res.data);
    } catch (error: any) {
      console.error(error.message);
      throw error;
    }
  },
  InicioGetVentasVsCompras: async (): Promise<DTOVentasVsComprasInicio> => {
    try {
      return apiClient
        .request({
          url: `${API_URL}/Inicio/GetVentasVsCompras`,
          method: "get",
        })
        .then((res) => res.data);
    } catch (error: any) {
      console.error(error.message);
      throw error;
    }
  },
  // Usuarios
  UsuariosGetUsuarios: async (): Promise<DTOUsuario[]> => {
    try {
      return apiClient
        .request({
          url: `${API_URL}/Usuarios/GetUsuarios`,
          method: "get",
        })
        .then((res) => res.data);
    } catch (error: any) {
      console.error(error.message);
      throw error;
    }
  },
  UsuariosGetUsuario: async (IDUsuario: number): Promise<DTOUsuario> => {
    try {
      return apiClient
        .request({
          url: `${API_URL}/Usuarios/GetUsuario`,
          method: "get",
          params: { IDUsuario },
        })
        .then((res) => res.data);
    } catch (error: any) {
      console.error(error.message);
      throw error;
    }
  },
  UsuariosCrearUsuario: async (data: DTOCrearUsuario): Promise<DTOUsuario> => {
    try {
      return apiClient
        .request({
          url: `${API_URL}/Usuarios/CrearUsuario`,
          method: "post",
          data: data,
        })
        .then((res) => res.data);
    } catch (error: any) {
      console.error(error.message);
      throw error;
    }
  },
  UsuariosActualizarUsuario: async (params: DTOActualizarUsuario): Promise<DTOUsuario> => {
    try {
      return apiClient
        .request({
          url: `${API_URL}/Usuarios/ActualizarUsuario`,
          method: "put",
          data: params,
        })
        .then((res) => res.data);
    } catch (error: any) {
      console.error(error.message);
      throw error;
    }
  },
  UsuariosInhabilitarUsuario: async (IDUsuario: number): Promise<void> => {
    try {
      return apiClient
        .request({
          url: `${API_URL}/Usuarios/InhabilitarUsuario`,
          method: "put",
          params: { IDUsuario },
        })
        .then((res) => res.data);
    } catch (error: any) {
      console.error(error.message);
      throw error;
    }
  },
  UsuariosHabilitarUsuario: async (IDUsuario: number): Promise<void> => {
    try {
      return apiClient
        .request({
          url: `${API_URL}/Usuarios/HabilitarUsuario`,
          method: "put",
          params: { IDUsuario },
        })
        .then((res) => res.data);
    } catch (error: any) {
      console.error(error.message);
      throw error;
    }
  },
  // Perfiles Puesto
  PerfilesPuestoGetPerfilesPuestoActivos: async (): Promise<DTOPerfilPuesto[]> => {
    try {
      return apiClient
        .request({
          url: `${API_URL}/PerfilesPuesto/GetPerfilesPuestoActivos`,
          method: "get",
        })
        .then((res) => res.data);
    } catch (error: any) {
      console.error(error.message);
      throw error;
    }
  },
  PerfilesPuestoGetPerfilesPuesto: async (): Promise<DTOPerfilPuesto[]> => {
    try {
      return apiClient
        .request({
          url: `${API_URL}/PerfilesPuesto/GetPerfilesPuesto`,
          method: "get",
        })
        .then((res) => res.data);
    } catch (error: any) {
      console.error(error.message);
      throw error;
    }
  },
  PerfilesPuestoGetPerfilPuesto: async (IDPerfilPuesto: number): Promise<DTOPerfilPuesto> => {
    try {
      return apiClient
        .request({
          url: `${API_URL}/PerfilesPuesto/GetPerfilPuesto`,
          method: "get",
          params: { IDPerfilPuesto },
        })
        .then((res) => res.data);
    } catch (error: any) {
      console.error(error.message);
      throw error;
    }
  },
  PerfilesPuestoCrearPerfilPuesto: async (data: DTOCrearPerfilPuesto): Promise<DTOPerfilPuesto> => {
    try {
      return apiClient
        .request({
          url: `${API_URL}/PerfilesPuesto/CrearPerfilPuesto`,
          method: "post",
          data: data,
        })
        .then((res) => res.data);
    } catch (error: any) {
      console.error(error.message);
      throw error;
    }
  },
  PerfilesPuestoActualizarPerfilPuesto: async (params: DTOActualizarPerfilPuesto): Promise<DTOPerfilPuesto> => {
    try {
      return apiClient
        .request({
          url: `${API_URL}/PerfilesPuesto/ActualizarPerfilPuesto`,
          method: "put",
          data: params,
        })
        .then((res) => res.data);
    } catch (error: any) {
      console.error(error.message);
      throw error;
    }
  },
  PerfilesPuestoInhabilitarPerfilPuesto: async (IDPerfilPuesto: number): Promise<void> => {
    try {
      return apiClient
        .request({
          url: `${API_URL}/PerfilesPuesto/InhabilitarPerfilPuesto`,
          method: "put",
          params: { IDPerfilPuesto },
        })
        .then((res) => res.data);
    } catch (error: any) {
      console.error(error.message);
      throw error;
    }
  },
  PerfilesPuestoHabilitarPerfilPuesto: async (IDPerfilPuesto: number): Promise<void> => {
    try {
      return apiClient
        .request({
          url: `${API_URL}/PerfilesPuesto/HabilitarPerfilPuesto`,
          method: "put",
          params: { IDPerfilPuesto },
        })
        .then((res) => res.data);
    } catch (error: any) {
      console.error(error.message);
      throw error;
    }
  },
  // Sucursales
  SucursalesGetSucursales: async (): Promise<DTOSucursal[]> => {
    try {
      return apiClient
        .request({
          url: `${API_URL}/Sucursales/GetSucursales`,
          method: "get",
        })
        .then((res) => res.data);
    } catch (error: any) {
      console.error(error.message);
      throw error;
    }
  },
  SucursalesGetSucursal: async (IDSucursal: number): Promise<DTOSucursal> => {
    try {
      return apiClient
        .request({
          url: `${API_URL}/Sucursales/GetSucursal`,
          method: "get",
          params: { IDSucursal },
        })
        .then((res) => res.data);
    } catch (error: any) {
      console.error(error.message);
      throw error;
    }
  },
  SucursalesCrearSucursal: async (data: DTOCrearSucursal): Promise<DTOSucursal> => {
    try {
      return apiClient
        .request({
          url: `${API_URL}/Sucursales/CrearSucursal`,
          method: "post",
          data: data,
        })
        .then((res) => res.data);
    } catch (error: any) {
      console.error(error.message);
      throw error;
    }
  },
  SucursalesActualizarSucursal: async (params: DTOActualizarSucursal): Promise<DTOSucursal> => {
    try {
      return apiClient
        .request({
          url: `${API_URL}/Sucursales/ActualizarSucursal`,
          method: "put",
          data: params,
        })
        .then((res) => res.data);
    } catch (error: any) {
      console.error(error.message);
      throw error;
    }
  },
  SucursalesInhabilitarSucursal: async (IDSucursal: number): Promise<void> => {
    try {
      return apiClient
        .request({
          url: `${API_URL}/Sucursales/InhabilitarSucursal`,
          method: "put",
          params: { IDSucursal },
        })
        .then((res) => res.data);
    } catch (error: any) {
      console.error(error.message);
      throw error;
    }
  },
  SucursalesHabilitarSucursal: async (IDSucursal: number): Promise<void> => {
    try {
      return apiClient
        .request({
          url: `${API_URL}/Sucursales/HabilitarSucursal`,
          method: "put",
          params: { IDSucursal },
        })
        .then((res) => res.data);
    } catch (error: any) {
      console.error(error.message);
      throw error;
    }
  },
  // Inventario
  InventarioGetInventario: async (): Promise<DTOInventario[]> => {
    try {
      return apiClient
        .request({
          url: `${API_URL}/Inventario/GetInventario`,
          method: "get",
        })
        .then((res) => res.data);
    } catch (error: any) {
      console.error(error.message);
      throw error;
    }
  },
  InventarioGetProducto: async (NoParte: string): Promise<DTOInventario> => {
    try {
      return apiClient
        .request({
          url: `${API_URL}/Inventario/GetProducto`,
          method: "get",
          params: { NoParte },
        })
        .then((res) => res.data);
    } catch (error: any) {
      console.error(error.message);
      throw error;
    }
  },
  InventarioCrearProducto: async (data: DTOCrearInventario): Promise<DTOInventario> => {
    try {
      return apiClient
        .request({
          url: `${API_URL}/Inventario/CrearProducto`,
          method: "post",
          data: data,
        })
        .then((res) => res.data);
    } catch (error: any) {
      console.error(error.message);
      throw error;
    }
  },
  InventarioActualizarProducto: async (params: DTOActualizarInventario): Promise<DTOInventario> => {
    try {
      return apiClient
        .request({
          url: `${API_URL}/Inventario/ActualizarProducto`,
          method: "put",
          data: params,
        })
        .then((res) => res.data);
    } catch (error: any) {
      console.error(error.message);
      throw error;
    }
  },
  InventarioInhabilitarProducto: async (NoParte: string): Promise<void> => {
    try {
      return apiClient
        .request({
          url: `${API_URL}/Inventario/InhabilitarProducto`,
          method: "put",
          params: { NoParte },
        })
        .then((res) => res.data);
    } catch (error: any) {
      console.error(error.message);
      throw error;
    }
  },
  InventarioHabilitarProducto: async (NoParte: string): Promise<void> => {
    try {
      return apiClient
        .request({
          url: `${API_URL}/Inventario/HabilitarProducto`,
          method: "put",
          params: { NoParte },
        })
        .then((res) => res.data);
    } catch (error: any) {
      console.error(error.message);
      throw error;
    }
  },
  // Unidades
  UnidadesGetUnidades: async (): Promise<DTOUnidad[]> => {
    try {
      return apiClient
        .request({
          url: `${API_URL}/Unidades/GetUnidades`,
          method: "get",
        })
        .then((res) => res.data);
    } catch (error: any) {
      console.error(error.message);
      throw error;
    }
  },
  // Modulos Acceso
  GetModulos: async (): Promise<DTOModulo[]> => {
    try {
      return apiClient
        .request({
          url: `${API_URL}/ModulosAcceso/GetModulos`,
          method: "get",
        })
        .then((res) => res.data);
    } catch (error: any) {
      console.error(error.message);
      throw error;
    }
  },
  GetNiveles: async (): Promise<DTONivel[]> => {
    try {
      return apiClient
        .request({
          url: `${API_URL}/ModulosAcceso/GetNiveles`,
          method: "get",
        })
        .then((res) => res.data);
    } catch (error: any) {
      console.error(error.message);
      throw error;
    }
  },
  GetModulosAccesoPerfilPuesto: async (IDPerfilPuesto: number): Promise<DTOModulosAcceso[]> => {
    try {
      return apiClient
        .request({
          url: `${API_URL}/ModulosAcceso/GetModulosAccesoPerfilPuesto`,
          method: "get",
          params: {IDPerfilPuesto}
        })
        .then((res) => res.data);
    } catch (error: any) {
      console.error(error.message);
      throw error;
    }
  },
  GetModulosAccesoUsuario: async (IDUsuario: number): Promise<DTOModulosAcceso[]> => {
    try {
      return apiClient
        .request({
          url: `${API_URL}/ModulosAcceso/GetModulosAccesoUsuario`,
          method: "get",
          params: {IDUsuario}
        })
        .then((res) => res.data);
    } catch (error: any) {
      console.error(error.message);
      throw error;
    }
  },
  
  RegistrarAccesoModulo: async (data: DTORegistrarAccesoModulo): Promise<DTOModulosAcceso> => {
    try {
      return apiClient
        .request({
          url: `${API_URL}/ModulosAcceso/RegistrarAccesoModulo`,
          method: "post",
          data: data
        })
        .then((res) => res.data);
    } catch (error: any) {
      console.error(error.message);
      throw error;
    }
  },
  EliminarAccesoModulo: async (IDModuloAcceso: number): Promise<DTOModulosAcceso> => {
    try {
      return apiClient
        .request({
          url: `${API_URL}/ModulosAcceso/EliminarAccesoModulo`,
          method: "delete",
          params: {IDModuloAcceso}
        })
        .then((res) => res.data);
    } catch (error: any) {
      console.error(error.message);
      throw error;
    }
  },
  // Usuario Sucursal
  UsuarioSucursalGetUsuariosPorSucursal: async (IDSucursal: number): Promise<DTOUsuarioSucursal[]> => {
    try {
      return apiClient
        .request({
          url: `${API_URL}/UsuarioSucursal/GetUsuariosPorSucursal`,
          method: "get",
          params: { IDSucursal },
        })
        .then((res) => res.data);
    } catch (error: any) {
      console.error(error.message);
      throw error;
    }
  },
  UsuarioSucursalCrearUsuarioSucursal: async (data: DTOCrearUsuarioSucursal): Promise<DTOUsuarioSucursal> => {
    try {
      return apiClient
        .request({
          url: `${API_URL}/UsuarioSucursal/CrearUsuarioSucursal`,
          method: "post",
          data: data,
        })
        .then((res) => res.data);
    } catch (error: any) {
      console.error(error.message);
      throw error;
    }
  },
  UsuarioSucursalEliminarUsuarioSucursal: async (IDSucursalUsuario: number): Promise<boolean> => {
    try {
      return apiClient
        .request({
          url: `${API_URL}/UsuarioSucursal/EliminarUsuarioSucursal`,
          method: "delete",
          params: { IDSucursalUsuario },
        })
        .then((res) => res.data);
    } catch (error: any) {
      console.error(error.message);
      throw error;
    }
  },
  UsuarioSucursalInhabilitarUsuarioSucursal: async (IDSucursalUsuario: number): Promise<DTOUsuarioSucursal> => {
    try {
      return apiClient
        .request({
          url: `${API_URL}/UsuarioSucursal/InhabilitarUsuarioSucursal`,
          method: "put",
          params: { IDSucursalUsuario },
        })
        .then((res) => res.data);
    } catch (error: any) {
      console.error(error.message);
      throw error;
    }
  },
  UsuarioSucursalHabilitarUsuarioSucursal: async (IDSucursalUsuario: number): Promise<DTOUsuarioSucursal> => {
    try {
      return apiClient
        .request({
          url: `${API_URL}/UsuarioSucursal/HabilitarUsuarioSucursal`,
          method: "put",
          params: { IDSucursalUsuario },
        })
        .then((res) => res.data);
    } catch (error: any) {
      console.error(error.message);
      throw error;
    }
  },
};

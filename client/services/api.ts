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
  Login: async (params: DTOIniciarSesion): Promise<DTOSesion> => {
    try {
      return apiClient
        .request({
          url: `${API_URL}/Login/IniciarSesion`,
          method: "post",
          data: params,
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
  UsuariosCrearUsuario: async (params: DTOCrearUsuario): Promise<DTOUsuario> => {
    try {
      return apiClient
        .request({
          url: `${API_URL}/Usuarios/CrearUsuario`,
          method: "post",
          data: params,
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
  PerfilesPuestoCrearPerfilPuesto: async (params: DTOCrearPerfilPuesto): Promise<DTOPerfilPuesto> => {
    try {
      return apiClient
        .request({
          url: `${API_URL}/PerfilesPuesto/CrearPerfilPuesto`,
          method: "post",
          data: params,
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
};

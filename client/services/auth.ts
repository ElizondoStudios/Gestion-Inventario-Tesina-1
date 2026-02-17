export const auth = {
  setToken: (token: string) => {
    localStorage.setItem("token", token);
  },
  getToken: () => {
    return localStorage.getItem("token");
  },
  deleteToken: () => {
    localStorage.removeItem("token");
  },
  setIDUsuario: (idUsuario: number) => {
    localStorage.setItem("idUsuario", `${idUsuario}`);
  },
  getUserId: () => {
    return localStorage.getItem("idUsuario");
  },
  deleteUserId: () => {
    localStorage.removeItem("idUsuario");
  },
  setNombre: (nombre: string) => {
    localStorage.setItem("nombre", nombre);
  },
  getNombre: () => {
    return localStorage.getItem("nombre");
  },
  deleteNombre: () => {
    localStorage.removeItem("nombre");
  },
  // Método para limpiar toda la sesión
  clearSession: () => {
    localStorage.removeItem("token");
    localStorage.removeItem("idUsuario");
    localStorage.removeItem("nombre");
  },
};
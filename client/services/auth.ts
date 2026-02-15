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
    localStorage.setItem("userId", `${idUsuario}`);
  },
  getUserId: () => {
    return localStorage.getItem("userId");
  },
  deleteUserId: () => {
    localStorage.removeItem("userId");
  },
  // Método para limpiar toda la sesión
  clearSession: () => {
    localStorage.removeItem("token");
    localStorage.removeItem("userId");
  },
};
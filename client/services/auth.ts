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
  setUserId: (userId: string) => {
    localStorage.setItem("userId", userId);
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
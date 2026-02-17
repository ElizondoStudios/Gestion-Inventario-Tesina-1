import { type RouteConfig, index, route } from "@react-router/dev/routes";

export default [
  index("routes/home.tsx"),
  route("login", "routes/login.tsx"),
  route("dashboard", "routes/dashboard.tsx", [
    route("inicio", "routes/inicio.tsx"),
    route("usuarios", "routes/usuarios.tsx"),
    route("perfiles-puesto", "routes/perfilesPuesto.tsx"),
    route("inventario", "routes/inventario.tsx"),
    route("sucursales", "routes/sucursales.tsx"),
  ]),
] satisfies RouteConfig;

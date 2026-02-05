import { type RouteConfig, index, route } from "@react-router/dev/routes";
export default [
  index("routes/inicio.tsx"),
  route("login", "routes/login.tsx")
] satisfies RouteConfig;

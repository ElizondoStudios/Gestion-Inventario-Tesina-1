import { z } from "zod";

export const crearUsuarioSucursalSchema = z.object({
  IDUsuario: z.number("El usuario es requerido"),
  IDSucursal: z.number("La sucursal es requerida"),
});

export type CrearUsuarioSucursalFormData = z.infer<typeof crearUsuarioSucursalSchema>;

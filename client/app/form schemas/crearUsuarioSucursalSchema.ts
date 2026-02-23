import { z } from "zod";

export const crearUsuarioSucursalSchema = z.object({
  IDUsuario: z.number({
    required_error: "El usuario es requerido",
  }),
  IDSucursal: z.number({
    required_error: "La sucursal es requerida",
  }),
});

export type CrearUsuarioSucursalFormData = z.infer<typeof crearUsuarioSucursalSchema>;

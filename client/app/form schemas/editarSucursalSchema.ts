import { z } from "zod";

export const editarSucursalSchema = z.object({
  IDSucursal: z.number(),
  Nombre: z.string().min(1, "El nombre es requerido"),
  Direccion: z.string().min(1, "La dirección es requerida"),
});

export type EditarSucursalFormData = z.infer<typeof editarSucursalSchema>;

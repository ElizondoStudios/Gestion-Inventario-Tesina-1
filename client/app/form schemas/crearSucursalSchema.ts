import { z } from "zod";

export const crearSucursalSchema = z.object({
  Nombre: z.string().min(1, "El nombre es requerido"),
  Direccion: z.string().min(1, "La dirección es requerida"),
});

export type CrearSucursalFormData = z.infer<typeof crearSucursalSchema>;

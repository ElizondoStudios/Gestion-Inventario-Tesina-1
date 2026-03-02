import { z } from "zod";

export const crearPerfilPuestoSchema = z.object({
  Descripcion: z.string()
});

export type CrearPerfilPuestoFormData = z.infer<typeof crearPerfilPuestoSchema>;

import { z } from "zod";

export const editarPerfilPuestoSchema = z.object({
  IDPerfilPuesto: z.number(),
  Descripcion: z.string()
});

export type EditarPerfilPuestoFormData = z.infer<typeof editarPerfilPuestoSchema>;

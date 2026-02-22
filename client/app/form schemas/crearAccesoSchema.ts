import { z } from "zod";

export const crearAccesoSchema = z.object({
  IDPerfilPuesto: z.number(),
  IDModulo: z.number().min(1, "El módulo es requerido"),
  IDNivelAcceso: z.number().min(1, "El nivel"),
});

export type CrearAccesoFormData = z.infer<typeof crearAccesoSchema>;

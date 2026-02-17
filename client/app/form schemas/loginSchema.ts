import { z } from "zod";

export const loginSchema = z.object({
  Correo: z
    .email("Formato de email inválido")
    .min(1, "El email es requerido"),

  Contrasenia: z
    .string()
});

export type LoginFormData = z.infer<typeof loginSchema>;

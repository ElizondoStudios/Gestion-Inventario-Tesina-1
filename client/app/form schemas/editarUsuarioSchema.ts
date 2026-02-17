import { z } from "zod";

export const editarUsuarioSchema = z.object({
  IDUsuario: z.number(),
  Nombre: z.string("Ingrese un nombre"),
  Correo: z.email("Formato de email inválido").min(1, "El email es requerido"),
  IDPerfilPuesto: z.string("Selecciona un opción"),
});

export type EditarUsuarioFormData = z.infer<typeof editarUsuarioSchema>;

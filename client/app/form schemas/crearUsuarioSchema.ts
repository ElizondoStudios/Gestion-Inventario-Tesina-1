import { z } from "zod";

export const crearUsuarioSchema = z.object({
  Nombre: z.string("Ingrese un nombre"),
  Correo: z.email("Formato de email inválido").min(1, "El email es requerido"),
  Contrasenia: z.string().regex(/(?=.*[A-Z])(?=.*[0-9])(?=.*[a-z])/, "La debe contener al menos una letra mayúscula, una letra minúscula y un número").min(8, "La contraseña debe ser de al menos 8 caracteres"),
  IDPerfilPuesto: z.string("Selecciona un opción"),
});

export type CrearUsuarioFormData = z.infer<typeof crearUsuarioSchema>;

import { z } from "zod";

export const generarMovimientoSchema = z.object({
  Cantidad: z.number("Ingrese una cantidad"),
  IDUsuario: z.number(),
  NoParte: z.string("Seleccione un producto"),
  IDSucursal: z.number("Seleccione una sucursal"),
  IDTipoMovimiento: z.number("Seleccione un tipo de movimiento"),
});

export type GenerarMovimientoFormData = z.infer<typeof generarMovimientoSchema>;

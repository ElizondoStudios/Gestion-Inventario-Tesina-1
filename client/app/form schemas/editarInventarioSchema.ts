import { z } from "zod";

export const editarInventarioSchema = z.object({
  NoParte: z.string().min(1, "El número de parte es requerido"),
  NombreProducto: z.string().min(1, "El nombre del producto es requerido"),
  DescripcionProducto: z.string().min(1, "La descripción es requerida"),
  Precio: z.number().min(0, "El precio debe ser mayor o igual a 0"),
  Costo: z.number().min(0, "El costo debe ser mayor o igual a 0"),
  IDUnidad: z.number().min(1, "La unidad es requerida"),
});

export type EditarInventarioFormData = z.infer<typeof editarInventarioSchema>;

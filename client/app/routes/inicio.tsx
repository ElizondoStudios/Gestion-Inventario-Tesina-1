import React from 'react'
import type { Route } from '../+types/root';

export function meta({}: Route.MetaArgs) {
  return [
    { title: "Gestión Inventario" },
    { name: "description", content: "Gestión Inventario" },
  ];
}

export default function inicio() {
  return (
    <div>inicio</div>
  )
}

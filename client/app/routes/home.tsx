import React from 'react'
import DashboardLayout from '~/layouts/DashboardLayout';
import { Outlet } from 'react-router';

export function meta() {
  return [
    { title: "Gestión Inventario" },
    { name: "description", content: "Gestión Inventario" },
  ];
}

export default function Home() {
  return (
    <Outlet></Outlet>
  )
}

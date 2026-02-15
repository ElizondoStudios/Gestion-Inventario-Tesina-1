import { useEffect } from 'react';
import { Outlet } from 'react-router';
import { redirect } from 'react-router';

export function meta() {
  return [
    { title: "Gestión Inventario" },
    { name: "description", content: "Gestión Inventario" },
  ];
}

export default function Home() {
  useEffect(() => {
    location.href="/login"
  }, [])

  return (
    <Outlet></Outlet>
  )
}

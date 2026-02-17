import { useEffect, useState } from 'react'
import { useSelector } from 'react-redux';
import { NavLink } from "react-router";
import { auth } from 'services/auth';

export default function Header() {
  const [nombreUsuario, setNombreUsuario] = useState("");
  const nombreRuta = useSelector((state: any) => state.currentPage.value)


  useEffect(() => {
    let nombre= auth.getNombre()
    nombre= nombre!==null? nombre.split(" ")[0]: "Usuario"
    setNombreUsuario(nombre)
  }, [])

  return (
    <div className='w-full flex justify-between'>
      <div>
        <span className="text-gray-400">Páginas</span>
        <span className='m-2'>/</span>
        <span>{nombreRuta}</span>
      </div>
      <div className="flex gap-4">
        <div className='flex gap-2 font-bold text-sm'>
          <i className="material-symbols-outlined">account_circle</i>
          <span>{nombreUsuario}</span>
        </div>
        <NavLink to="/login">
          <i className="material-symbols-outlined">logout</i>
        </NavLink>
      </div>
    </div>
  )
}

import React, { useEffect } from 'react'
import Sidebar from '~/components/Sidebar'
import Header from '~/components/Header'
import { auth } from 'services/auth'
import { useNavigate } from 'react-router'

export default function DashboardLayout({children}: {children: React.ReactNode}) {
  const navigate = useNavigate();
  
  useEffect(() => {
    // Validar que el usuario tenga sesión iniciada
    const authToken= auth.getToken();
    if(authToken===null){
      navigate("/login")
    }
  }, [])
  
  return (
    <>
      <main className='min-h-dvh w-full drawer lg:drawer-open'>
        <input id="my-drawer-1" type="checkbox" className="drawer-toggle" />
        <div className='drawer-content p-4 w-full'>
          <Header></Header>
          {children}
        </div>
        <div className="drawer-side">
          <label htmlFor="my-drawer-1" aria-label="close sidebar" className="drawer-overlay"></label>
          <Sidebar></Sidebar>
        </div>
      </main>
      <footer></footer>
    </>
  )
}

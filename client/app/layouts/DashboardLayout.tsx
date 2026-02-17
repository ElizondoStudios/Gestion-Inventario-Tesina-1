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
      <main className='min-h-dvh w-full flex'>
        <div className="sticky w-1/5">
          <Sidebar></Sidebar>
        </div>
        <div className='w-4/5 p-4'>
          <Header></Header>
          {children}
        </div>
      </main>
      <footer></footer>
    </>
  )
}

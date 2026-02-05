import React from 'react'
import Sidebar from '~/components/Sidebar'

export default function DashboardLayout({children}: {children: React.ReactNode}) {
  return (
    <>
      <main className='container h-lvh'>
        <Sidebar></Sidebar>
        {children}
      </main>
      <footer></footer>
    </>
  )
}

import React from 'react'
import Sidebar from '~/components/Sidebar'
import Header from '~/components/Header'

export default function DashboardLayout({children}: {children: React.ReactNode}) {
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

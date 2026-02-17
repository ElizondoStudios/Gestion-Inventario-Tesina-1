import React from 'react'
import { Outlet } from 'react-router'
import DashboardLayout from '~/layouts/DashboardLayout'

export default function dashboard() {
  return (
    <DashboardLayout>
      <Outlet></Outlet>
    </DashboardLayout>
  )
}

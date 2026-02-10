import { useSelector } from "react-redux"
import "./Loading.css"
import { Riple } from "react-loading-indicators"
import { useState } from "react"

export default function Loading() {
  const isLoading = useSelector((state: any) => state.loading.value)
  useState()
  return (
    isLoading && (
      <div className="load-indicator">
        <Riple color="var(--color-primary)" size="large"></Riple>
      </div>
    )
  )
}

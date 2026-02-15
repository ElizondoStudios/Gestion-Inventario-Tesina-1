export function formatCurrency(val: number | undefined){
  return val!==undefined? Intl.NumberFormat("es-MX", {style: "currency", currency: "MXN"}).format(val): ""
}
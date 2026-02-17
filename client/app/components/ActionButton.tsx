interface props{
  icon: string;
  text: string;
  action: (data: any) => void
  disabled?: boolean
}

export default function ActionButton({icon, text, action, disabled = false}: props) {
  return (
    <button className="btn btn-sm bg-transparent border-none text-gray-400 disabled:text-gray-300" type="button" onClick={action} disabled={disabled}>
      <i className="material-symbols-outlined">{icon}</i>
      <span className="text-xs">{text}</span>
    </button>
  )
}

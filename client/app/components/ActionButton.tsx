interface props{
  icon: string;
  text: string;
  action: (data: any) => void
}

export default function ActionButton({icon, text, action}: props) {
  return (
    <button className="btn btn-sm bg-transparent border-none" type="button" onClick={action}>
      <i className="material-symbols-outlined text-gray-400">{icon}</i>
      <span className="text-gray-400 text-xs">{text}</span>
    </button>
  )
}

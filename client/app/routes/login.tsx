import logo from "../assets/logo-light.png"
import { useForm } from "react-hook-form";
import { zodResolver } from "@hookform/resolvers/zod";
import { loginSchema, type LoginFormData } from "../form schemas/loginSchema"
import { api } from 'services/api';
import { toast } from "react-toastify";
import { auth } from 'services/auth';
import { useNavigate } from 'react-router';
import { useEffect } from "react";
import banner from "../assets/login-banner.png"

export default function login() {
  const navigate = useNavigate();

  const {
    register,
    handleSubmit,
    formState: { errors },
  } = useForm<LoginFormData>({
    resolver: zodResolver(loginSchema),
  });

  const onSubmit = (Logindata: LoginFormData) => {
    api.Login(Logindata)
      .then( data => {
        auth.setToken(data.Token)
        auth.setIDUsuario(data.IDUsuario)
        navigate("/dashboard")
      })
      .catch((error) => {
        toast.error("Usuario o contraseña incorrectos");
      })
  };

  useEffect(() => {
    auth.clearSession();
  }, [])

  return (
    <div className="h-dvh w-dvw flex items-center justify-center">
      <img src={banner} className="absolute top-0 h-1/2 min-w-full" />
      <div className="card bg-base-100 shadow w-1/3 min-w-110">
        <div className="card-body flex-col gap-5 items-center justify-center">
          <img src={logo} alt="Logo" className='w-12 h-12' />
          <span className='font-bold text-lg'>Iniciar Sesión</span>
          <form className="w-full flex flex-col gap-4" onSubmit={handleSubmit(onSubmit)}>
            <div className="w-full">
              <label>Correo</label>
              <input {...register("Correo")} type="email" className=" w-full input" placeholder="tucorreo@correo.com" />
              {errors.Correo && (
                <p className='text-sm text-error'>{errors.Correo.message}</p>
              )}
            </div>
            <div className="w-full">
              <label>Contraseña</label>
              <input {...register("Contrasenia")} type="password" className=" w-full input" placeholder="******" />
              {errors.Contrasenia && (
                <p className='text-sm text-error'>{errors.Contrasenia.message}</p>
              )}
            </div>
            <button type="submit" className='btn btn-primary w-full'>Iniciar Sesión</button>
          </form>
        </div>
      </div>
    </div>
  )
}

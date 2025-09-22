import React, { useState } from "react";
import { login } from "../services/auth";

export default function Login() {
  const [email, setEmail] = useState("");
  const [pass, setPass] = useState("");

  const submit = async () => {
    try {
      const res = await login(email, pass);
      // supongamos que el backend devuelve { Token: "...", Respuesta: 'OK' }
      if (res?.Token) {
        localStorage.setItem("token", res.Token);
        window.location.href = "/";
      } else {
        alert("Error en login: " + JSON.stringify(res));
      }
    } catch (err) {
      console.error("login error", err);
      alert("Error en login");
    }
  };

  return (
    <div className="h-screen flex items-center justify-center">
      <div className="w-96 p-6 card">
        <h2 className="text-xl font-bold mb-4">Iniciar sesión</h2>
        <input className="w-full p-2 mb-2 rounded" placeholder="Correo" value={email} onChange={(e) => setEmail(e.target.value)} />
        <input type="password" className="w-full p-2 mb-4 rounded" placeholder="Contraseña" value={pass} onChange={(e) => setPass(e.target.value)} />
        <button onClick={submit} className="btn-hibox w-full">Entrar</button>
      </div>
    </div>
  );
}

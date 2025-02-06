import React, { useState } from "react";
import { authAPI } from "../services/api";

const Login = ({ onLogin }) => {
  const [username, setUsername] = useState("");
  const [password, setPassword] = useState("");

  const handleSubmit = async (e) => {
    e.preventDefault();
    try {
      const token = await authAPI.login({ username, password }); // Envía las credenciales
      localStorage.setItem("token", token); // Guarda el token en localStorage
      onLogin(); // Llama a la función onLogin para redirigir al usuario
    } catch (error) {
      console.error("Error al iniciar sesión", error);
    }
  };

  return (
    <form onSubmit={handleSubmit}>
      <input
        type="text"
        placeholder="Usuario"
        value={username}
        onChange={(e) => setUsername(e.target.value)}
      />
      <input
        type="password"
        placeholder="Contraseña"
        value={password}
        onChange={(e) => setPassword(e.target.value)}
      />
      <button type="submit">Iniciar Sesión</button>
    </form>
  );
};

export default Login;
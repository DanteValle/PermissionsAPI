import React, { useState } from "react";
import { authAPI } from "../services/api";

const Login = ({ onLogin }) => {
  const [username, setUsername] = useState("");
  const [password, setPassword] = useState("");

  const handleSubmit = async (e) => {
    e.preventDefault();
    try {
      const token = await authAPI.login({ username, password });
      localStorage.setItem("token", token);
      onLogin();
    } catch (error) {
      console.error("Error al iniciar sesión", error);
    }
  };

  return (
    <div
      className="login-container"
      style={{
        backgroundImage: "url('https://785-cms-cdn.azureedge.net/n5cmsblob/2025/01/portada-blog-11-696x440.jpg')",
        backgroundSize: "cover",
        backgroundPosition: "center",
        height: "100vh",
        display: "flex",
        justifyContent: "center",
        alignItems: "center",
      }}
    >
      <div className="card shadow" style={{ padding: "20px", maxWidth: "400px", background: "rgba(255, 255, 255, 0.9)", borderRadius: "10px" }}>
        <h3 className="text-center">Iniciar Sesión</h3>
        <form onSubmit={handleSubmit}>
          <div className="mb-3">
            <label htmlFor="username" className="form-label">Usuario</label>
            <input
              type="text"
              id="username"
              className="form-control"
              placeholder="Usuario"
              value={username}
              onChange={(e) => setUsername(e.target.value)}
            />
          </div>
          <div className="mb-3">
            <label htmlFor="password" className="form-label">Contraseña</label>
            <input
              type="password"
              id="password"
              className="form-control"
              placeholder="Contraseña"
              value={password}
              onChange={(e) => setPassword(e.target.value)}
            />
          </div>
          <div className="d-grid">
            <button type="submit" className="btn btn-primary">Iniciar Sesión</button>
          </div>
        </form>
      </div>
    </div>
  );
};

export default Login;
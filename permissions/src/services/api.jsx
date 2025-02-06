// src/services/api.js
import axios from "axios";

const API_BASE_URL = "https://localhost:7005/api"; // Reemplaza con la URL base de tu API

const api = axios.create({
  baseURL: API_BASE_URL,
});

// Interceptor para agregar el token a las solicitudes
api.interceptors.request.use((config) => {
  const token = localStorage.getItem("token");
  if (token) {
    config.headers.Authorization = `Bearer ${token}`;
  }
  return config;
});

export const authAPI = {
  login: async (credentials) => {
    const response = await api.post("/Auth/login", {
      username: credentials.username,
      password: credentials.password,
    });
    return response.data.token; // Extrae el token de la respuesta
  },
};

export const permissionAPI = {
  getPermissions: async () => {
    const response = await api.get("/Permissions");
    return response.data; // Devuelve la lista de permisos
  },
  requestPermission: (data) => api.post("/Permissions/request", data),
  modifyPermission: (data) => api.put("/Permissions/modify", data),
};
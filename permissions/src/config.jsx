const API_BASE_URL = "https://localhost:7005/api"; // Reemplaza con la URL base de tu API

export const API_URLS = {
  LOGIN: `${API_BASE_URL}/Auth/login`, // Endpoint para el login
  REQUEST_PERMISSION: `${API_BASE_URL}/Permissions/request`, // Endpoint para solicitar permisos
  MODIFY_PERMISSION: `${API_BASE_URL}/Permissions/modify`, // Endpoint para modificar permisos
  GET_PERMISSIONS: `${API_BASE_URL}/Permissions`, // Endpoint para obtener permisos
};
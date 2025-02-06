import React, { useState, useEffect } from "react";
import { permissionAPI } from "../services/api";

const Permiso = () => {
  const [permisos, setPermisos] = useState([]);
  const [formData, setFormData] = useState({
    nameEmployee: "",
    lastNameEmployee: "",
    permissionTypeId: "",
    permissionDate: "",
  });

  useEffect(() => {
    fetchPermisos();
  }, []);

  const fetchPermisos = async () => {
    try {
      const response = await permissionAPI.getPermissions();
      setPermisos(response.data); // Guarda la lista de permisos en el estado
    } catch (error) {
      console.error("Error al obtener permisos", error);
    }
  };

  const handleRequestPermission = async (e) => {
    e.preventDefault();
    try {
      await permissionAPI.requestPermission(formData);
      fetchPermisos(); // Actualiza la lista de permisos
      setFormData({
        nameEmployee: "",
        lastNameEmployee: "",
        permissionTypeId: "",
        permissionDate: "",
      }); // Limpia el formulario
    } catch (error) {
      console.error("Error al solicitar permiso", error);
    }
  };

  return (
    <div>
      <h1>Permisos</h1>
      <form onSubmit={handleRequestPermission}>
        <input
          type="text"
          placeholder="Nombre del empleado"
          value={formData.nameEmployee}
          onChange={(e) =>
            setFormData({ ...formData, nameEmployee: e.target.value })
          }
        />
        <input
          type="text"
          placeholder="Apellido del empleado"
          value={formData.lastNameEmployee}
          onChange={(e) =>
            setFormData({ ...formData, lastNameEmployee: e.target.value })
          }
        />
        <input
          type="number"
          placeholder="Tipo de permiso (ID)"
          value={formData.permissionTypeId}
          onChange={(e) =>
            setFormData({ ...formData, permissionTypeId: e.target.value })
          }
        />
        <input
          type="date"
          placeholder="Fecha de permiso"
          value={formData.permissionDate}
          onChange={(e) =>
            setFormData({ ...formData, permissionDate: e.target.value })
          }
        />
        <button type="submit">Solicitar Permiso</button>
      </form>
      <table>
        <thead>
          <tr>
            <th>ID</th>
            <th>Nombre</th>
            <th>Apellido</th>
            <th>Tipo de Permiso</th>
            <th>Fecha de Permiso</th>
          </tr>
        </thead>
        <tbody>
          {permisos.map((permiso) => (
            <tr key={permiso.id}>
              <td>{permiso.id}</td>
              <td>{permiso.nameEmployee}</td>
              <td>{permiso.lastNameEmployee}</td>
              <td>
                {permiso.permissionType
                  ? permiso.permissionType.description
                  : "N/A"}
              </td>
              <td>{new Date(permiso.permissionDate).toLocaleDateString()}</td>
            </tr>
          ))}
        </tbody>
      </table>
    </div>
  );
};

export default Permiso;
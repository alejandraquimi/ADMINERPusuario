import axioInstance from "./axiosConfig.js";
import { decodeToken } from "./loginService.js";

export async function postUsuario(params) {
  try {
    const response = await axioInstance.post("/Usuario", params);

    return response.data;
  } catch (error) {
    console.error("Error en al guardar Usuario:", error);

    if (error.response) {
      if (error.response.status === 400) {
        throw new Error("Error: 400, Existe un usuario con el mismo correo!");
      } else {
        throw new Error(
          `Error en al guardar Usuario: ${error.response.status} ${error}`
        );
      }
    }
  }
}
export async function deleteUsuario(param) {
  try {
    const response = await axioInstance.delete(`/Usuario/${param}`);

    return response.data;
  } catch (error) {
    console.error("Error en al Inactivar Usuario:", error);

    if (error.response) {
      if (error.response.status === 400) {
        throw new Error("Error: 400");
      } else {
        throw new Error(
          `Error en al Inactivar Usuario: ${error.response.status} ${error}`
        );
      }
    }
  }
}

export async function getUsuarioByquery(params) {
  let usuarios = [];
  try {
    const response = await axioInstance.post(`/ReporteUsuario`, params);
    usuarios = response.data;
    return usuarios;
  } catch (error) {
    console.error("Error en obtener informacion:", error);

    if (error.response) {
      if (error.response.status === 400) {
        throw new Error("Error: 400");
      } else if (error.response.status === 404) {
        throw new Error("No se encontro usuarios");
      } else {
        throw new Error(`Error al buscar Usuario: ${error.response.status} `);
      }
    }
  }
}
export async function putUsuarioByquery(params) {
  try {
    const response = await axioInstance.put(`/Usuario`, params);
    return response.data;
  } catch (error) {
    console.error("Error en obtener informacion:", error);

    if (error.response) {
      if (error.response.status === 400) {
        throw new Error("Error: 400");
      } else {
        throw new Error(
          `Error en al actualizar Usuario: ${error.response.status} ${error}`
        );
      }
    }
  }
}

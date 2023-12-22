import axioInstance from "./axiosConfig.js";

export async function postContacto(params) {
  try {
    const response = await axioInstance.post("/Contacto", params);

    return response.data;
  } catch (error) {
    console.error("Error en al Agregar Contacto:", error);

    if (error.response) {
      if (error.response.status === 400) {
        throw new Error("Error: 400 Ya existe este usuario en sus contactos");
      } else {
        throw new Error(
          `Error en al Agregar Contacto: ${error.response.status} ${error}`
        );
      }
    }
  }
}
export async function getContactosByUsuario(param) {
  let usuarios = [];
  try {
    const response = await axioInstance.get(`/Contacto/${param}`);
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
        throw new Error(
          `Error al traer los Contactos Usuario: ${error.response.status} `
        );
      }
    }
  }
}

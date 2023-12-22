import axiosInstance from "./axiosConfig.js";
import { jwtDecode } from "jwt-decode";
import { setUserInfo } from "../redux/authSlice.js";
import { getUsuarioByquery, postUsuario } from "./usuarioService.js";

export async function postlogin(params) {
  try {
    const response = await axiosInstance.post("/Login/Athetication", params);
    const token = response.data.token;

    axiosInstance.updateToken(token);

    return response.data;
  } catch (error) {
    console.error("Error en la autenticación:", error);

    if (error.response) {
      if (error.response.status === 400) {
        console.log("SI ENTRE EN 404");
        throw new Error("Error: Correo o contraseña no encontrado");
      } else {
        throw new Error(`Error del servidor: ${error.response.status}`);
      }
    }
  }
}

export function decodeToken() {
  const savedToken = localStorage.getItem("accessToken");

  if (savedToken) {
    try {
      const decoded = jwtDecode(savedToken);

      return parseInt(decoded.IDUsuario);
    } catch (error) {
      console.error("Error al decodificar el token:", error.message);
      return null;
    }
  } else {
    console.error("No se encontró ningún token en el localStorage");
    return null;
  }
}

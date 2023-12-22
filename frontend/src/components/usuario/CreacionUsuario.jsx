import React, { useState, useEffect } from "react";
import "./CrearUsuario.css";
import CustomSnackbar from "../custom/SnackbarCustom";
import {
  postUsuario,
  getUsuarioByquery,
  putUsuarioByquery,
} from "../../services/usuarioService";
import { useAppSelector, useAppDispatch } from "../../redux/hooks";
import { setUserInfo } from "../../redux/authSlice";

const CrearUsuario = ({ editar }) => {
  const valoresIniciales = {
    nombre: "",
    correoElectronico: "",
    contrasenaHash: "",
    telefono: "",
  };

  const [usuario, setUsuario] = useState(valoresIniciales);
  const [snackbarMessages, setSnackbarMessages] = useState([]);
  const [idUsuario, setIdUsuario] = useState(null);
  const infor = useAppSelector((state) => state.auth.userInfo);
  const contactos = useAppSelector((state) => state.auth.contactos);

  const dispatch = useAppDispatch();

  useEffect(() => {
    (async () => {
      if (editar) {
        if (infor.usuario != null && infor.idUsuario != 0) {
          setIdUsuario(infor.idUsuario);

          const usuarioCompleto = infor.usuario;
          setUsuario({
            nombre: usuarioCompleto.nombre,
            correoElectronico: usuarioCompleto.correoElectronico,
            telefono: usuarioCompleto.telefono,
          });
        }
      } else {
        setUsuario(valoresIniciales);
      }
    })();
  }, [editar]);

  const handleGuardarClick = async () => {
    if (
      !usuario.nombre ||
      !usuario.correoElectronico ||
      (editar ? false : !usuario.contrasenaHash) ||
      !usuario.telefono
    ) {
      const errorMessage = "Faltan campos por completar!";

      const newMessage = {
        message: errorMessage,
        severity: "error",
      };

      setSnackbarMessages((prevMessages) => [...prevMessages, newMessage]);
      return;
    }

    try {
      let successMessage = "";

      if (editar) {
        if (usuario.contrasenaHash !== "") {
          setUsuario((prevUsuario) => ({
            ...prevUsuario,
            contrasenaHash: usuario.contrasenaHash,
          }));
        } else {
          setUsuario((prevUsuario) => ({
            ...prevUsuario,
            contrasenaHash: null,
          }));
        }
        const response = await putUsuarioByquery({
          idUsuario,
          nombre: usuario.nombre,
          contrasenaHash: usuario.contrasenaHash,
          correoElectronico: usuario.correoElectronico,
          telefono: usuario.telefono,
        });
        dispatch(
          setUserInfo({ idUsuario: infor.idUsuario, usuario: response })
        );

        successMessage = "Usuario Actualizado!";
      } else {
        const response = await postUsuario(usuario);

        successMessage = "Usuario Creado!";

        setUsuario(valoresIniciales);
      }

      const newMessage = {
        message: successMessage,
        severity: "success",
      };

      setSnackbarMessages((prevMessages) => [...prevMessages, newMessage]);
    } catch (error) {
      let errorMessage = error.message;

      const newMessage = {
        message: errorMessage,
        severity: "error",
      };

      setSnackbarMessages((prevMessages) => [...prevMessages, newMessage]);

      console.error("Error durante guardar usuario:", error);
    }
  };
  const handleInputChange = (nombre, value) => {
    setUsuario((prevUsuario) => ({
      ...prevUsuario,
      [nombre]: value || "",
    }));
  };

  return (
    <div className="crear-usuario-container">
      <CustomSnackbar
        snackbarMessage={snackbarMessages[0]}
        onClose={() => {
          setSnackbarMessages((prev) => prev.slice(1));
        }}
      />
      <h2>{editar ? "Editar" : "Crear"} Usuario</h2>
      <form className="form-container">
        <div className="form-group">
          <label htmlFor="nombre">Nombre:</label>
          <input
            type="text"
            id="nombre"
            value={usuario.nombre || ""}
            onChange={(e) => {
              const newValue = e.target.value;
              handleInputChange("nombre", newValue);
            }}
            required
          />
        </div>
        <div className="form-group">
          <label htmlFor="email">Email:</label>
          <input
            type="email"
            id="email"
            value={usuario.correoElectronico || ""}
            onChange={(e) => {
              const newValue = e.target.value;
              handleInputChange("correoElectronico", newValue);
            }}
            required
          />
        </div>
        <div className="form-group">
          <label htmlFor="contrasena">Contraseña:</label>
          <input
            type="password"
            id="contrasena"
            value={usuario.contrasenaHash || ""}
            onChange={(e) => {
              const newValue = e.target.value;
              handleInputChange("contrasenaHash", newValue);
            }}
            required
          />
        </div>
        <div className="form-group">
          <label htmlFor="telefono">Teléfono:</label>
          <input
            type="tel"
            id="telefono"
            value={usuario.telefono || ""}
            onChange={(e) => {
              const newValue = e.target.value;
              handleInputChange("telefono", newValue);
            }}
            required
          />
        </div>
        <button
          type="button"
          onClick={handleGuardarClick}
          className="guardar-button"
        >
          Guardar
        </button>
      </form>
    </div>
  );
};

export default CrearUsuario;

import React, { useState, useEffect } from "react";
import "./ReporteUsuario.css";
import { getContactosByUsuario } from "../../services/contactoService";

import CustomSnackbar from "../custom/SnackbarCustom";
import { decodeToken } from "../../services/loginService";

const ReporteContacto = () => {
  const [datos, setDatos] = useState([]);
  const [snackbarMessages, setSnackbarMessages] = useState([]);
  const [idUsuario, setIdUsuario] = useState(null);

  useEffect(() => {
    handleBuscar();
  }, []);

  const handleBuscar = async () => {
    try {
      const idUsuario = decodeToken();

      if (idUsuario != null) {
        const responseUsuarioByID = await getContactosByUsuario(idUsuario);
        if (responseUsuarioByID.length > 0) {
          setDatos(responseUsuarioByID);
        }

        setIdUsuario(idUsuario);
      }
    } catch (e) {
      let errorMessage = "No se encontro";

      const newMessage = {
        message: errorMessage,
        severity: "info",
      };

      setSnackbarMessages((prevMessages) => [...prevMessages, newMessage]);
      setDatos([]);
    }
  };

  return (
    <div className="reporte-container">
      <CustomSnackbar
        snackbarMessage={snackbarMessages[0]}
        onClose={() => {
          setSnackbarMessages((prev) => prev.slice(1));
        }}
      />
      <h1>Tus Contactos</h1>

      <div>
        {datos.map((item, index) => (
          <div key={index}>
            <label>#: {index + 1}</label>
            <br />
            <label>Nombre: {item.usuarioC.nombre}</label>
            <br />
            <label>
              Correo: {item.usuarioC ? item.usuarioC.correoElectronico : "N/A"}
            </label>
            <br />
            <label>Teléfono: {item.usuarioC.telefono}</label>
            <br />
            <hr />
          </div>
        ))}
      </div>
    </div>
  );
};

export default ReporteContacto;

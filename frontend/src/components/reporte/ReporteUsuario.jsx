import React, { useState, useEffect } from "react";
import "./ReporteUsuario.css";
import { Search, Delete } from "@mui/icons-material";
import PersonAddIcon from "@mui/icons-material/PersonAdd";
import {
  deleteUsuario,
  getUsuarioByquery,
} from "../../services/usuarioService";
import { postContacto } from "../../services/contactoService";
import { useAppSelector, useAppDispatch } from "../../redux/hooks";
import { setUserInfo } from "../../redux/authSlice";
import { setContactos } from "../../redux/authSlice";

import CustomSnackbar from "../custom/SnackbarCustom";

const ReporteUsuario = () => {
  const [datos, setDatos] = useState([]);
  const [query, setQuery] = useState("");
  const [snackbarMessages, setSnackbarMessages] = useState([]);
  const [idUsuario, setIdUsuario] = useState(null);
  const [usuarioByID, setUsuarioByID] = useState(null);
  const [contactosXUsuario, setContactosXUsuario] = useState([]);
  const infor = useAppSelector((state) => state.auth.userInfo);
  const dispatch = useAppDispatch();
  const contactos = useAppSelector((state) => state.auth.contactos);

  useEffect(() => {
    handleBuscar();
  }, []);

  const handleBuscar = async () => {
    try {
      const idUsuario = infor.idUsuario;

      if (idUsuario != 0 && infor.usuario != null) {
        const usuarioApp = infor.usuario;
        setUsuarioByID(usuarioApp);
        setContactosXUsuario(contactos);

        setIdUsuario(idUsuario);

        const response = await getUsuarioByquery({ idUsuario, query });

        if (response.length > 0) {
          setDatos(response);
        }
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
  const handleAgregarContacto = async (item) => {
    try {
      const response = await postContacto({
        idUsuarioP: idUsuario,
        idUsuarioC: item.idUsuario,
      });
      setContactosXUsuario((prevArray) => [...prevArray, response]);

      dispatch(setContactos([...contactosXUsuario, response]));
      let successMessage = "Contacto Agregado!!!";

      const newMessage = {
        message: successMessage,
        severity: "success",
      };

      setSnackbarMessages((prevMessages) => [...prevMessages, newMessage]);
    } catch (error) {
      let errorMessage = `Error al guardar el contacto: ${error}`;

      const newMessage = {
        message: errorMessage,
        severity: "success",
      };

      setSnackbarMessages((prevMessages) => [...prevMessages, newMessage]);
    }
  };
  const handleDesactivarUsuario = async (item, index) => {
    try {
      await deleteUsuario(item.idUsuario);
      const copiaData = [...datos];

      copiaData.splice(index, 1);
      setDatos(copiaData);

      let successMessage = "Usuario Inactivado!!!";

      const newMessage = {
        message: successMessage,
        severity: "success",
      };

      setSnackbarMessages((prevMessages) => [...prevMessages, newMessage]);
    } catch (error) {
      let errorMessage = `Error al inactivar el usuario: ${error}`;

      const newMessage = {
        message: errorMessage,
        severity: "success",
      };

      setSnackbarMessages((prevMessages) => [...prevMessages, newMessage]);
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
      <h1>Reporte</h1>
      <div className="barra-busqueda">
        <input
          type="text"
          value={query}
          onChange={(e) => setQuery(e.target.value)}
          placeholder="Buscar por nombre o correo"
        />
        <button onClick={handleBuscar}>
          <Search />
        </button>
      </div>
      <table>
        <thead>
          <tr>
            <th>#</th>
            <th>Nombre</th>
            <th>Correo</th>
            <th>Teléfono</th>
            <th>Acción</th>
          </tr>
        </thead>
        <tbody>
          {datos.map((item, index) => (
            <tr key={index}>
              <td>{index + 1}</td>
              <td>{item.nombre}</td>
              <td>{item.correoElectronico}</td>
              <td>{item.telefono}</td>
              <td>
                {!contactosXUsuario.some(
                  (contacto) => contacto.idUsuarioC === item.idUsuario
                ) && (
                  <button
                    className="accion-btn editar"
                    title="Editar"
                    onClick={() => handleAgregarContacto(item, index)}
                  >
                    <PersonAddIcon />
                  </button>
                )}
                <button
                  className="accion-btn eliminar"
                  title="Eliminar"
                  onClick={() => handleDesactivarUsuario(item, index)}
                >
                  <Delete />
                </button>
              </td>
            </tr>
          ))}
        </tbody>
      </table>
    </div>
  );
};

export default ReporteUsuario;

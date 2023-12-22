import React, { useEffect, useState } from "react";
import NavBar from "../../components/custom/NavBar";
import CrearUsuario from "../../components/usuario/CreacionUsuario";
import ReporteUsuario from "../../components/reporte/ReporteUsuario";
import ReporteContacto from "../../components/reporte/ReporteContacto";
import { getUsuarioByquery } from "../../services/usuarioService";
import "./Home.css";
import { useAppSelector, useAppDispatch } from "../../redux/hooks";
import { decodeToken } from "../../services/loginService";
import { setUserInfo } from "../../redux/authSlice";
import { setContactos } from "../../redux/authSlice";
import { useNavigate } from "react-router-dom";

const Home = () => {
  const [access, setAccess] = useState(false);
  const navigate = useNavigate();

  const dispatch = useAppDispatch();

  const opciones = [
    { title: "Crear Usuario", children: <CrearUsuario editar={false} /> },
    { title: "Editar Usuario", children: <CrearUsuario editar={true} /> },
    {
      title: "Consultar Usuario y Agregar Contacto",
      children: <ReporteUsuario />,
    },
    { title: "Mostrar tus Contactos", children: <ReporteContacto /> },
    {
      title: "Cerrar Sesión",
      children: "Contenido de la opción 3",
    },
  ];

  useEffect(() => {
    const actualizarAuthentication = async () => {
      try {
        const decoded = decodeToken();
        if (decoded != null) {
          setAccess(true);
          const response = await getUsuarioByquery({ idUsuario: decoded });
          if (response.length > 0) {
            const contactos = response[0].usuariosContactos;
            dispatch(setUserInfo({ idUsuario: decoded, usuario: response[0] }));
            dispatch(setContactos(contactos));
          }
        }
      } catch (error) {
        console.error("Error al actualizar informacion:", error.message);
      }
    };

    actualizarAuthentication();
  }, []);

  return (
    <div>
      <NavBar options={opciones} />
    </div>
  );
};

export default Home;

import React, { useState } from "react";
import { useNavigate } from "react-router-dom";
import "./Login.css";
import { postlogin } from "../../services/loginService";
import CustomSnackbar from "../../components/custom/SnackbarCustom";
const Login = () => {
  const [snackbarMessages, setSnackbarMessages] = useState([]);
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");
  const navigate = useNavigate();

  const handleLogin = async () => {
    try {
      const response = await postlogin({
        correoElectronico: email,
        contrasenaHash: password,
      });

      // Redirigir a la página de inicio si la autenticación es exitosa
      navigate("/home");
    } catch (error) {
      let errorMessage = error.message;

      const newMessage = {
        message: errorMessage,
        severity: "error",
      };

      setSnackbarMessages((prevMessages) => [...prevMessages, newMessage]);

      console.error("Error durante el inicio de sesión:", error);
      // Manejar errores de autenticación, mostrar un mensaje, etc.
    }
  };
  return (
    <div className="login-container">
      <CustomSnackbar
        snackbarMessage={snackbarMessages[0]}
        onClose={() => {
          setSnackbarMessages((prev) => prev.slice(1));
        }}
      />
      <h2>Login</h2>
      <form>
        <label>
          Correo:
          <input
            type="text"
            value={email}
            onChange={(e) => setEmail(e.target.value)}
          />
        </label>
        <br />
        <label>
          Contraseña:
          <input
            type="password"
            value={password}
            onChange={(e) => setPassword(e.target.value)}
          />
        </label>
        <br />
        <button className="button-login" type="button" onClick={handleLogin}>
          Iniciar Sesión
        </button>
      </form>
    </div>
  );
};

export default Login;

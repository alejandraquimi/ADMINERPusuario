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
      <form className="login-form">
        <div className="form-group">
          <label htmlFor="email">Correo:</label>
          <input
            type="text"
            id="email"
            value={email}
            onChange={(e) => setEmail(e.target.value)}
          />
        </div>
        <div className="form-group">
          <label htmlFor="password">Contraseña:</label>
          <input
            type="password"
            id="password"
            value={password}
            onChange={(e) => setPassword(e.target.value)}
          />
        </div>
        <button className="button-login" type="button" onClick={handleLogin}>
          Iniciar Sesión
        </button>
      </form>
    </div>
  );
};

export default Login;

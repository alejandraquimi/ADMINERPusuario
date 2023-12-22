// Routes.js
import React from "react";
import { BrowserRouter as Router, Route, Routes } from "react-router-dom";
import Home from "../views/home/Home";
import Login from "../views/login/Login";

const AppRoutes = () => {
  return (
    <Routes>
      <Route path="/home" element={<Home />} />
      <Route path="/login" element={<Login />} />
      {/* Otras rutas si es necesario */}
    </Routes>
  );
};

const RoutesContainer = () => {
  return (
    <Router>
      <AppRoutes />
    </Router>
  );
};

export default RoutesContainer;

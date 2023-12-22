// Routes.js
import React from "react";
import {
  BrowserRouter as Router,
  Route,
  Routes,
  Navigate,
} from "react-router-dom";
import Home from "../views/home/Home";
import Login from "../views/login/Login";

const AppRoutes = () => {
  return (
    <Routes>
      <Route path="/" element={<Navigate to="/login" />} />
      <Route path="/home" element={<Home />} />

      <Route path="/login" element={<Login />} />
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

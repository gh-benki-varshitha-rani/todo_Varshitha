// src/App.js
import React, { useState } from "react";
import { Routes, Route, Navigate } from "react-router-dom";
import Navbar from "./components/Navbar";
import LoginForm from "./components/LoginForm";
import RegisterForm from "./components/RegisterForm";
import TodoList from "./components/TodoList";
import "./index.css";

export default function App() {
  const [token, setToken] = useState(localStorage.getItem("token"));
  const [username, setUsername] = useState(localStorage.getItem("username") || "");
  const [role, setRole] = useState(localStorage.getItem("role") || "");

  function handleLogin() {
    setToken(localStorage.getItem("token"));
    setUsername(localStorage.getItem("username"));
    setRole(localStorage.getItem("role"));
  }

  function handleLogout() {
    localStorage.clear();
    setToken(null);
    setUsername("");
    setRole("");
  }

  return (
    <div className="container">
      <Navbar token={token} username={username} role={role} onLogout={handleLogout} />

      <Routes>
        <Route path="/" element={<Navigate to={token ? "/todos" : "/login"} />} />
        <Route path="/register" element={<RegisterForm />} />
        <Route path="/login" element={<LoginForm onLogin={handleLogin} />} />
        <Route path="/todos" element={token ? <TodoList /> : <Navigate to="/login" />} />
        <Route path="*" element={<h2>404 - Page not found</h2>} />
      </Routes>
    </div>
  );
}

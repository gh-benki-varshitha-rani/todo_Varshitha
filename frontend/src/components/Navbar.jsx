// src/components/Navbar.jsx
import React from "react";
import { Link, useNavigate } from "react-router-dom";

export default function Navbar({ token, username, role, onLogout }) {
  const navigate = useNavigate();

  function handleLogoutClick() {
    onLogout();
    navigate("/login");
  }

  return (
    <nav style={styles.nav}>
      <div style={styles.left}>
        <h3>📋 ToDo Manager</h3>
      </div>
      <div style={styles.right}>
        {token ? (
          <>
            <span style={styles.greeting}>
             hello <strong>{username}</strong> ({role})
            </span>
            <Link to="/todos" style={styles.link}>Todos</Link>
            <button onClick={handleLogoutClick} style={styles.button}>Logout</button>
          </>
        ) : (
          <>
            <Link to="/login" style={styles.link}>Login</Link>
            <Link to="/register" style={styles.link}>Register</Link>
          </>
        )}
      </div>
    </nav>
  );
}

const styles = {
  nav: {
    padding: "10px 20px",
    backgroundColor: "#282c34",
    color: "white",
    display: "flex",
    justifyContent: "space-between",
    alignItems: "center",
    marginBottom: "20px"
  },
  left: {
    fontSize: "20px"
  },
  right: {
    display: "flex",
    alignItems: "center",
    gap: "15px"
  },
  link: {
    color: "#61dafb",
    textDecoration: "none",
    fontWeight: "bold"
  },
  button: {
    backgroundColor: "#ff6b6b",
    border: "none",
    padding: "6px 12px",
    color: "white",
    borderRadius: "4px",
    cursor: "pointer"
  },
  greeting: {
    marginRight: "10px",
    fontStyle: "italic"
  }
};


import React, { useState } from "react";
import { useNavigate } from "react-router-dom";

export default function RegisterForm() {
  const navigate = useNavigate();

  const [username, setUsername] = useState("");
  const [password, setPassword] = useState("");
  const [role, setRole] = useState("User");
  const [message, setMessage] = useState("");

  async function handleSubmit(e) {
    e.preventDefault();
    setMessage(""); // Clear previous messages

    try {
      const res = await fetch("http://localhost:5000/api/Auth/register", {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify({ username, password, role })
      });

      const data = await res.json(); // ✅ Parse JSON properly

      if (res.ok) {
        setMessage(data.message || "✅ Registration successful"); // ✅ Display success
        setTimeout(() => navigate("/login"), 1000); // ⏳ Redirect to login
      } else {
        setMessage(data.message || "❌ Registration failed"); // ✅ Handle error
      }
    } catch (err) {
      setMessage("Server error. Please try again later."); // Fallback
    }
  }

  return (
    <form onSubmit={handleSubmit}>
      <h2>Register</h2>
      {message && <div>{message}</div>}
      <input
        type="text"
        placeholder="Username"
        value={username}
        onChange={e => setUsername(e.target.value)}
        required
      />
      <input
        type="password"
        placeholder="Password"
        value={password}
        onChange={e => setPassword(e.target.value)}
        required
      />
      <select value={role} onChange={e => setRole(e.target.value)}>
        <option value="User">User</option>
        <option value="Admin">Admin</option>
      </select>
      <button type="submit">Register</button>
    </form>
  );
}

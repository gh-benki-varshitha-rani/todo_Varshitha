import React, { useState } from "react";

export default function LoginForm({ onLogin }) {
  const [username, setUsername] = useState("");
  const [password, setPassword] = useState("");
  const [error, setError] = useState("");

  async function handleSubmit(e) {
    e.preventDefault();
    setError(""); // Clear previous error

    try {
      const res = await fetch("http://localhost:5000/api/Auth/login", {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify({ username, password })
      });

      const data = await res.json();

      if (res.ok && data.token) {
  localStorage.setItem("token", data.token);
  localStorage.setItem("username", data.username); // ✅ Save username
  localStorage.setItem("role", data.role);     
    localStorage.setItem("userId", data.userId); // ✅ Save userId here
    // ✅ Save role
  onLogin(); // 🔁 Trigger re-render or redirection
} else {
  setError(data.message || "Login failed");
}
    } catch (err) {
      setError("Server error. Please try again later.");
    }
  }

  return (
    <form onSubmit={handleSubmit}>
      <h2>Login</h2>
      {error && <div style={{ color: "red" }}>{error}</div>}
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
      <button type="submit">Login</button>
    </form>
  );
}


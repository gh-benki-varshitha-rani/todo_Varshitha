import React, { useEffect, useState } from "react";
import { getTodos, createTodo, updateTodo, deleteTodo } from "../services/api";
import TodoForm from "./TodoForm";

export default function TodoList() {
  const [todos, setTodos] = useState([]);
  const [editingTodo, setEditingTodo] = useState(null);
  const [users, setUsers] = useState([]);
  const [error, setError] = useState("");
  const [filter, setFilter] = useState("all");

  const token = localStorage.getItem("token");
  const role = localStorage.getItem("role");
  const userId = localStorage.getItem("userId");

  const filteredTodos = todos.filter(todo => {
    if (filter === "completed") return todo.isComplete;
    if (filter === "incomplete") return !todo.isComplete;
    return true;
  });

  useEffect(() => {
    if (role === "Admin") {
      fetch("http://localhost:5000/api/Admin/users", {
        headers: {
          Authorization: `Bearer ${token}`
        }
      })
        .then(res => res.json())
        .then(data => setUsers(data))
        .catch(err => console.error("❌ Failed to load users", err));
    }
  }, [role, token]);

  async function loadTodos() {
    try {
      const url =
        role === "Admin"
          ? "http://localhost:5000/api/Admin/tasks"
          : "http://localhost:5000/api/TodoItems";

      const res = await fetch(url, {
        headers: {
          Authorization: `Bearer ${token}`
        }
      });

      if (!res.ok) throw new Error(`Error ${res.status}`);
      const data = await res.json();
      setTodos(data);
    } catch (err) {
      console.error(err);
      setError("❌ Failed to load todos.");
    }
  }

  useEffect(() => {
    loadTodos();
  }, [token]);
async function handleAddOrUpdate(todo) {
      console.log("userId from localStorage:", userId);

  try {
    const payload = {
      title: todo.title?.trim(),
      description: todo.description || "",
      dueDate: todo.dueDate || new Date().toISOString(),
      isComplete: todo.isComplete ?? false,
      priority: parseInt(todo.priority || 1),
      ownerId: parseInt(todo.ownerId || userId),
      assignedToId: todo.assignedToId ? parseInt(todo.assignedToId) : null
    };
console.log("Payload before sending:", payload);

    // ✅ Validation before sending to API
    if (!payload.title || !payload.dueDate || !payload.priority || !payload.ownerId) {
      setError("❗ Please provide title, due date, priority, and owner.");
      return;
    }

    // 🔄 Create or Update
    if (todo.id) {
      await updateTodo(todo.id, payload); // PUT
    } else {
      await createTodo(payload); // POST
    }

    setEditingTodo(null);
    await loadTodos();
  } catch (err) {
    console.error(err);
    setError("❌ Failed to save todo.");
  }
}


  async function handleDelete(id) {
    try {
      await deleteTodo(id);
      await loadTodos();
    } catch (err) {
      console.error(err);
      setError("❌ Failed to delete todo.");
    }
  }

  async function handleToggleComplete(todo) {
    try {
      await updateTodo(todo.id, { ...todo, isComplete: !todo.isComplete });
      await loadTodos();
    } catch (err) {
      console.error(err);
      setError("❌ Failed to update todo status.");
    }
  }

  function handleEdit(todo) {
    setEditingTodo(todo);
  }

  function handleCancelEdit() {
    setEditingTodo(null);
  }

  return (
    <div>
      <h2>📝 Todo List</h2>

      {error && <div style={{ color: "red" }}>{error}</div>}

      {/* 🔍 Filter dropdown */}
      <div style={{ margin: "10px 0" }}>
        <label>Filter: </label>
        <select value={filter} onChange={(e) => setFilter(e.target.value)}>
          <option value="all">All</option>
          <option value="completed">✅ Completed</option>
          <option value="incomplete">🕗 Incomplete</option>
        </select>
      </div>

      <TodoForm
        onSave={handleAddOrUpdate}
        editingTodo={editingTodo}
        onCancel={editingTodo ? handleCancelEdit : null}
        users={users}
        role={role}
        userId={userId}
      />

      <ul style={{ padding: 0, listStyleType: "none" }}>
        {filteredTodos.map((todo) => (
          <li
            key={todo.id}
            style={{
              border: "1px solid #ddd",
              padding: "10px",
              margin: "10px 0",
              backgroundColor: todo.isComplete ? "#e6ffed" : "#fff"
            }}
          >
            <div>
              <strong style={{ textDecoration: todo.isComplete ? "line-through" : "none" }}>
                {todo.title}
              </strong>{" "}
              {todo.isComplete && <span style={{ color: "green" }}>✅</span>}
              {todo.description && ` - ${todo.description}`}
              {todo.dueDate && (
                <span style={{ marginLeft: 10, color: "#888" }}>
                  📅 Due: {new Date(todo.dueDate).toLocaleString()}
                </span>
              )}
              <span style={{ marginLeft: 10, color: "#888" }}>
                ⚡ Priority: {todo.priority}
              </span>

              {role === "Admin" && (
                <>
                  {todo.ownerName && <div>👤 Owner: {todo.ownerName}</div>}
                  {todo.assignedToName && <div>📌 Assigned To: {todo.assignedToName}</div>}
                </>
              )}
            </div>

            <div style={{ marginTop: 5 }}>
              <button onClick={() => handleEdit(todo)}>Edit</button>
              <button onClick={() => handleDelete(todo.id)} style={{ marginLeft: 8, color: "red" }}>
                Delete
              </button>
              <button
                onClick={() => handleToggleComplete(todo)}
                style={{ marginLeft: 8 }}
              >
                {todo.isComplete ? "Mark Incomplete" : "Mark Complete"}
              </button>
            </div>
          </li>
        ))}
      </ul>
    </div>
  );
}

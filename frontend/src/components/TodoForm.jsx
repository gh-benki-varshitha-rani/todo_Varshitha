import React, { useState, useEffect } from "react";

export default function TodoForm({ onSave, editingTodo, onCancel, users, role, userId }) {
  const [title, setTitle] = useState("");
  const [description, setDescription] = useState("");
  const [dueDate, setDueDate] = useState("");
  const [priority, setPriority] = useState("1");
  const [assignedToId, setAssignedToId] = useState("");
  const [isComplete, setIsComplete] = useState(false);

  useEffect(() => {
    if (editingTodo) {
      setTitle(editingTodo.title || "");
      setDescription(editingTodo.description || "");
      setDueDate(editingTodo.dueDate ? editingTodo.dueDate.slice(0, 16) : "");
      setPriority(editingTodo.priority?.toString() || "1");
      setAssignedToId(editingTodo.assignedToId?.toString() || "");
      setIsComplete(editingTodo.isComplete ?? false);
    } else {
      setTitle("");
      setDescription("");
      setDueDate("");
      setPriority("1");
      setAssignedToId("");
      setIsComplete(false);
    }
  }, [editingTodo]);

  function handleSubmit(e) {
    e.preventDefault();

    if (!title.trim() || !dueDate) {
      alert("Please provide title and due date.");
      return;
    }

    const todo = {
      title: title.trim(),
      description: description.trim(),
      dueDate: new Date(dueDate).toISOString(),
      priority: parseInt(priority),
      isComplete,
      ownerId: parseInt(userId)
    };

    if (role === "Admin") {
      todo.assignedToId = assignedToId ? parseInt(assignedToId) : null;
    }

    if (editingTodo?.id) {
      todo.id = editingTodo.id;
    }

    onSave(todo);

    if (!editingTodo) {
      setTitle("");
      setDescription("");
      setDueDate("");
      setPriority("1");
      setAssignedToId("");
      setIsComplete(false);
    }
  }

  return (
    <form onSubmit={handleSubmit} style={{ marginBottom: "20px" }}>
      <h3>{editingTodo ? "✏️ Edit Todo" : "➕ Add Todo"}</h3>

      <input
        type="text"
        placeholder="Title *"
        value={title}
        onChange={(e) => setTitle(e.target.value)}
        required
      />

      <input
        type="text"
        placeholder="Description"
        value={description}
        onChange={(e) => setDescription(e.target.value)}
      />

      <input
        type="datetime-local"
        value={dueDate}
        onChange={(e) => setDueDate(e.target.value)}
        required
      />

      <select value={priority} onChange={(e) => setPriority(e.target.value)}>
        <option value="1">🔥 High</option>
        <option value="2">⏳ Medium</option>
        <option value="3">🧊 Low</option>
      </select>

      <label style={{ display: "block", marginTop: "10px" }}>
        <input
          type="checkbox"
          checked={isComplete}
          onChange={(e) => setIsComplete(e.target.checked)}
        />{" "}
        Mark as Complete
      </label>

      {/* 👤 Admin Assign To */}
      {role === "Admin" && (
        <select
          value={assignedToId}
          onChange={(e) => setAssignedToId(e.target.value)}
          required
        >
          <option value="">-- Assign To --</option>
          {users.map((user) => (
            <option key={user.id} value={user.id}>
              {user.username} 
            </option>
          ))}
        </select>
      )}

      <button type="submit" style={{ marginTop: 10 }}>
        {editingTodo ? "Update" : "Add"}
      </button>
      {editingTodo && (
        <button type="button" onClick={onCancel} style={{ marginLeft: 8 }}>
          Cancel
        </button>
      )}
    </form>
  );
}

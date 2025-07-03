// src/api.js
import axios from "axios";

const API_URL = "http://localhost:5000/api";

// Create Axios instance
const api = axios.create({
  baseURL: API_URL,
  headers: {
    "Content-Type": "application/json",
  },
});

// ✅ Automatically attach token to every request
api.interceptors.request.use(
  (config) => {
    const token = localStorage.getItem("token");
    if (token) {
      config.headers.Authorization = `Bearer ${token}`;
    }
    return config;
  },
  (error) => {
    return Promise.reject(error);
  }
);

// ✅ Auth APIs
export const register = (username, password, role) =>
  api.post("/Auth/register", { username, password, role });

export const login = (username, password) =>
  api.post("/Auth/login", { username, password });

// ✅ Todo APIs
export const getTodos = () => api.get("/TodoItems");

export const createTodo = async (todo) => {
  const response = await api.post("/TodoItems", todo);
  return response.data;
};
export const updateTodo = async (id, todo) => {
  // Clone and remove the id if present in the payload
  const { id: _, ...payload } = todo;

  try {
    const response = await api.put(`/TodoItems/${id}`, payload);
    return response.data;
  } catch (error) {
    const msg = error.response?.data?.message || "Update failed";
    throw new Error(msg);
  }
};
export const deleteTodo = async (id) => {
  try {
    const response = await api.delete(`/TodoItems/${id}`);
    return response.data;
  } catch (error) {
    const msg = error.response?.data?.message || "Delete failed";
    throw new Error(msg);
  }
};

// Optional: Export the base instance for custom uses
export default api;

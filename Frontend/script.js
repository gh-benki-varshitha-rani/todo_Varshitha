const apiBase = "http://localhost:5248/api/TodoItems"; // Update port if needed
let editingId = null;

document.getElementById('todo-form').addEventListener('submit', async function (e) {
  e.preventDefault();

  const todo = {
    id: editingId || 0,
    title: document.getElementById('title').value,
    description: document.getElementById('description').value,
    dueDate: new Date(document.getElementById('dueDate').value).toISOString(),
    isComplete: false,
    priority: parseInt(document.getElementById('priority').value)
  };

  if (editingId) {
    await fetch(`${apiBase}/${editingId}`, {
      method: 'PUT',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify(todo)
    });
    showAlert("Task updated successfully!");
    editingId = null;
  } else {
    await fetch(apiBase, {
      method: 'POST',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify(todo)
    });
    showAlert("Task added successfully!");
  }

  document.getElementById('todo-form').reset();
  loadTodos();
});

async function loadTodos() {
  const res = await fetch(apiBase);
  let todos = await res.json();

  const filterValue = document.getElementById('filterStatus').value;
  const searchText = document.getElementById('searchTitle').value.toLowerCase(); // ✅ Fix here

  // Filter by status
  if (filterValue === "complete") {
    todos = todos.filter(t => t.isComplete);
  } else if (filterValue === "incomplete") {
    todos = todos.filter(t => !t.isComplete);
  }

  // Filter by search
  if (searchText) {
    todos = todos.filter(t => t.title.toLowerCase().includes(searchText));
  }

  const list = document.getElementById('todo-list');
  list.innerHTML = '';

  let completedCount = 0;

  todos.forEach(todo => {
    if (todo.isComplete) completedCount++;

    const li = document.createElement('li');
    li.className = "list-group-item d-flex justify-content-between align-items-start bg-light mb-2";
    li.innerHTML = `
      <div>
        <h5>${todo.title} ${'⭐'.repeat(todo.priority)}</h5>
        <small>${todo.description || ''}</small><br>
        <small>Due: ${new Date(todo.dueDate).toLocaleString()}</small><br>
        <strong>Status: ${todo.isComplete ? "✔️ Completed" : "⏳ Incomplete"}</strong>
      </div>
      <div>
        <button class="btn btn-sm btn-outline-primary me-1" onclick="startEdit(${todo.id})">✏️</button>
        <button class="btn btn-sm btn-outline-danger" onclick="deleteTodo(${todo.id})">🗑️</button>
        ${!todo.isComplete ? `<button class="btn btn-sm btn-outline-success mt-1" onclick="markComplete(${todo.id})">✔️ Done</button>` : ''}
      </div>
    `;
    list.appendChild(li);
  });

  // Update progress bar
  const progress = document.getElementById('completionBar');
  const percent = todos.length ? Math.round((completedCount / todos.length) * 100) : 0;
  progress.style.width = percent + "%";
  progress.textContent = `${percent}% Completed`;
}



async function markComplete(id) {
  const res = await fetch(`${apiBase}/${id}`);
  const todo = await res.json();
  todo.isComplete = true;

  await fetch(`${apiBase}/${id}`, {
    method: 'PUT',
    headers: { 'Content-Type': 'application/json' },
    body: JSON.stringify(todo)
  });
 showAlert("Task marked as complete!");
  loadTodos();
}

async function deleteTodo(id) {
  await fetch(`${apiBase}/${id}`, {
    method: 'DELETE'
  });
  showAlert("Task deleted successfully!", "danger");
  loadTodos();
}

async function startEdit(id) {
  const res = await fetch(`${apiBase}/${id}`);
  const todo = await res.json();

  document.getElementById('title').value = todo.title;
  document.getElementById('description').value = todo.description;
  document.getElementById('dueDate').value = todo.dueDate.slice(0, 16);
  document.getElementById('priority').value = todo.priority;
  editingId = id;
  window.scrollTo({ top: 0, behavior: 'smooth' });
  document.getElementById('todo-form').scrollIntoView({ behavior: 'smooth' });
}
function showAlert(message, type = "success") {
  const alert = document.createElement('div');
  alert.className = `alert alert-${type} alert-dismissible fade show position-fixed top-0 end-0 m-3`;
  alert.role = "alert";
  alert.innerHTML = `
    ${message}
    <button type="button" class="btn-close" data-bs-dismiss="alert" aria-label="Close"></button>
  `;
  document.body.appendChild(alert);

  setTimeout(() => {
    alert.classList.remove("show");
    alert.classList.add("hide");
    setTimeout(() => alert.remove(), 500);
  }, 10000);
}


// Event listeners for filter and search
document.getElementById('filterStatus').addEventListener('change', loadTodos);
document.getElementById('searchTitle').addEventListener('input', loadTodos);

// Initial load
loadTodos();

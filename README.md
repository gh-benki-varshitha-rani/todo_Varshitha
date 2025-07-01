# ToDo_Varshitha

A full-stack ToDo List web application built using **ASP.NET Core (.NET 9 Web API)** and a responsive frontend using **HTML, CSS, JavaScript, and Bootstrap**.

---

## 📝 Description

This project demonstrates a secure and feature-rich ToDo List app with the following capabilities:

- 🛡️ JWT-based authentication (login/register)
- ✅ Create, read, update, delete tasks
- 📊 Filter by status & search by title
- ⭐ Priority handling
- 🎯 Responsive frontend with Bootstrap
- 🔐 Backend secured with role-based access

---

## 🚀 Features

### ✅ API Features

- User authentication with JWT (login/register)
- CRUD operations on tasks
- Task filtering by status (Completed/Incompleted)
- Task search by title
- Priority management (1–5 stars)

### 🎨 Frontend Features

- Responsive design using Bootstrap
- Add/Edit/Delete/Complete tasks
- Priority selection using star rating
- Search and filter tasks
- Live updates via API

---

## 📁 Project Structure

TodoApi/
├── Controllers/ # Auth and Todo controllers
├── Models/ # User, Task models and DTOs
├── Services/ # AuthService, UserService, TodoService
├── Migrations/ # EF Core migrations
├── wwwroot/ # Frontend (index.html, CSS, JS)
├── Program.cs # Main entry point
├── appsettings.json # Configuration (JWT, DB)
└── README.md # Project documentation

yaml
Copy
Edit

---

## 🛠️ Technologies Used

- **.NET 9 Web API (ASP.NET Core)**
- **Entity Framework Core + SQLite**
- **HTML5, CSS3, JavaScript**
- **Bootstrap 5**
- **Visual Studio / VS Code**
- **Git & GitHub**

---

## 🖥️ Installation Instructions

1. **Clone the repo:**

```bash
git clone https://github.com/gh-benki-varshitha-rani/todo_Varshitha.git
cd todo_Varshitha
Restore dependencies:

bash
Copy
Edit
dotnet restore
Build the project:

bash
Copy
Edit
dotnet build
Run the backend API:

bash
Copy
Edit
dotnet run --project TodoApi
Visit the API docs:

bash
Copy
Edit
https://localhost:5000/swagger
Open index.html in a browser:

You can use Live Server in VS Code or open it directly to access the frontend.

🔐 Authentication Flow
Register a user at:

bash
Copy
Edit
POST /api/Auth/register
Login with credentials:

bash
Copy
Edit
POST /api/Auth/login
You'll receive a JWT token in the response.

Copy the token and use it as:

makefile
Copy
Edit
Authorization: Bearer <your-token-here>
Access secure endpoints like:

bash
Copy
Edit
GET /api/Todo
💻 Usage
On loading the page, all tasks are displayed.

Add tasks using the input form.

Use icons:

✏️ Edit

🗑️ Delete

✔️ Mark Complete

Use dropdown to filter tasks by status.

Use the search box to find tasks by title.

Prioritize using star ratings (1–5).

📷 Screenshots (optional)
You can include images like:

markdown
Copy
Edit
![Todo Screenshot](./screenshots/todo1.png)
🧠 How GitHub Copilot Helped
Generated boilerplate controller and model code

Suggested LINQ queries

Wrote frontend JS for fetch/POST/PUT/DELETE

Assisted in writing CSS and Bootstrap layout

🧪 Troubleshooting
.NET not recognized? Install .NET 9 SDK

Database issues? Delete TodoList.db and rerun

Frontend not showing tasks? Check console logs and API server

Port conflict? Update launchSettings.json

📜 License
This project is licensed under the MIT License.

🤝 Contributing
Contributions welcome! Fork the repo, make changes, and submit a pull request.


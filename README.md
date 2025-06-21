# todo_Varshitha
A ToDo List web application built using ASP.NET Core (.NET API) with a responsive frontend using HTML, CSS, JavaScript.

# ToDo List .NET Web App

## Description

A simple ToDo List web application built with ASP.NET Core. This project demonstrates how to create, read, update, and delete tasks using a RESTful API.

## Installation Instructions

To set up this project on your local machine:
1. Clone the repository:
    ```bash
    git clone https://github.com/your-username/todo_Varshitha.git
    cd todo_Varshitha
    ```
2. Restore dependencies:
    ```bash
    dotnet restore
    ```
3. Build the project:
    ```bash
    dotnet build
    ```
4. Run the application:
    ```bash
    dotnet run
    ```
5. Open your browser and navigate to `http://localhost:5248` (or the port specified in the output) to access the app.

## Repository

You can also clone the repository directly using the following command:

```bash
git clone https://github.com/gh-benki-varshitha-rani/todo_Varshitha.git
```

This will create a local copy of the project on your machine.
## Usage

2. Open the project folder in Visual Studio or VS Code.

3. Use the built-in terminal or IDE features to run and debug the application.

4. Interact with the API using tools like Postman or your browser, or use the provided frontend.

## Features

- Add, edit, and delete tasks
- Mark tasks as completed
- Responsive UI for desktop and mobile
- RESTful API built with ASP.NET Core

## License

This project is licensed under the MIT License.
## Contributing

Contributions are welcome! Please fork the repository and submit a pull request for any enhancements or bug fixes.

## Contact

For questions or feedback, please open an issue or contact the repository owner.
## Running the Backend API

5. Run the backend API:

    ```bash
    dotnet run --project TodoApi
    ```

   This will start the API server. By default, it will be available at `http://localhost:5248` (or the port specified in your launch settings).

You can now interact with the API endpoints using tools like Postman, curl, or your frontend application.
6. Open `index.html` in your browser to view the frontend interface and interact with the ToDo List application.
## How to Run the Project

- Ensure that the .NET 9 SDK is installed on your machine.
- Navigate to the project directory in your terminal.
- Start the backend by running the following command:
    ```
    dotnet run
    ```
- The API should now be running and accessible at the configured URL (typically `https://localhost:5248` or `http://localhost:5248`).
- Open `index.html` in your browser (you can use the `Live Server` extension in VS Code).
## Features

- Add new tasks
- Edit task title, description, and due date
- Delete tasks
- Mark tasks as completed
- Filter by task status (Completed / Incomplete)
- Search tasks by title
- Assign priority using 1–5 stars
- Responsive UI with Bootstrap styling
- Data persistence using EF Core and SQLite
## Usage

- On page load, all tasks will be listed.
- You can add a new task from the form at the top.
- Click ✏️ to edit, 🗑️ to delete, and ✔️ to mark as complete.
- Filter and search tasks using the dropdown and search box.

You can add screenshots here like:
![Todo Screenshot](./screenshots/todo1.png)
## Technologies Used

- C# with .NET 9 (ASP.NET Core Web API)
- Entity Framework Core
- HTML, CSS, JavaScript
- Bootstrap 5
- Visual Studio Code
## How GitHub Copilot Was Used

- Used to generate boilerplate API controller code
- Suggested LINQ queries and DTO conversion logic
- Helped write frontend JS functions like `fetch`, `POST`, `PUT`, and `DELETE`
- Assisted in writing clean and readable CSS styles
## Project Structure

```
TodoApi/
├── Controllers/         # API controllers for handling HTTP requests
├── Models/              # Data models and DTOs
├── Data/                # Database context and migrations
├── wwwroot/             # Static frontend files (HTML, CSS, JS)
├── Program.cs           # Application entry point
├── appsettings.json     # Configuration settings
├── TodoApi.csproj       # Project file
└── README.md            # Project documentation
```

This structure separates backend logic, data access, and frontend assets for maintainability and clarity.
## Troubleshooting

- If you encounter errors during `dotnet restore` or `dotnet build`, ensure you have the correct .NET SDK version installed.
- For port conflicts, update the launch settings or use a different port.
- If the frontend does not load tasks, check the browser console for API errors and verify the backend is running.
- For database issues, delete the `TodoApi.db` file and let EF Core recreate it on next run.

## Acknowledgements

- Inspired by the official Microsoft ASP.NET Core documentation.
- Thanks to the open-source community for helpful libraries and tools.
- Special thanks to GitHub Copilot for code suggestions and productivity boosts.
# ASP.NET Core MVC Project: Session Management System

## Description

This project is an ASP.NET Core MVC application designed to manage sessions and attendance. It supports user registration, authentication, session management, and attendance tracking, with different functionalities available based on user roles.

## References
Usj Course Documentation
GeeksForGeeks
ChatGpt
Teddy Smith ASP.NET MVC Playlist:https://www.youtube.com/watch?v=q2AcJmB03Io&list=PL82C6-O4XrHde_urqhKJHH-HTUfTK6siO

## Features

- **User Authentication**: Secure login and registration system.
- **Session Management**: Allows creation, modification, and browsing of sessions.
- **Attendance Tracking**: Users can register and track attendance for sessions.
- **Role-Based Access Control**: Different functionalities based on user roles.

## Tech Stack

- **.NET Core**: ASP.NET Core MVC for backend.
- **Entity Framework Core**: ORM for database operations.
- **SQL Server**: Database system.
- **Razor Views**: Frontend technology.
- **Bootstrap**: For styling and responsive design.

## Project Structure

### Controllers

- **HomeController**: Manages the home and privacy pages, error handling.
- **LoginController**: Handles user login processes, including session management and authentication.
- **RegisterController**: Manages user registration.
- **SessionController**: Responsible for session-related operations like creation, deletion, and edits.
- **AttendanceController**: Manages attendance recording and viewing.

### Models

- **User**: Defines properties for the user such as username, password, role, etc.
- **Session**: Defines properties for sessions including start time, end time, location, and purpose.
- **Attendance**: Tracks which users attended which sessions.
- **ErrorViewModel**: Handles error messaging by providing a request ID.

### ViewModels

- **LoginViewModel**: Includes fields necessary for logging in (username, password).
- **RegisterViewModel**: Used for registering a new user, includes fields like username, email, password, and confirmation password.
- **SessionViewModel**: Represents sessions, used when creating or editing sessions.
- **AttendanceViewModel**: Used for displaying and editing attendance details.
- **CreateSessionViewModel**: Represents the form for creating a new session.

### Views

- **Login.cshtml**: View for user login.
- **Register.cshtml**: View for user registration.
- **Index.cshtml**: Typically the home page or list views (various).
- **Create.cshtml**: View for creating a new entity (session or attendance).
- **Edit.cshtml**: View for editing an existing entity.
- **History.cshtml**: View for showing past records or logs.

## Setup and Installation

### Prerequisites

- .NET 5.0 SDK or later.
- SQL Server.
- Visual Studio 2019 or later.

### Database Setup

- Configure the connection string in `appsettings.json`.
- Apply migrations to set up the database:

```bash
dotnet ef migrations add InitialCreate
dotnet ef database update
```

### Running the Project

- Open the solution in Visual Studio.
- Restore NuGet packages.
- Run the application (F5).

## Usage

- **Home Page**: Accessed at the root URL. Provides links to other functionalities.
- **Register**: Accessed via `/Register`. New users can create an account.
- **Login**: Accessed via `/Login`. Users can log into the application.
- **Session Management**: Create and manage sessions once logged in.
- **Attendance Tracking**: Users can mark and view attendance.

## Contributing

Contributors are welcome. Please fork the repository and submit pull requests with your changes.

## License

[MIT](LICENSE.md)

---

This README aims to be comprehensive but may still need adjustments based on your specific project requirements and any additional functionalities or changes you make to the project. Adjust accordingly to fit the detailed aspects of your development and operational environment.

## Time Tracker

A simple time tracking tool that lets users manage people, tasks, and time records. People can be assigned work, log hours, and see how much time is spent on different activities.

Built with .NET and Angular, it showcases modern web development. 

The backend demonstrates fundamental principles of Clean Architecture, SOLID, and other best practices by utilising the separation of concerns, dependency inversion, use cases, domain entities, and more. 

The frontend demonstrates the current recommendations for Angular codebases, including standalone components, block template syntax, and reactive forms, and features a responsive UI powered by Bootstrap.

### Technologies Used

Backend: .NET 9, ASP.NET Core, Entity Framework, Scrutor, MS SQL Server

Frontend: Angular (v20), TypeScript, npm, Bootstrap 5, ng-bootstrap

### What's Missing
- The app implements only selected use cases; some key features (like managing people or editing tasks) are currently missing.
- Tests (unit and integration) for both the backend and frontend.
- CI checks using GitHub Actions.
- Proper code formatting and linting.
- Full adoption of the CQRS pattern (e.g., replacing repositories and domain entities in queries with database views, raw SQL, and a lightweight mapper like Dapper).
- Authentication and authorisation (intentionally out of scope for this project).

### Local Setup
- Install [.NET 9](https://dotnet.microsoft.com/download/dotnet/9.0) and the [EF CLI tools](https://learn.microsoft.com/en-us/ef/core/cli/dotnet) (`dotnet tool install --global dotnet-ef`).
- Install [Node.js](https://nodejs.org/en/download) (v22) and npm.
- In the `workbenchtimetracker.client` folder, run:
  - `npm i` to install Node.js dependencies.
- In the `WorkbenchTimeTracker.Server` folder, run:
  - `dotnet ef database update` to create a local database and apply migrations.
  - `dotnet run` to build and start the API server. This also automatically launches the dev server for the frontend.
- Open the app at https://localhost:55230/.

# Madridista Hub

Full-stack web application for managing Real Madrid squads, match schedules, staff, and sponsors. Built with ASP.NET Core MVC + Entity Framework Core + PostgreSQL.

## Tech Stack

- **Backend:** ASP.NET Core 8, Entity Framework Core, PostgreSQL (Npgsql)
- **Frontend:** Razor Views (server-rendered) + SPA component (LiveStats via fetch API)
- **Auth:** ASP.NET Core Identity with Admin and User roles
- **Architecture:** Repository Pattern + Service Layer + DTOs + Dependency Injection
- **API:** REST API at `/api/players` with Swagger UI
- **Containerization:** Docker + Docker Compose

## Features

- Squad management — players with stats (goals, assists, ratings per match)
- Match scheduling and results — fixtures with lineups and player ratings
- Staff directory — coaching and support staff per team
- Sponsor management — partnership tiers and logos
- User profiles — watchlist, favorite players, match comments
- LiveStats page — dynamic player stats fetched from REST API via JavaScript
- Admin panel — create, edit, delete all entities (Admin role required)

## Getting Started

### Prerequisites

- .NET 8 SDK
- PostgreSQL 16 (or Docker)

### Run locally

1. Clone the repository:
   ```bash
   git clone https://github.com/yourusername/Madridista-Hub.git
   cd Madridista-Hub
   ```

2. Set the connection string in `RealMadridWeb/appsettings.json`:
   ```json
   "ConnectionStrings": {
     "DefaultConnection": "Host=localhost;Port=5432;Database=realmadrid_cms;Username=your_user;Password=your_password"
   }
   ```

3. Apply migrations and run:
   ```bash
   cd RealMadridWeb
   dotnet ef database update
   dotnet run
   ```

4. Open `http://localhost:5090` in your browser.

### Run with Docker

```bash
docker-compose up --build
```

The app will be available at `http://localhost:8080`.

### Default admin account

On first run, `DataSeeder` automatically creates the Admin and User roles and a default admin account. Check `Data/DataSeeder.cs` for credentials.

## Project Structure

```
RealMadridWeb/
├── Controllers/        # MVC Razor controllers + PlayersApiController
├── DTOs/               # Data Transfer Objects (no direct EF entity exposure)
├── Models/             # EF Core entities
├── Repositories/       # Data access layer (interface + implementation)
├── Services/           # Business logic layer (interface + implementation)
├── Views/              # Razor .cshtml pages
├── Data/               # ApplicationDbContext + DataSeeder
├── Middleware/         # Global exception handler
├── ViewModels/         # Login, Register, Profile, Error view models
└── Migrations/         # EF Core migration history
```

## API Endpoints

| Method | Endpoint | Auth | Description |
|--------|----------|------|-------------|
| GET | `/api/players` | Public | Get all players |
| GET | `/api/players/{id}` | Public | Get player by ID |
| POST | `/api/players` | Admin | Create player (returns 201 + Location) |
| PUT | `/api/players/{id}` | Admin | Update player (returns 204) |
| DELETE | `/api/players/{id}` | Admin | Delete player (returns 204) |

Full API documentation available at `/swagger` when running in Development mode.

## Database Entities

- **Team** — One-to-Many with Player, Staff, Match
- **Player** — Many-to-Many with Match (via PlayerMatch)
- **Match** — stores fixtures, results, and venue info
- **PlayerMatch** — junction table with MinutesPlayed and Rating
- **Staff** — coaching and support staff per team
- **Sponsor** — Many-to-Many with Team (via TeamSponsor)
- **FavoritePlayer** — links IdentityUser to Player
- **WatchlistMatch** — links IdentityUser to Match
- **MatchComment** — user comments on matches

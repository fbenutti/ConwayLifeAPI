# Conway's Game of Life API

A production-ready C# API implementing Conway's Game of Life using .NET 9 and PostgreSQL.

## Features

- Upload initial board state
- Compute next generation
- Compute N generations ahead
- Simulate until stable (final) state with configurable max steps
- Board data persists via PostgreSQL
- Clean layered architecture (Controller → Service → DB)
- Unit tests for logic and controller behavior
- Fully Dockerized (API + PostgreSQL)
- Swagger UI enabled

## Getting Started

### Prerequisites

- [.NET 9 SDK](https://dotnet.microsoft.com/download)
- [Docker](https://www.docker.com/products/docker-desktop)

### Clone and Run with Docker

```bash
git clone https://github.com/fbenutti/ConwayLifeAPI.git
cd conway-life-api
docker-compose up --build
```

The API will be available at:
http://localhost:5000/swagger



### Run Without Docker (local only)

- Create a PostgreSQL database called conway
- Update appsettings.json with your connection string
- Run:

`dotnet ef database update`

`dotnet run`
# SharedNotepad

## What Docker Options Exist In This Repo

1. `docker-compose.yml`
Full stack run (Postgres + Backend + Frontend).

2. `src/API/Backend/Dockerfile`
Backend-only image.

3. `frontend/Dockerfile`
Frontend-only image.

## Prerequisites

- Docker Desktop running.
- Free ports:
  - `3000` frontend
  - `8080` backend
  - `5432` full-stack compose postgres
  - `55432` standalone postgres for backend-only flow

## Option A: Full Stack Run (Compose)

Start:

```powershell
docker compose -f docker-compose.yml up -d --build
```

Validate:

```powershell
curl http://localhost:8080/health
curl http://localhost:3000
```

Stop:

```powershell
docker compose -f docker-compose.yml down
```

## Option B: Standalone Backend + Frontend (No Compose)

### 1) Start Postgres (exact setup)

```powershell
docker rm -f pg-backsolo 2>$null
docker volume rm pg_backsolo_data 2>$null
docker run -d --name pg-backsolo `
  -e POSTGRES_DB=shared_notepad `
  -e POSTGRES_USER=postgres `
  -e POSTGRES_PASSWORD=postgres `
  -p 55432:5432 `
  -v pg_backsolo_data:/var/lib/postgresql/data `
  postgres:17-alpine
```

Verify DB:

```powershell
docker exec -it pg-backsolo psql -U postgres -d shared_notepad -c "select 1;"
```

### 2) Build and run backend

```powershell
docker build -f src/API/Backend/Dockerfile -t sharednotepad-backend:latest .
docker rm -f back-solo 2>$null
docker run -d --name back-solo -p 8080:8080 `
  -e ASPNETCORE_URLS=http://0.0.0.0:8080 `
  -e "ConnectionStrings__DefaultConnection=Host=host.docker.internal;Port=55432;Database=shared_notepad;Username=postgres;Password=postgres" `
  sharednotepad-backend:latest
```

Verify backend:

```powershell
curl http://localhost:8080/health
```

### 3) Build and run frontend

```powershell
docker build -f frontend/Dockerfile -t front:latest frontend
docker rm -f front-solo 2>$null
docker run -d --name front-solo -p 3000:80 `
  -e API_BASE_URL=http://localhost:8080 `
  front:latest
```

Open:
- `http://localhost:3000`

## Rider Setup Notes

### Docker.Backend (`back-solo`)

- Dockerfile: `src/API/Backend/Dockerfile`
- Context folder: `.`
- Fast Mode: `OFF`
- Port binding: `8080:8080`
- Env vars:
  - `ASPNETCORE_URLS=http://0.0.0.0:8080`
  - `ConnectionStrings__DefaultConnection=Host=host.docker.internal;Port=55432;Database=shared_notepad;Username=postgres;Password=postgres`

### Docker.Frontend (`front-solo`)

- Dockerfile: `frontend/Dockerfile`
- Fast Mode: `OFF`
- Port binding: `3000:80`
- Env var:
  - `API_BASE_URL=http://localhost:8080`

### Docker.FullProject

- Compose file: `docker-compose.yml`
- In compose mode backend DB host is `postgres` (service name), not `host.docker.internal`.

## Troubleshooting

- `failed to calculate checksum ... *.csproj not found`
Set Rider Dockerfile context to project root `.`.

- `no command specified`
Disable Fast Mode in Rider Docker run config.

- Backend starts but not reachable
Publish port `8080:8080`.

- Backend log shows `localhost:5432`
Connection string env var was not applied.

- `password authentication failed for user "postgres"`
Wrong DB instance or stale volume. Recreate `pg-backsolo` + `pg_backsolo_data`.

FROM mcr.microsoft.com/dotnet/sdk:10.0 AS builder
WORKDIR /src

COPY ["SharedNotepad.sln", "./"]
COPY ["src/API/Backend/Backend.csproj", "src/API/Backend/"]
COPY ["src/Domain/SharedNotepad.Core/SharedNotepad.Core.csproj", "src/Domain/SharedNotepad.Core/"]
COPY ["src/Infrastructure/SharedNotepad.Infrastructure/SharedNotepad.Infrastructure.csproj", "src/Infrastructure/SharedNotepad.Infrastructure/"]

RUN dotnet restore "src/API/Backend/Backend.csproj"

COPY . .
RUN dotnet publish "src/API/Backend/Backend.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS build
WORKDIR /app

ENV ASPNETCORE_URLS=http://+:8080
ENV ConnectionStrings__DefaultConnection="Host=host.docker.internal;Port=55432;Database=shared_notepad;Username=postgres;Password=postgres"
EXPOSE 8080

COPY --from=builder /app/publish .
ENTRYPOINT ["dotnet", "Backend.dll"]

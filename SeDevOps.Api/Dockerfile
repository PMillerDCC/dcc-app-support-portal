# -----------------------------
# 1. Build stage
# -----------------------------
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy solution and restore dependencies
COPY *.sln .
COPY SeDevOps.Api/SeDevOps.Api.csproj SeDevOps.Api/
COPY SeDevOps.Data/SeDevOps.Data.csproj SeDevOps.Data/

RUN dotnet restore

# Copy the rest of the source code
COPY . .

# Build the API
RUN dotnet publish SeDevOps.Api/SeDevOps.Api.csproj -c Release -o /app/publish


# -----------------------------
# 2. Runtime stage
# -----------------------------
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app

# Copy published output
COPY --from=build /app/publish .

# Expose API port (matches your launchSettings.json)
EXPOSE 7106

# Start the API
ENTRYPOINT ["dotnet", "SeDevOps.Api.dll"]
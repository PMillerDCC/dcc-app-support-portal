# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy only the API project file
COPY SeDevOps.Api.csproj .

# Copy the Data project file
COPY ../SeDevOps.Data/SeDevOps.Data.csproj ../SeDevOps.Data/

# Restore dependencies
RUN dotnet restore SeDevOps.Api.csproj

# Copy the entire repo (API + Data)
COPY . .
COPY ../SeDevOps.Data ../SeDevOps.Data

# Publish the API
RUN dotnet publish SeDevOps.Api.csproj -c Release -o /app/publish

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /app/publish .

ENV ASPNETCORE_URLS=http://0.0.0.0:${PORT}

ENTRYPOINT ["dotnet", "SeDevOps.Api.dll"]
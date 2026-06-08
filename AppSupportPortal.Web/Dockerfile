# -----------------------------
# 1. Build stage
# -----------------------------
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy solution and restore dependencies
COPY *.sln .
COPY AppSupportPortal.Web/AppSupportPortal.Web.csproj AppSupportPortal.Web/
COPY SeDevOps.Data/SeDevOps.Data.csproj SeDevOps.Data/

RUN dotnet restore

# Copy the rest of the source code
COPY . .

# Build and publish the MVC app
RUN dotnet publish AppSupportPortal.Web/AppSupportPortal.Web.csproj -c Release -o /app/publish


# -----------------------------
# 2. Runtime stage
# -----------------------------
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app

# Copy published output
COPY --from=build /app/publish .

# Expose MVC port (matches your launchSettings.json)
EXPOSE 7180

# Start the MVC app
ENTRYPOINT ["dotnet", "AppSupportPortal.Web.dll"]
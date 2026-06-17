# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy project files
COPY SeDevOps.Api/SeDevOps.Api.csproj SeDevOps.Api/
COPY SeDevOps.Data/SeDevOps.Data.csproj SeDevOps.Data/
COPY AppSupportPortal.Web/AppSupportPortal.Web.csproj AppSupportPortal.Web/

# Restore dependencies
RUN dotnet restore SeDevOps.Api/SeDevOps.Api.csproj

# Copy everything else
COPY . .

# Publish the API
RUN dotnet publish SeDevOps.Api/SeDevOps.Api.csproj -c Release -o /app/publish


# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app

# Copy published output
COPY --from=build /app/publish .

# Render exposes PORT dynamically
ENV ASPNETCORE_URLS=http://0.0.0.0:${PORT}

# Start the API
ENTRYPOINT ["dotnet", "SeDevOps.Api.dll"]
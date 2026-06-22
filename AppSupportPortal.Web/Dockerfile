# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy project files
COPY AppSupportPortal.Web/AppSupportPortal.Web.csproj AppSupportPortal.Web/
COPY SeDevOps.Api/SeDevOps.Api.csproj SeDevOps.Api/
COPY SeDevOps.Data/SeDevOps.Data.csproj SeDevOps.Data/

# Restore dependencies
RUN dotnet restore AppSupportPortal.Web/AppSupportPortal.Web.csproj

# Copy everything else
COPY . .

# Publish the app
RUN dotnet publish AppSupportPortal.Web/AppSupportPortal.Web.csproj -c Release -o /app/publish

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /app/publish .

# Render provides PORT dynamically
ENV ASPNETCORE_URLS=http://0.0.0.0:${PORT}

ENTRYPOINT ["dotnet", "AppSupportPortal.Web.dll"]
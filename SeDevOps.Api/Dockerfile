# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY SeDevOps.Api.csproj .
COPY SeDevOps.Data/SeDevOps.Data.csproj SeDevOps.Data/

RUN dotnet restore SeDevOps.Api.csproj

COPY . .

RUN dotnet publish SeDevOps.Api.csproj -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /app/publish .

ENV ASPNETCORE_URLS=http://0.0.0.0:${PORT}

ENTRYPOINT ["dotnet", "SeDevOps.Api.dll"]
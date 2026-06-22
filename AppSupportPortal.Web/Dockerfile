FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY AppSupportPortal.Web.csproj .
COPY SeDevOps.Data/SeDevOps.Data.csproj SeDevOps.Data/

RUN dotnet restore AppSupportPortal.Web.csproj

COPY . .

RUN dotnet publish AppSupportPortal.Web.csproj -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /app/publish .

ENV ASPNETCORE_URLS=http://0.0.0.0:${PORT}

ENTRYPOINT ["dotnet", "AppSupportPortal.Web.dll"]
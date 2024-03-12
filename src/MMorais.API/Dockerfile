#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/MMorais.API/MMorais.API.csproj", "src/MMorais.API/"]
COPY ["src/MMorais.CrossCutting/MMorais.CrossCutting.csproj", "src/MMorais.CrossCutting/"]
COPY ["src/MMorais.Application/MMorais.Application.csproj", "src/MMorais.Application/"]
COPY ["src/MMorais.Domain/MMorais.Domain.csproj", "src/MMorais.Domain/"]
COPY ["src/MMorais.Core/MMorais.Core.csproj", "src/MMorais.Core/"]
COPY ["src/MMorais.Infrastructure/MMorais.Infrastructure.csproj", "src/MMorais.Infrastructure/"]
RUN dotnet restore "./src/MMorais.API/MMorais.API.csproj"
COPY . .
WORKDIR "/src/src/MMorais.API"
RUN dotnet build "./MMorais.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./MMorais.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MMorais.API.dll"]
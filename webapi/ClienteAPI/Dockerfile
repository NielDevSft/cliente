#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.
ARG VERSION=8.0

FROM mcr.microsoft.com/dotnet/aspnet:$VERSION AS base
USER app
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:$VERSION AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

COPY ./*.sln .
COPY ./ClienteAPI.API/*.csproj ClienteAPI.API/
COPY ./ClienteAPI.Common/*.csproj ClienteAPI.Common/
COPY ./ClienteAPI.Domain/*.csproj ClienteAPI.Domain/
COPY ./ClienteAPI.Domain.Core/*.csproj ClienteAPI.Domain.Core/
COPY ./ClienteAPI.Persistence/*.csproj ClienteAPI.Persistence/
COPY . .
RUN dotnet restore 


WORKDIR /src/ClienteAPI.API
RUN dotnet build ./ClienteAPI.API.csproj -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish ./ClienteAPI.API.csproj -c $BUILD_CONFIGURATION -o /app/publish --no-restore

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

ENTRYPOINT dotnet ClienteAPI.API.dll
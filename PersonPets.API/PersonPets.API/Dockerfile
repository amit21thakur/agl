#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["PersonPets.API/PersonPets.API.csproj", "PersonPets.API/"]
RUN dotnet restore "PersonPets.API/PersonPets.API.csproj"
COPY . .
WORKDIR "/src/PersonPets.API"
RUN dotnet build "PersonPets.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "PersonPets.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PersonPets.API.dll"]
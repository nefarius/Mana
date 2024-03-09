#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["src/Mana.csproj", "Mana/"]
RUN dotnet restore "Mana/Mana.csproj"
COPY . .
WORKDIR "/src/Mana"
RUN dotnet build "Mana.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Mana.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Mana.dll"]
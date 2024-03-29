FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Client/Client.csproj", "Client/"]
COPY ["Shared/Shared.csproj", "Shared/"]
RUN dotnet restore "Client/Client.csproj"
COPY . .
WORKDIR "/src/Client"
RUN apt-get update
RUN apt-get install -y procps
RUN apt-get install -y unzip
RUN dotnet build "Client.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Client.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Client.dll"]
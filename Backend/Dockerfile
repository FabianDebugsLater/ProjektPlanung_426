FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src
COPY ["Backend/Backend.csproj", "Backend/"]
RUN dotnet restore "Backend/Backend.csproj" -f net9.0
COPY . .
WORKDIR "/src/Backend"
RUN dotnet build "Backend.csproj" -c Release -f net9.0 -o /app/build

FROM build AS publish
RUN dotnet publish "Backend.csproj" -c Release -f net9.0 -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Backend.dll"]
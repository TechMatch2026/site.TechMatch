FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ["TechMatch.backend.csproj", "./"]
RUN dotnet restore "TechMatch.backend.csproj"

COPY . .
RUN dotnet publish "TechMatch.backend.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app

COPY --from=build /app/publish .

EXPOSE 10000

ENTRYPOINT ["dotnet", "TechMatch.backend.dll"]

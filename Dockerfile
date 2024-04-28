# https://hub.docker.com/_/microsoft-dotnet
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# copy csproj and restore as distinct layers
COPY AmveraWeather/AmveraWeather/*.csproj ./AmveraWeather/AmveraWeather/
COPY AmveraWeather/AmveraWeather.Client/*.csproj ./AmveraWeather/AmveraWeather.Client/
RUN dotnet restore AmveraWeather/AmveraWeather/AmveraWeather.csproj


# copy everything else and build app
COPY . .
WORKDIR /src/AmveraWeather/AmveraWeather
RUN dotnet publish "AmveraWeather.csproj" -c release -o /app/publish

# final stage/image
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "AmveraWeather.dll"]
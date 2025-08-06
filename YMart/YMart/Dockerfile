FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Copy the project file and restore dependencies
COPY ["YMart/YMart.csproj", "YMart/"]
RUN dotnet restore "./YMart/YMart.csproj"

# Copy the rest of the source code
COPY . .

# Set working directory to the project folder
WORKDIR "/src/YMart"

# Set environment to Development
ENV ASPNETCORE_ENVIRONMENT=Development

# Build the project
RUN dotnet build "YMart.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "YMart.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app

# Copy published output
COPY --from=publish /app/publish .

#  Copy the development settings file manually
COPY YMart/appsettings.Development.json ./appsettings.Development.json

ENTRYPOINT ["dotnet", "YMart.dll"]
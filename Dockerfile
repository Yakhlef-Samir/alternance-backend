# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy csproj files and restore dependencies
COPY src/Alternance.Api/Alternance.Api.csproj src/Alternance.Api/
COPY src/Alternance.Application/Alternance.Application.csproj src/Alternance.Application/
COPY src/Alternance.Domain/Alternance.Domain.csproj src/Alternance.Domain/
COPY src/Alternance.Infrastructure/Alternance.Infrastructure.csproj src/Alternance.Infrastructure/

RUN dotnet restore src/Alternance.Api/Alternance.Api.csproj

# Copy everything else and build
COPY . .
WORKDIR /src/src/Alternance.Api
RUN dotnet build -c Release -o /app/build

# Publish stage
FROM build AS publish
RUN dotnet publish -c Release -o /app/publish

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
EXPOSE 80
EXPOSE 443

COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Alternance.Api.dll"]

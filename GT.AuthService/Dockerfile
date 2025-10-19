# Stage 1: Build the application
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build-env
WORKDIR /app

# Copy only the .sln and .csproj files first for better caching
COPY GT.AuthService.sln .
COPY GT.AuthService.Api/GT.AuthService.Api.csproj GT.AuthService.Api/
COPY GT.AuthService.Domain/GT.AuthService.Domain.csproj GT.AuthService.Domain/
COPY GT.AuthService.Application/GT.AuthService.Application.csproj GT.AuthService.Application/
COPY GT.AuthService.Infrastructure/GT.AuthService.Infrastructure.csproj GT.AuthService.Infrastructure/
COPY GT.AuthService.Test/GT.AuthService.Test.csproj GT.AuthService.Test/

# Restore dependencies
RUN dotnet restore "GT.AuthService.sln"

# Copy the rest of the source code
COPY . .

# Publish the application
WORKDIR /app/GT.AuthService.Api
RUN dotnet publish "GT.AuthService.Api.csproj" -c Release -o /app/out

# Stage 2: Create the final runtime image
FROM mcr.microsoft.com/dotnet/aspnet:9.0
WORKDIR /app
COPY --from=build-env /app/out .
ENTRYPOINT ["dotnet", "GT.AuthService.Api.dll"]

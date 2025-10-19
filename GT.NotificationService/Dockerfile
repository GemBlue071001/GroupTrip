# Stage 1: Build the application
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /app

# Copy only the .sln and .csproj files first for better caching
COPY GT.NotificationService.sln .
COPY Api/GT.NotificationService.Api/GT.NotificationService.Api.csproj Api/GT.NotificationService.Api/
COPY Domain/GT.NotificationService.Domain/GT.NotificationService.Domain.csproj Domain/GT.NotificationService.Domain/
COPY Application/GT.NotificationService.Application/GT.NotificationService.Application.csproj Application/GT.NotificationService.Application/
COPY Infrastructure/GT.NotificationService.Infrastructure/GT.NotificationService.Infrastructure.csproj Infrastructure/GT.NotificationService.Infrastructure/
# If you have a test project, include it here:
COPY Test/GT.NotificationService.Test/GT.NotificationService.Test.csproj Test/GT.NotificationService.Test/

# Restore dependencies
RUN dotnet restore "GT.NotificationService.sln"

# Copy the rest of the source code
COPY . .

# Publish the application
WORKDIR /app/Api/GT.NotificationService.Api
RUN dotnet publish "GT.NotificationService.Api.csproj" -c Release -o /app/out

# Stage 2: Create the final runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build-env /app/out .
ENTRYPOINT ["dotnet", "GT.NotificationService.Api.dll"]

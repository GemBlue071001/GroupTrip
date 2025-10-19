# Stage 1: Build the application
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /app

# Copy only the .sln and .csproj files first for better caching
COPY GT.TripManagementService.sln .
COPY Api/GT.TripManagementService.Api/GT.TripManagementService.Api.csproj Api/GT.TripManagementService.Api/
COPY Domain/GT.TripManagementService.Domain/GT.TripManagementService.Domain.csproj Domain/GT.TripManagementService.Domain/
COPY Application/GT.TripManagementService.Application/GT.TripManagementService.Application.csproj Application/GT.TripManagementService.Application/
COPY Infrastructure/GT.TripManagementService.Infrastructure/GT.TripManagementService.Infrastructure.csproj Infrastructure/GT.TripManagementService.Infrastructure/
# If you have a test project, include it here:
COPY Test/GT.TripManagementService.Test/GT.TripManagementService.Test.csproj Test/GT.TripManagementService.Test/

# Restore dependencies
RUN dotnet restore "GT.TripManagementService.sln"

# Copy the rest of the source code
COPY . .

# Publish the application
WORKDIR /app/Api/GT.TripManagementService.Api
RUN dotnet publish "GT.TripManagementService.Api.csproj" -c Release -o /app/out

# Stage 2: Create the final runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build-env /app/out .
ENTRYPOINT ["dotnet", "GT.TripManagementService.Api.dll"]

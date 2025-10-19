# Stage 1: Build the application
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /app

# Copy only the .sln and .csproj files first for better caching
COPY GT.MediaService.sln .
COPY Api/GT.MediaService.Api/GT.MediaService.Api.csproj Api/GT.MediaService.Api/
COPY Domain/GT.MediaService.Domain/GT.MediaService.Domain.csproj Domain/GT.MediaService.Domain/
COPY Application/GT.MediaService.Application/GT.MediaService.Application.csproj Application/GT.MediaService.Application/
COPY Infrastructure/GT.MediaService.Infrastructure/GT.MediaService.Infrastructure.csproj Infrastructure/GT.MediaService.Infrastructure/
# If you have a test project, include it here:
COPY Test/GT.MediaService.Test/GT.MediaService.Test.csproj Test/GT.MediaService.Test/

# Restore dependencies
RUN dotnet restore "GT.MediaService.sln"

# Copy the rest of the source code
COPY . .

# Publish the application
WORKDIR /app/Api/GT.MediaService.Api
RUN dotnet publish "GT.MediaService.Api.csproj" -c Release -o /app/out

# Stage 2: Create the final runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build-env /app/out .
ENTRYPOINT ["dotnet", "GT.MediaService.Api.dll"]

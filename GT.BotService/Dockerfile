# Stage 1: Build the application
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /app

# Copy only the .sln and .csproj files first for better caching
COPY GT.BotService.sln .
COPY Api/GT.BotService.Api/GT.BotService.Api.csproj Api/GT.BotService.Api/
COPY Domain/GT.BotService.Domain/GT.BotService.Domain.csproj Domain/GT.BotService.Domain/
COPY Application/GT.BotService.Application/GT.BotService.Application.csproj Application/GT.BotService.Application/
COPY Infrastructure/GT.BotService.Infrastructure/GT.BotService.Infrastructure.csproj Infrastructure/GT.BotService.Infrastructure/
# If you have a test project, include it here:
COPY Test/GT.BotService.Test/GT.BotService.Test.csproj Test/GT.BotService.Test/

# Restore dependencies
RUN dotnet restore "GT.BotService.sln"

# Copy the rest of the source code
COPY . .

# Publish the application
WORKDIR /app/Api/GT.BotService.Api
RUN dotnet publish "GT.BotService.Api.csproj" -c Release -o /app/out

# Stage 2: Create the final runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build-env /app/out .
ENTRYPOINT ["dotnet", "GT.BotService.Api.dll"]

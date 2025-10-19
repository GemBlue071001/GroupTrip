# Stage 1: Build the application
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /app

# Copy only the .sln and .csproj files first for better caching
COPY GT.BookingService.sln .
COPY Api/GT.BookingService.Api/GT.BookingService.Api.csproj Api/GT.BookingService.Api/
COPY Domain/GT.BookingService.Domain/GT.BookingService.Domain.csproj Domain/GT.BookingService.Domain/
COPY Application/GT.BookingService.Application/GT.BookingService.Application.csproj Application/GT.BookingService.Application/
COPY Infrastructure/GT.BookingService.Infrastructure/GT.BookingService.Infrastructure.csproj Infrastructure/GT.BookingService.Infrastructure/
# If you have a test project, include it here:
COPY Test/GT.BookingService.Test/GT.BookingService.Test.csproj Test/GT.BookingService.Test/

# Restore dependencies
RUN dotnet restore "GT.BookingService.sln"

# Copy the rest of the source code
COPY . .

# Publish the application
WORKDIR /app/Api/GT.BookingService.Api
RUN dotnet publish "GT.BookingService.Api.csproj" -c Release -o /app/out

# Stage 2: Create the final runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build-env /app/out .
ENTRYPOINT ["dotnet", "GT.BookingService.Api.dll"]

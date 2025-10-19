# Stage 1: Build the application
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /app

# Copy only the .sln and .csproj files first for better caching
COPY GT.PaymentService.sln .
COPY Api/GT.PaymentService.Api/GT.PaymentService.Api.csproj Api/GT.PaymentService.Api/
COPY Domain/GT.PaymentService.Domain/GT.PaymentService.Domain.csproj Domain/GT.PaymentService.Domain/
COPY Application/GT.PaymentService.Application/GT.PaymentService.Application.csproj Application/GT.PaymentService.Application/
COPY Infrastructure/GT.PaymentService.Infrastructure/GT.PaymentService.Infrastructure.csproj Infrastructure/GT.PaymentService.Infrastructure/
# If you have a test project, include it here:
COPY Test/GT.PaymentService.Test/GT.PaymentService.Test.csproj Test/GT.PaymentService.Test/

# Restore dependencies
RUN dotnet restore "GT.PaymentService.sln"

# Copy the rest of the source code
COPY . .

# Publish the application
WORKDIR /app/Api/GT.PaymentService.Api
RUN dotnet publish "GT.PaymentService.Api.csproj" -c Release -o /app/out

# Stage 2: Create the final runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build-env /app/out .
ENTRYPOINT ["dotnet", "GT.PaymentService.Api.dll"]

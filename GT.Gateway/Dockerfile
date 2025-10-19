# Stage 1: Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /app

COPY GT.Gateway.sln .
COPY GT.Gateway.Api/GT.Gateway.Api.csproj GT.Gateway.Api/ 
RUN dotnet restore "GT.Gateway.sln"

COPY . .
WORKDIR /app/GT.Gateway.Api
# RUN dotnet publish "GT.Gateway.Api.csproj" -c Release -o /app/out
RUN dotnet publish -c Release -o /app/out

# Stage 2: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build-env /app/out .
ENTRYPOINT ["dotnet", "GT.Gateway.Api.dll"]


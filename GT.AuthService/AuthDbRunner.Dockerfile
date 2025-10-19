FROM mcr.microsoft.com/dotnet/sdk:9.0 AS runner
WORKDIR /app

COPY . .
RUN dotnet tool install --global dotnet-ef
ENV PATH="$PATH:/root/.dotnet/tools"

# Service này chỉ chạy migrate
ENTRYPOINT ["dotnet", "ef", "database", "update", "--project", "GT.AuthService.Infrastructure", "--startup-project", "GT.AuthService.Api"]


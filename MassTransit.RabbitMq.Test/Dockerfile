FROM mcr.microsoft.com/dotnet/runtime:8.0 AS base
USER $APP_UID
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["MassTransit.RabbitMq.Test/MassTransit.RabbitMq.Test.csproj", "MassTransit.RabbitMq.Test/"]
RUN dotnet restore "MassTransit.RabbitMq.Test/MassTransit.RabbitMq.Test.csproj"
COPY . .
WORKDIR "/src/MassTransit.RabbitMq.Test"
RUN dotnet build "MassTransit.RabbitMq.Test.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "MassTransit.RabbitMq.Test.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MassTransit.RabbitMq.Test.dll"]

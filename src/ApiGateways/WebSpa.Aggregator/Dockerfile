FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
ENV ASPNETCORE_URLS=http://+:80
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src

# Keeping the the first lines the same for every Dockerfile takes advantage of Dockers caching mechanism
COPY "Kwetter.sln" "Kwetter.sln"

COPY "BuildingBlocks/EventBus/EventBus/EventBus.csproj" "BuildingBlocks/EventBus/EventBus/EventBus.csproj"
COPY "BuildingBlocks/EventBus/EventBusRabbitMQ/EventBusRabbitMQ.csproj" "BuildingBlocks/EventBus/EventBusRabbitMQ/EventBusRabbitMQ.csproj"
COPY "BuildingBlocks/Abstractions/Abstractions.csproj" "BuildingBlocks/Abstractions/Abstractions.csproj"
COPY "BuildingBlocks/Configurations/Configurations.csproj" "BuildingBlocks/Configurations/Configurations.csproj"
COPY "BuildingBlocks/CQRS/CQRS.csproj" "BuildingBlocks/CQRS/CQRS.csproj"
COPY "BuildingBlocks/IdentityBlocks/IdentityBlocks.csproj" "BuildingBlocks/IdentityBlocks/IdentityBlocks.csproj"
COPY "BuildingBlocks/KwetterDomain/KwetterDomain.csproj" "BuildingBlocks/KwetterDomain/KwetterDomain.csproj"
COPY "BuildingBlocks/KwetterGrpc/KwetterGrpc.csproj" "BuildingBlocks/KwetterGrpc/KwetterGrpc.csproj"
COPY "BuildingBlocks/KwetterLogger/KwetterLogger.csproj" "BuildingBlocks/KwetterLogger/KwetterLogger.csproj"
COPY "BuildingBlocks/KwetterSwagger/KwetterSwagger.csproj" "BuildingBlocks/KwetterSwagger/KwetterSwagger.csproj"

COPY "Services/Tweet/Api/Tweet.Application/Tweet.Application.csproj" "Services/Tweet/Api/Tweet.Application/Tweet.Application.csproj"
COPY "Services/Tweet/Api/Tweet.Domain/Tweet.Domain.csproj" "Services/Tweet/Api/Tweet.Domain/Tweet.Domain.csproj"
COPY "Services/Tweet/Api/Tweet.Infrastructure/Tweet.Infrastructure.csproj" "Services/Tweet/Api/Tweet.Infrastructure/Tweet.Infrastructure.csproj"
COPY "Services/Tweet/Api/Tweet.Api/Tweet.Api.csproj" "Services/Tweet/Api/Tweet.Api/Tweet.Api.csproj"

COPY "Services/Tweet/Tweet.Events/Tweet.Events.csproj" "Services/Tweet/Tweet.Events/Tweet.Events.csproj"
COPY "Services/Tweet/Tweet.GrpcContracts/Tweet.GrpcContracts.csproj" "Services/Tweet/Tweet.GrpcContracts/Tweet.GrpcContracts.csproj"
COPY "Services/Tweet/Tweet.Hub/Tweet.Hub.csproj" "Services/Tweet/Tweet.Hub/Tweet.Hub.csproj"

COPY "Services/UserRelations/Api/UserRelations.Api/UserRelations.Api.csproj" "Services/UserRelations/Api/UserRelations.Api/UserRelations.Api.csproj"
COPY "Services/UserRelations/Api/UserRelations.Application/UserRelations.Application.csproj" "Services/UserRelations/Api/UserRelations.Application/UserRelations.Application.csproj"
COPY "Services/UserRelations/Api/UserRelations.Domain/UserRelations.Domain.csproj" "Services/UserRelations/Api/UserRelations.Domain/UserRelations.Domain.csproj"
COPY "Services/UserRelations/Api/UserRelations.Infrastructure/UserRelations.Infrastructure.csproj" "Services/UserRelations/Api/UserRelations.Infrastructure/UserRelations.Infrastructure.csproj"

COPY "Services/UserRelations/UserRelations.Events/UserRelations.Events.csproj" "Services/UserRelations/UserRelations.Events/UserRelations.Events.csproj"
COPY "Services/UserRelations/UserRelations.GrpcContracts/UserRelations.GrpcContracts.csproj" "Services/UserRelations/UserRelations.GrpcContracts/UserRelations.GrpcContracts.csproj"

COPY "Services/Identity/Identity.Api/Identity.Api.csproj" "Services/Identity/Identity.Api/Identity.Api.csproj"
COPY "Services/Identity/Identity.GrpcContracts/Identity.GrpcContracts.csproj" "Services/Identity/Identity.GrpcContracts/Identity.GrpcContracts.csproj"

COPY "ApiGateways/MainGateway/MainGateway.csproj" "ApiGateways/MainGateway/MainGateway.csproj"
COPY "ApiGateways/WebSpa.Aggregator/WebSpa.Aggregator.csproj" "ApiGateways/WebSpa.Aggregator/WebSpa.Aggregator.csproj"

COPY "docker-compose.dcproj" "docker-compose.dcproj"

RUN dotnet restore "Kwetter.sln"

COPY . .
WORKDIR /src/ApiGateways/WebSpa.Aggregator
RUN dotnet build -c Release -o /app/build

FROM build AS publish
RUN dotnet publish -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Kwetter.ApiGateways.WebSpa.Aggregator.dll"] 


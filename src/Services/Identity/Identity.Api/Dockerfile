FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
ENV ASPNETCORE_URLS=http://+:80
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src

# Keeping the the first lines the same for every Dockerfile takes advantage of Dockers caching mechanism
COPY "Kwetter.sln" "Kwetter.sln"

COPY "BuildingBlocks/Core/Core.Api/Core.Api.csproj" "BuildingBlocks/Core/Core.Api/Core.Api.csproj"
COPY "BuildingBlocks/Core/Core.Application/Core.Application.csproj" "BuildingBlocks/Core/Core.Application/Core.Application.csproj"
COPY "BuildingBlocks/Core/Core.Domain/Core.Domain.csproj" "BuildingBlocks/Core/Core.Domain/Core.Domain.csproj"
COPY "BuildingBlocks/Core/Core.Infrastructure/Core.Infrastructure.csproj" "BuildingBlocks/Core/Core.Infrastructure/Core.Infrastructure.csproj"

COPY "Services/Tweet/Tweet.Api/Tweet.Api.csproj" "Services/Tweet/Tweet.Api/Tweet.Api.csproj"
COPY "Services/Tweet/Tweet.Application/Tweet.Application.csproj" "Services/Tweet/Tweet.Application/Tweet.Application.csproj"
COPY "Services/Tweet/Tweet.Domain/Tweet.Domain.csproj" "Services/Tweet/Tweet.Domain/Tweet.Domain.csproj"
COPY "Services/Tweet/Tweet.Infrastructure/Tweet.Infrastructure.csproj" "Services/Tweet/Tweet.Infrastructure/Tweet.Infrastructure.csproj"

COPY "Services/Identity/Identity.Api/Identity.Api.csproj" "Services/Identity/Identity.Api/Identity.Api.csproj"

COPY "docker-compose.dcproj" "docker-compose.dcproj"

RUN dotnet restore "Kwetter.sln"

COPY . .
WORKDIR /src/Services/Identity/Identity.Api
RUN dotnet build -c Release -o /app/build

FROM build AS publish
RUN dotnet publish -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Kwetter.Services.Identity.Api.dll"] 


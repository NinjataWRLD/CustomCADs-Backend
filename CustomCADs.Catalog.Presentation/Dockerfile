# !! HAVEN'T RUN THIS YET. Will configure properly one day, but not today :) !!

# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081


# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["CustomCADs.Catalog.Presentation/CustomCADs.Catalog.Presentation.csproj", "CustomCADs.Catalog.Presentation/"]
RUN dotnet restore "./CustomCADs.Catalog.Presentation/CustomCADs.Catalog.Presentation.csproj"
COPY . .
WORKDIR "/src/CustomCADs.Catalog.Presentation"
RUN dotnet build "./CustomCADs.Catalog.Presentation.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./CustomCADs.Catalog.Presentation.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CustomCADs.Catalog.Presentation.dll"]
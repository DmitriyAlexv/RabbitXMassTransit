FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 5237
EXPOSE 7123

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Debug
WORKDIR /src
COPY ["WebAPI.Consumer/WebAPI.Consumer.csproj", "WebAPI.Consumer/"]
RUN dotnet restore "WebAPI.Consumer/WebAPI.Consumer.csproj"
COPY . .
WORKDIR "/src/WebAPI.Consumer"
RUN dotnet build "WebAPI.Consumer.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Debug
RUN dotnet publish "WebAPI.Consumer.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WebAPI.Consumer.dll"]

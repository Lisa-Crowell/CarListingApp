FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["CarListingApp.Api/CarListingApp.Api.csproj", "CarListingApp.Api/"]
RUN dotnet restore "CarListingApp.Api/CarListingApp.Api.csproj"
COPY . .
WORKDIR "/src/CarListingApp.Api"
RUN dotnet build "CarListingApp.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CarListingApp.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CarListingApp.Api.dll"]

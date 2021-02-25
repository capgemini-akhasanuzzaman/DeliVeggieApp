#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY ["Gateway/ProductService/ProductService.csproj", "Gateway/ProductService/"]
COPY ["Gateway/Application/Application.csproj", "Gateway/Application/"]
COPY ["Gateway/Domain/Domain.csproj", "Gateway/Domain/"]
COPY ["Gateway/DeliVeggieSharedLibrary/DeliVeggieSharedLibrary.csproj", "Gateway/DeliVeggieSharedLibrary/"]
COPY ["Gateway/Infrastructure/Infrastructure.csproj", "Gateway/Infrastructure/"]
RUN dotnet restore "Gateway/ProductService/ProductService.csproj"
COPY . .
WORKDIR "/src/Gateway/ProductService"
RUN dotnet build "ProductService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ProductService.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ProductService.dll"]
#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
#ENV ASPNETCORE_URLS=https://+:5006;http://+:5005
WORKDIR /app
EXPOSE 80
EXPOSE 433

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY ["src/Apps/Kasir.Api/Kasir.Api.csproj", "src/Apps/Kasir.Api/"]
COPY ["src/Common/Kasir.Infrastructure/Kasir.Infrastructure.csproj", "src/Common/Kasir.Api.Infrastructure/"]
COPY ["src/Common/Kasir.Application/Kasir.Application.csproj", "src/Common/Kasir.Api.Application/"]
COPY ["src/Common/Kasir.Domain/Kasir.Domain.csproj", "src/Common/Kasir.Api.Domain/"]
RUN dotnet restore "src/Apps/Kasir.Api/Kasir.Api.csproj"
COPY . .
WORKDIR "/src/src/Apps/Kasir.Api"
RUN dotnet build "Kasir.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Kasir.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Kasir.Api.dll"]
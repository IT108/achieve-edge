#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80

ENV ASPNETCORE_URLS=http://*:80
FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
COPY . /src/achieve-edge/
#WORKDIR /src
#COPY ["achieve-edge.csproj", "achieve-edge/"]
#COPY . achieve-edge
WORKDIR "/src/achieve-edge"
RUN dotnet restore "achieve-edge.csproj"

RUN dotnet build "achieve-edge.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "achieve-edge.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "achieve-edge.dll"]
FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY ["AZEApi/AZEApi.csproj", "AZEApi/"]
RUN dotnet restore "AZEApi/AZEApi.csproj"
COPY . .
WORKDIR "/src/AZEApi"
RUN dotnet build "AZEApi.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "AZEApi.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "AZEApi.dll"]
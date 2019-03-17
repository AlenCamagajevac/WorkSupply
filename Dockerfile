FROM microsoft/dotnet:sdk AS base
WORKDIR /app
ENV ASPNETCORE_ENVIRONMENT=Production
ENV ASPNETCORE_URLS http://*:5000
EXPOSE 5000

FROM microsoft/dotnet:sdk AS builder
ARG Configuration=Release
WORKDIR /src
COPY *.sln ./
COPY WorkSupply.API/WorkSupply.API.csproj WorkSupply.API/
COPY WorkSupply.Core/WorkSupply.Core.csproj WorkSupply.Core/
COPY WorkSupply.Persistence.SQL/WorkSupply.Persistence.SQL.csproj WorkSupply.Persistence.SQL/
COPY WorkSupply.Services/WorkSupply.Services.csproj WorkSupply.Services/
RUN dotnet restore
COPY . .
WORKDIR /src/WorkSupply.API
RUN dotnet build -c $Configuration -o /app

FROM builder AS publish
ARG Configuration=Release
RUN dotnet publish -c $Configuration -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "WorkSupply.API.dll"]
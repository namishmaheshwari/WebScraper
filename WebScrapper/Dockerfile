FROM microsoft/dotnet:2.1-runtime AS base
WORKDIR /app

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY WebScrapper/WebScrapper.csproj WebScrapper/
RUN dotnet restore WebScrapper/WebScrapper.csproj
COPY . .
WORKDIR /src/WebScrapper
RUN dotnet build WebScrapper.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish WebScrapper.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "WebScrapper.dll"]

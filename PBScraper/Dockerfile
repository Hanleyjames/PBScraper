FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 56654
EXPOSE 44313

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY ["PBScraper/PBScraper.csproj", "PBScraper/"]
RUN dotnet restore "PBScraper/PBScraper.csproj"
COPY . .
WORKDIR "/src/PBScraper"
RUN dotnet build "PBScraper.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "PBScraper.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "PBScraper.dll"]
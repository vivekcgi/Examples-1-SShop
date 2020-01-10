FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY SShop/SShop.csproj SShop/
RUN dotnet restore SShop/SShop.csproj
COPY . .
WORKDIR /src/SShop
RUN dotnet build SShop.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish SShop.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "SShop.dll"]

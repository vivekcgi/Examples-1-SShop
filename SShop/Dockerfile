FROM mcr.microsoft.com/dotnet/core/sdk:2.2-alpine AS build
WORKDIR /src
COPY ["SShop/SShop.csproj", "SShop/"]
RUN dotnet restore "SShop/SShop.csproj"
COPY . .
RUN dotnet publish "SShop/SShop.csproj" -c Release -o /app

FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-alpine AS runtime
WORKDIR /app
COPY --from=build /app .
ENTRYPOINT ["dotnet", "SShop.dll"]

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build

WORKDIR /app

COPY src/Domain/ ./Domain/
COPY ./ ./ProductAPI/

WORKDIR /app/ProductAPI
RUN dotnet restore

RUN dotnet publish -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:9.0

WORKDIR /app

COPY --from=build /app/publish .

EXPOSE 80

ENTRYPOINT ["dotnet", "ProductAPI.dll"]

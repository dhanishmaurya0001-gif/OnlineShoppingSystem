FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY OnlineShoppingSystem/OnlineShoppingSystem.csproj OnlineShoppingSystem/
RUN dotnet restore OnlineShoppingSystem/OnlineShoppingSystem.csproj
COPY . .
RUN dotnet publish OnlineShoppingSystem/OnlineShoppingSystem.csproj -c Release -o /out --no-restore /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /out .
ENV ASPNETCORE_URLS=http://0.0.0.0:10000 \
    ASPNETCORE_ENVIRONMENT=Production \
    DEMO_MODE=true
EXPOSE 10000
USER app
ENTRYPOINT ["dotnet", "OnlineShoppingSystem.dll"]

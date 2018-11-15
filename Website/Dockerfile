FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY ContosoMaintenance.Web/ContosoMaintenance.Web.csproj ContosoMaintenance.Web/
RUN dotnet restore ContosoMaintenance.Web/ContosoMaintenance.Web.csproj
COPY . .
WORKDIR /src/ContosoMaintenance.Web
RUN dotnet build ContosoMaintenance.Web.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish ContosoMaintenance.Web.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "ContosoMaintenance.Web.dll"]

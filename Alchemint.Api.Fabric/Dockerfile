FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY ["Alchemint.Api.Fabric/Alchemint.Api.Fabric.csproj", "Alchemint.Api.Fabric/"]
RUN dotnet restore "Alchemint.Api.Fabric/Alchemint.Api.Fabric.csproj"
COPY . .
WORKDIR "/src/Alchemint.Api.Fabric"
RUN dotnet build "Alchemint.Api.Fabric.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "Alchemint.Api.Fabric.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Alchemint.Api.Fabric.dll"]
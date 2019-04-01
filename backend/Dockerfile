FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY ["backend", "backend/"]
# COPY ./backend .
RUN dotnet restore
# ?
COPY . .
WORKDIR "/src/backend"
RUN dotnet build -c Release -o /app

FROM build AS publish
RUN dotnet publish "Lingva.WebAPI/Lingva.WebAPI.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .

ENTRYPOINT ["dotnet", "Lingva.WebAPI.dll"]
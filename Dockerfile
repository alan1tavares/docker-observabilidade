FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build-env
WORKDIR /app

COPY ./*.csproj ./WebAplication/
RUN dotnet restore ./WebAplication/

COPY . ./WebAplication/
WORKDIR /app/WebAplication
RUN dotnet publish -c Release -o out
 
FROM mcr.microsoft.com/dotnet/aspnet:5.0
WORKDIR /app

COPY --from=build-env /app/WebAplication/out .

ENTRYPOINT [ "dotnet", "docker-observabilidade.dll" ]
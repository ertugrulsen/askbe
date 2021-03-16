FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build-env
WORKDIR /app

COPY NuGet.config ./
COPY AskDefinex/*.csproj ./
RUN dotnet restore


COPY AskDefinex/. ./
RUN dotnet publish -c Release -o out


FROM mcr.microsoft.com/dotnet/core/aspnet:3.1
WORKDIR /app
COPY --from=build-env /app/out .
EXPOSE 5000
ENTRYPOINT ["dotnet", "AskDefinex.dll"]

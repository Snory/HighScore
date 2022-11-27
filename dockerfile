FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
ENV ASPNETCORE_URLS=https://+:80/ 
EXPOSE 80

# copy .sln and .csproj 
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY *.sln .
COPY HighScore.API/HighScore.API.csproj /src/HighScore.API/
COPY HighScore.Data/HighScore.Data.csproj /src/HighScore.Data/
COPY HighScore.Domain/HighScore.Domain.csproj /src/HighScore.Domain/
# restore every dependency as well as project specific tools
RUN dotnet restore
# copy everything else
COPY . .

# build it - create binaries files
RUN dotnet build -c Release -o /app/build

# publish - pack it to folder
FROM build AS publish
RUN dotnet publish -c Release -o /app/publish

# build image
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "HighScore.API.dll"]
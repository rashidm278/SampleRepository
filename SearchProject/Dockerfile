FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
ENV ASPNETCORE_URLS=http://+:80
EXPOSE 80

COPY SearchProject.Api.sln .
COPY SearchProject.Api/SearchProject.Api.csproj SearchProject.Api/
COPY SearchProject.Entities/SearchProject.Entities.csproj SearchProject.Entities/
COPY SearchProject.Interfaces/SearchProject.Interfaces.csproj SearchProject.Interfaces/
COPY SearchProject.Repository/SearchProject.Repository.csproj SearchProject.Repository/
COPY SearchProject.Api.Tests/SearchProject.Api.Tests.csproj SearchProject.Api.Tests/
COPY SearchProject.Repository.Tests/SearchProject.Repository.Tests.csproj SearchProject.Repository.Tests/
RUN dotnet restore

COPY . .
WORKDIR /src/SearchProject.Api
RUN dotnet publish -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/publish .
EXPOSE 80
ENTRYPOINT ["dotnet", "SearchProject.Api.dll"]
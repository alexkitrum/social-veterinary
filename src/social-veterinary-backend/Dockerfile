# build image
FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build-env
COPY . .
RUN dotnet restore "SocialVeterinary/SocialVeterinary.sln"

# TODO: run unit tests and collect test coverage

# publish backend app
FROM build-env AS publish
RUN dotnet publish "SocialVeterinary/SocialVeterinary.Api.csproj" -c Release -o /app

# build runtime image with published content
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1
WORKDIR /app
EXPOSE 80
EXPOSE 443
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "SocialVeterinary.Api.dll"]

# Base image for the .NET application
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081



# Build the application
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["DbsUsersManagementService.csproj", "./"]
RUN dotnet restore "./DbsUsersManagementService.csproj"
COPY . .

WORKDIR "/src"
RUN dotnet build "./DbsUsersManagementService.csproj" -c Release -o /app/build


# Publish the application
FROM build AS publish
RUN dotnet publish "DbsUsersManagementService.csproj" -c Release -o /app/publish

# Final runtime image
FROM base AS final
WORKDIR /app
RUN sed -i "s|DEFAULT@SECLEVEL=2|DEFAULT@SECLEVEL=1|g" /etc/ssl/openssl.cnf
ENV ASPNETCORE_URLS=http://+:80
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DbsUsersManagementService.dll"]

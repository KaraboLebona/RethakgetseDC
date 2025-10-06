# Build stage
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src

# Copy the csproj and restore as distinct layers
COPY DayCareProject/*.csproj DayCareProject/
WORKDIR /src/DayCareProject
RUN dotnet restore

# Copy the rest of the code
COPY . .
RUN dotnet publish -c Release -o /app/out

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:7.0
WORKDIR /app
COPY --from=build /app/out .
ENTRYPOINT ["dotnet", "DayCareProject.dll"]

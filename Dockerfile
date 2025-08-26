# Use the official .NET SDK 8.0 to build the app
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy csproj and restore dependencies
COPY RecipeApp.csproj .
RUN dotnet restore RecipeApp.csproj

# Copy the rest of the source code
COPY . .

# Build and publish app to /app/out
RUN dotnet publish RecipeApp.csproj -c Release -o /app/out

# Use the ASP.NET runtime image for production
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/out .

# Expose port 8080 (Render expects apps to listen on 0.0.0.0:8080)
ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080

ENTRYPOINT ["dotnet", "RecipeApp.dll"]

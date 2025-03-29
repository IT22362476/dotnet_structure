# See https://aka.ms/customizecontainer to learn how to customize your debug container 
# and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# Base stage used when running from Visual Studio in fast mode (default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8009
#EXPOSE 8001 (uncomment if needed)

# Set environment variables for ASP.NET Core and connection strings
ENV ASPNETCORE_URLS=http://+:8009
ENV CONNECTIONSTRINGS__DEFAULTCONNECTION="Server=coredb;Database=INVDB;User Id=sa;Password=Sqlserver2019;MultipleActiveResultSets=true;TrustServerCertificate=True;"
ENV CONNECTIONSTRINGS__DEFAULTTOCKENCONNECTION="Server=coredb;Database=CoreDB;User Id=sa;Password=Sqlserver2019;MultipleActiveResultSets=true;TrustServerCertificate=True;"
ENV ASPNETCORE_ENVIRONMENT=Development
ENV ASPNETCORE_HTTP_PORTS=8009
ENV TZ=Asia/Colombo
# Stage for building the service project
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Copy .csproj files and restore dependencies
COPY ["Services/Inv/Inv.WebAPI/Inv.WebAPI.csproj", "Services/Inv/Inv.WebAPI/"]
COPY ["Services/Inv/Inv.Application/Inv.Application.csproj", "Services/Inv/Inv.Application/"]
COPY ["Services/Inv/Inv.Domain/Inv.Domain.csproj", "Services/Inv/Inv.Domain/"]
COPY ["Services/Inv/Inv.Infrastructure/Inv.Infrastructure.csproj", "Services/Inv/Inv.Infrastructure/"]
COPY ["Services/Inv/Inv.Persistence/Inv.Persistence.csproj", "Services/Inv/Inv.Persistence/"]
COPY ["Services/CommonLibrary/JwtTokenAuthentication.csproj", "Services/CommonLibrary/"]
# Restore project dependencies
RUN dotnet restore "./Services/Inv/Inv.WebAPI/Inv.WebAPI.csproj"

# Copy the rest of the files and build the project
COPY . .
WORKDIR "/src/Services/Inv/Inv.WebAPI"
RUN dotnet build "./Inv.WebAPI.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Stage for publishing the service project
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Inv.WebAPI.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Final stage for running the service in production or regular mode (non-Debug)
FROM base AS final

# Ensure environment variables are set in the final stage
ENV ASPNETCORE_URLS=http://+:8009
ENV CONNECTIONSTRINGS__DEFAULTCONNECTION="Server=coredb;Database=INVDB;User Id=sa;Password=Sqlserver2019;MultipleActiveResultSets=true;TrustServerCertificate=True;"
ENV CONNECTIONSTRINGS__DEFAULTTOCKENCONNECTION="Server=coredb;Database=CoreDB;User Id=sa;Password=Sqlserver2019;MultipleActiveResultSets=true;TrustServerCertificate=True;"
ENV ASPNETCORE_ENVIRONMENT=Development
ENV ASPNETCORE_HTTP_PORTS=8009
ENV TZ=Asia/Colombo
# Copy published output to the final stage
COPY --from=publish /app/publish .

# Set the entry point to the WebAPI DLL
ENTRYPOINT ["dotnet", "Inv.WebAPI.dll"]

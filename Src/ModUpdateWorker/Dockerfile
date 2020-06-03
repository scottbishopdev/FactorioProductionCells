# NOTE: This dockerfile is designed to be run in the context of the root directory of this application.

# Define the builder container, copy the source, restore packages, and build the project.
FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS mod-update-worker-build
WORKDIR /app

WORKDIR /app/Application
COPY ./Application/Application.csproj ./
RUN dotnet restore

WORKDIR /app/Infrastructure
COPY ./Infrastructure/Infrastructure.csproj ./
RUN dotnet restore

WORKDIR /app/ModUpdateWorker
COPY ./ModUpdateWorker/ModUpdateWorker.csproj ./
RUN dotnet restore

WORKDIR /app
COPY ./Application/. ./Application/
COPY ./Infrastructure/. ./Infrastructure/
COPY ./ModUpdateWorker/. ./ModUpdateWorker/
RUN dotnet publish ./ModUpdateWorker/ModUpdateWorker.csproj -c Release -o ./ModUpdateWorker/out/

# Create the runner container and copy over the entrypoint.
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS mod-update-worker
WORKDIR /app
COPY --from=mod-update-worker-build ./app/ModUpdateWorker/out/. ./

ENTRYPOINT ["dotnet", "ModUpdateWorker.dll"]

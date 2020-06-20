# NOTE: This dockerfile is designed to be run in the context of the Src directory of this application.

# Define the builder container, copy the source, restore packages, and build the project.
FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS mod-update-scheduler-build
WORKDIR /app

WORKDIR /app/Domain
COPY ./Domain/Domain.csproj ./
RUN dotnet restore

WORKDIR /app/Application
COPY ./Application/Application.csproj ./
RUN dotnet restore

WORKDIR /app/Infrastructure
COPY ./Infrastructure/Infrastructure.csproj ./
RUN dotnet restore

WORKDIR /app/ModUpdateScheduler
COPY ./ModUpdateScheduler/ModUpdateScheduler.csproj ./
RUN dotnet restore

WORKDIR /app
COPY ./Domain/. ./Domain/
COPY ./Application/. ./Application/
COPY ./Infrastructure/. ./Infrastructure/
COPY ./ModUpdateScheduler/. ./ModUpdateScheduler/
RUN dotnet publish ./ModUpdateScheduler/ModUpdateScheduler.csproj -c Release -o ./ModUpdateScheduler/out/

# Create the runner container and copy over the entrypoint.
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS mod-update-scheduler
WORKDIR /app
COPY --from=mod-update-scheduler-build ./app/ModUpdateScheduler/out/. ./

ENTRYPOINT ["dotnet", "ModUpdateScheduler.dll"]

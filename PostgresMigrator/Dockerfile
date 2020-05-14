# This will create the build container for FluentMigrator. It will create a .dll that can be copied into the runner container.
FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS fluentmigrator-build-env
WORKDIR /app

# Copy over the .csproj file and retreive the NuGet packages referenced within it.
COPY . ./
RUN dotnet publish -c Release -o output

# This will create the container that actually runs FluentMigrator against Postgres
# TODO: It seems that FluentMigrator specifically requires version 2.1 of .NET Core, which is off. Shouldn't the latest be compatible?
FROM mcr.microsoft.com/dotnet/core/sdk:2.1 AS postgres-migrator
WORKDIR /app
COPY --from=fluentmigrator-build-env /app/output .

# We need to set a PATH variable so the container knows where to find the fm executable
ENV PATH="/root/.dotnet/tools:${PATH}"
ENTRYPOINT ["dotnet", "PostgresMigrator.dll"]

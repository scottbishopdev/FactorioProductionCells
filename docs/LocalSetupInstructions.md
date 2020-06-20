**NOTE:** This document is a work in progress, and may not be accurate yet.

# Running a Local Instance of the Website
Getting a local instance of the website up and running is quick and easy! This document should contain everything you need to get a local environment up and running.

## Prerequisites
You'll need both [Git](https://git-scm.com/) and [Docker](https://www.docker.com/) installed on your local machine.

## Getting the Repository
To get started, you'll need the repository cloned locally. Navigate to the parent directory where you'd like the app to run from, then open a command line and execute the following:
```bash
# Clone the repository down
$ git clone https://github.com/sbishop411/FactorioProductionCells

# Go into the repository
$ cd FactorioProductionCells
```

## Configuring the .env File
For purposes of security and customization, several critical values must be defined in a `.env` file in the root of the repository. You'll need to configure each of these environment variables before running the website. Here's what each of them do:
* **`POSTGRES_DB` -** The name of the database Postgres database that will be created. This can be anything you like.
* **`POSTGRES_USER` -** The name of the admin user for the Postgres database. Default is "postgres".
* **`POSTGRES_PASSWORD` -** The password for the Postgres admin user account. This can be anything you like, but please use a strong password.
* **`POSTGRES_PORT` -** The port that Postgres will listen on for connections. Default is "5432".
* **`PGADMIN_DEFAULT_EMAIL` -** The email address of the default user for pgAdmin. This can be your email address.
* **`PGADMIN_DEFAULT_PASSWORD` -** The password for the default user for pgAdmin. Again, use a strong password.
* **`PGADMIN_PORT` -** The port that pgAdmin will listen on. This can be any valid port that's unused on the host.
* **`RABBITMQ_DEFAULT_USER` -** The name of the default user for RabbitMQ.
* **`RABBITMQ_DEFAULT_PASS` -** The password for the RabbitMQ default user account. Please use a strong password.
* **`RABBITMQ_PORT` -** The port that RabbitMQ will listen on.
* **`RABBITMQ_MANAGEMENT_PORT` -** The port that the RabbitMQ management interface will listen on.

The project also currently uses environment variables to define information about default system users. There are current 3 default users that are required, and each user has 4 configurable properties. The three required users are:
* **Administrator -** This mandatory user account is intended to be the user account for the owner of the system.
* **ModUpdateScheduler -** This mandatory user account will be used by the ModUpdateScheduler service when interacting with the database.
* **ModUpdateWorker -** This mandatory user account will be used by the ModUpdateWorker service when interacting with the database.

The four configurable properties for each user are in the format `<USER>_<PROPERTY>` (e.g. `ADMINISTRATOR_USER` for the Administrator user's email address). The four configurable properties for each user are:

* **`<USER>_USERNAME` -** The username to be used for the user account.
* **`<USER>_EMAIL` -** The email address that will be associated with the user account.
* **`<USER>_PASSWORD` -** The password that will be used to access the user account. Please be sure to use strong passwords.
* **`<USER>_ID` -** A guid value that will act as the unique identifier and primary key for the user account. Ideally, this class of environment variable will be eliminated in the future, but is currently required due to issues with circular dependencies when seeding the database with the default users.

The repository comes with a scaffolded `.env` file, but please note that default values for many critical environment variables will not be provided in order to ensure a proper (and safe) configuration of this system.

## Configuring servers.json for pgAdmin
pgAdmin uses a file called `servers.json` to define which databases it should connect to when it starts up. If you'd like pgAdmin to connect to postgres automatically instead of doing so manually, you'll need to create your own `servers.json` file at `./config/pgadmin/server.json`. You can read more about the format for the file [here](https://www.pgadmin.org/docs/pgadmin4/development/import_export_servers.html).

Here's an example of what your `servers.json` file might look like. Keep in mind that some of these values should match values that you set in the `.env` file.

``` json
{
    "Servers": {
        "1": {
            "Name": <POSTGRES_DB from .env file>,
            "Group": "Servers",
            "Port": <POSTGRES_PORT from .env file>,
            "Username": <POSTGRES_USER from .env file>,
            "Host": "postgres",
            "SSLMode": "prefer",
            "MaintenanceDB": "postgres"
        }
    }
}
```
**Note:** You'll still need to enter the database's password manually on connection, but the server will appear in pgAdmin's server list.


## Starting Things Up
After configuring the .env file, you should be good to go! Go back to your command line (in the root of the repository) and run the following commands:
``` bash
# Build the Docker images locally
$ docker-compose build

# Start the Docker services
$ docker-compose up -d
```

That's it! You should now be able to access the website by opening a browser and navigating to `localhost:<WEBSITE_PORT>`, and pgAdmin by navigating to `localhost:<PGADMIN_PORT>`.
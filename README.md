

# Clean Architecture 

An opinionated **ASP.NET Core** solution setup for creating web applications using **Clean Architecture** and **Domain-Driven Design** principles.

The setup follows important modern development principles such as high test coverage, SOLID principles, containerisation, code-first database management, enforced code styles, API tests, architecture tests and automated acceptance testing.

The application extends the typical *Weather Forecast* example provided in default .NET project templates and contains the following components:

- **API** - ASP.NET 7 REST API with Swagger support
- **Angular SPA** - Angular SPA hosted using ASP.NET 7
- **Database** - SQL Server/PostgreSQL database integration via Entity Framework Core
- **Migrations** - Code-First database migrations managed using a console application

## Table of Contents

- [Clean Architecture](#Clean-Architecture)
  - [Table of Contents](#Table-of-Contents)
  - [Quick Start](#Quick-Start)
  - [Install Template](#Install-Template)
- [Developer Guide](#Developer-Guide)
  - [IDE](#ide)
  - [Solution Project Structure](#Solution-Project-Structure)
  - [Nuget Libraries](#Nuget-Libraries)
  - [Angular](#angular)
  - [Entity Framework Core](#Entity-Framework-Core)
  - [Docker](#docker)
  - [Kubernetes](#kubernetes)
- [Architecture and Development Principles](#Architecture-and-Development-Principles)
  - [Clean Architecture](#Clean-Architecture)
  - [Domain-Centric Architecture](#Domain-Centric-Architecture)
  - [Domain-Driven Design](#Domain-Driven-Design)
  - [Command Query Responsibility Segregation](#Command-Query-Responsibility-Segregation)
  - [Domain Events](#Domain-Events)
  - [Data Transfer Objects and Mapping](#Data-Transfer-Objects-and-Mapping)
- [Migrations](#Migrations)
  - [Create a Database Migration](#Create-a-Database-Migration)
  - [Run Migrations](#Run-Migrations)
- [Running the Project](#Running-the-Project)
  - [Local Environment Setup](#Local-Environment-Setup)
  - [Projects to Run](#Projects-to-Run)
- [Testing](#testing)
  - [Unit Testing](#unit-testing)
  - [API Tests](#api-tests)
  - [Architecture Tests](#Architecture-Tests)
  - [Automated Acceptance Tests](#Automated-Acceptance-Tests)
  

## Quick Start

*All of the required tools and libraries must be installed using the **Developer Guide** before these steps can be run.*

1. Start local database in Docker

```bash
docker-compose --profile dev up -d
```

2. Run the **CleanArchitecture.Migrations** project to deploy database schema
3. Run the **CleanArchitecture.Api** and **CleanArchitecture.Web** projects to debug the application


## Install Template

The solution can be installed as a template to be used for creating new solutions via the .NET ClI or Visual Studio/Rider.

```bash
# Install from directory
dotnet new install .

# Uninstall from directory
dotnet new uninstall .
```

# Developer Guide

The following steps are required to get your Developer machine ready for working with this project. 

This project uses NET 7.0 and Angular. Everything you need to get started should be included below - if there are any gaps, please do add them in.

## IDE

### Visual Studio 2022

Visual Studio 2022 is recommending for opening the main solution for this project - `CleanArchitecture.sln`.

### Visual Studio Code

VS Code is recommended for working on the Angular SPA application due to the additional extensions that can be used.

## Solution Project Structure

The solution is broken down into the following projects:

- **CleanArchitecture.Api** - ASP.NET 7 Web API with Swagger support
- **CleanArchitecture.Application** - Application layer containing Commands/Queries/Domain Event Handlers
- **CleanArchitecture.Core** - Domain layer containing Entities and Domain Events
- **CleanArchitecture.Infrastructure** - Infrastructure layer for all external integration e.g. database, notifications, serialization
- **CleanArchitecture.Web** - Angular SPA hosted using ASP.NET 7
- **CleanArchitecture.Hosting** - Hosting cross-cutting concerns e.g. configuration and logging
- **CleanArchitecture.Migrations** - Code-First EF database migrations and migration runner

### Test Projects

Each source project has a relevant test project for Unit/API tests. The exception to this is the following:

- **CleanArchitecture.AcceptanceTests** - Automated BDD Acceptance Tests using .NET Playwright and SpecFlow

## Nuget Libraries

The following Nuget libraries are used across the solution:

- **Entity Framework Core (EF Core)** - ORM for interacting with Database. *SQL Server* provider used by default however EF makes it easy to swap out for a different database.
- **Autofac** - Used for dependency injection and splitting service registration into 'Modules'
- **Swagger/Swashbuckle** - UI for interacting with API
- **AutoMapper** - Used to map from Entities in *Core* to DTOs in *Application*
- **MediatR** - Mediator implementation for implementing *Domain Event Handlers* and *CQRS*
- **CSharpFunctionalExtensions** - Base Class implementation for *Entities* and *ValueObjects*
- **XUnit** - Test runner for Unit and API tests
- **FluentAssertions** - Fluent extension methods for running assertions in tests
- **Playwright** - UI automation library used for *Acceptance Tests*
- **Moq** - Test Doubles library for creating *Mocks* and *Stubs* in tests
- **NetArchTest** - Architecture testing library

## Angular

Angular is used for the SPA website for this project. The code for the Angular project can be found in `src/CleanArchitecture/ClientApp`.

### Node

Node.js is required to build and run the Angular SPA.

### Hosting and Running
The Angular project is hosted within an ASP.NET application. This makes deployment and configuration much easier. 
The Web project uses the **Microsoft.AspNetCore.SpaProxy** Nuget package to handle running the relevant Angular CLI commands when the project is debugged.

HTTP requests are proxied by the Web project during local development. This allows traffic with both the SPA and the API to go through the same URL so that there are no CORS issues - this is similar to how the applications will work when deployed.
The proxy configuration can be found in the `proxy.conf.js` file in the Angular project.


## Entity Framework Core

Entity Framework Core (EF) is used as the database ORM for this proect.

Install the Entity Framework CLI
```bash
dotnet tool install --global dotnet-ef
```

## Docker

Docker is used to build and deploy the code for this project. It is also used to spin-up services for local development such as the database.

### Docker Desktop

Docker Desktop is recommended for running Docker if you are using a Windows development machine.

### Docker Compose

Docker Compose is used to orchestrate running Docker images for local development. This is included as part of Docker Desktop.

## Kubernetes

Kubernetes manifests have been created for deploying the application to Kubernetes if required.

### Helm

Helm can be used as a package manager to deploy the applications in this project to Kubernetes.

The Helm **charts** for this project can be found in the `charts` folder.

# Architecture and Development Principles

The following principles are used in the code and architecture for this application.

## Clean Architecture

Clean Architecture is used to split the projects used into the following layers:

- Presentation - Web and API projects
- Application - Application services
- Core - Domain logic and Entities
- Infrastucture - Services for working with external systems e.g. database, event bus

Clean architecture puts the business logic and application model at the center of the app. Instead of having business logic depend on data access or other infrastructure concerns, this dependency is inverted: infrastructure and implementation details depend on the Application Core. This is achieved by defining abstractions, or interfaces, in the Core and Application layers, which are then implemented by types defined in the Infrastructure layer. A common way of visualizing this architecture is to use a series of concentric circles, similar to an onion.

## Domain-Centric Architecture

Domain Centric Architecture is used to split the code in the Core and Application layer by use-cases e.g. Locations, Weather.

The code within each use-case folder is split by object type e.g. Entities, Services.

Domain Centric Architecture makes it easier to navigate between code when working on a particular concept. It also means that code is already separated for if a concept needs to be moved in the future such as to its own independent Microservice.

## Domain-Driven Design

Domain-Driven Design (DDD) is used to model the Core (Domain) layer of the app. The main data structures (Aggregate Roots) for the application are fully encapsulated by using private setters on any properties. By using this principle all Domain logic is encapsulated and controlled from within the Aggregate Root.

Data can only be loaded and saved using an Aggregate Root - therefore the IRepository interface has a generic contstraint for only working with the AggregateRoot base class.

Aggregates can work in coordination by emitting Domain Events which are subscribed to by Domain Event Handlers in a different Bounded Context. MediatR is used to dispatch the Domain Events to their associated Domain Event Handlers.

A full guide to the DDD techniques used in this solution can be found here:
https://betterprogramming.pub/domain-driven-design-a-walkthrough-of-building-an-aggregate-c84113aa9975

## Command Query Responsibility Segregation

CQRS is used within the Application layer to mediate data between the database and the API. The code is split into:

### Queries
Queries are used to retrieve data and should never mutate any state at all. Queries implement the base **Query** class.

Each Query class has an associated **QueryHandler**. 

### Commands
Commands implement the base **Command** or **CreateCommand** class and can be used to mutate state e.g. Create, Update, Delete.

CreateCommands should be used when creating data as they allow the Id of the created object to be returned.

Each Command class has an associated **CommandHandler** or **CreateCommandHandler** class. 

### MediatR

The MediatR Nuget package is used for the mediator implementation to match Queries/Commands/Domain Events to the associated handlers. The handlers are dynamically registered into the IoC container automatically.

Handlers are kept in the same file as their Query/Command. This makes it easier to navigate to them when debugging code.

## Domain Events

Domain Events are matched to their relevant Domain Event Handlers using MediatR. Domain Events are published via Aggregate Roots and dispatched directly before changes are saved to the database.

A full guide to the dispatching technique can be found here:
https://betterprogramming.pub/domain-driven-design-domain-events-and-integration-events-in-net-5a2a58884aaa

## Data Transfer Objects and Mapping

The API returns Data Transfer Object (DTO) classes. These are dumb POCO classes with public getters and setters - this is important to allow them to be serialized easily.

The Application layer maps **Entities** from the database into **DTOs** using the **Automapper** Nuget package. Mappings are defined in the Application layer in the `MappingProfile` folders.

The separation between Entity and DTO is very important to isolate the Core/Domain layer from changes to the API. It allows for backwards compatible changes to be implemented.

# Migrations

All schema and data migrations are automated using the **CleanArchitecture.Migrations** project.

The migrations are generated from and held in the **CleanArchitecture.Migrations** project. 
Migrations are generated automatically using the EF CLI. Each time the EF CLI is used a migration class will be added to a **Migrations** folder in the project. EF will compare the entities in your EF Context class to the existing snapshot and work out what migrations need to be added.

## Create a Database Migration

A migration can be generated using the following command:

```bash
dotnet ef migrations add <migration-name>
```

Before migrations can be added the database must be running. See 'Run Local Database in Docker'.

## Run Migrations

Migrations can be run by running the **CleanArchitecture.Migrations** console app project.

# Running the Project

Before the project can be run all of the **Developer Setup** steps must be completed.

There are also some additional steps which must be run to setup your local environment.

## Local Environment Setup

The following steps must be performed to setup your local environment for running the project.

### Start Local Instances

Docker is used to start the following services locally:

- SQL Server or PostgreSQL

A **dev** profile is included in the docker-compose file to run the required local services:
```
docker-compose --profile dev up -d
```

The services can be stopped using:

```
docker-compose --profile dev down
```

This will not delete the data volumes created for the database so that the container can be started-up up again with the same data. To delete the data volume the following must be used:

```
docker-compose --profile dev down -v
```

### Setup Database

Once the local database is running in Docker the **CleanArchitecture.Migrations** project (See [Run Migrations]) must be run to instantiate the database and populate some seed data.

## Projects to Run

Before running the application your local services must be running and you must setup your database using the steps above.

The following projects must be set as startup projects to run the entire solution:
- **CleanArchitecture.Api**
- **CleanArchitecture.Web**

# Testing

Various types of tests are used in the project. Each project being tested from the `src` folder has an associated project in the `tests` folder.

## Unit Testing

Unit tests should be written to test all code added to the Core and Infrastructure layers of the solution. Code in the Core (Domain) layer should have 100% code coverage without exception as it is the most important part of the application.

The **Moq** Nuget package is used to create Mocks and Stubs for the tests.

The **FluentAssertions** Nuget package is used to run assertions for the tests.

The **Builder Pattern** is used to build test Entities for the tests. This makes the tests more flexible and easier to change when the Entities change.

## API Tests

**WebApplicationFactory** is used to spin-up the API locally for testing. This allows services from the IoC container to be swapped out and mocked.

The API tests should test the *API* and *Application* layers of the solution. Any *Infrastructure* services should be mocked and swapped out. The **MockRepositoryFactory** class can be used to mock the **IRepository** interface.

## Architecture Tests

Tests are performed on the architecture of the solution using **NetArchTest**. These tests check that the Clean Architecture layering is adhered to and that the development principles and naming conventions being used are not breached.

## Automated Acceptance Tests

Automated acceptance tests are run against a local instance of the website.

The tests are run as .NET Unit Tests using Playwright and SpecFlow.

### Playwright

Playwright is an automated UI Testing framework developed by Microsoft. If can be installed by following the instructions here: https://playwright.dev/docs/intro#installation

Code can be auto-generated from using the web application by running the following:

```powershell
PowerShell.exe -ExecutionPolicy Bypass -File .\tests\CleanArchitecture.AcceptanceTests\bin\Debug\net8.0\playwright.ps1 codegen https://localhost:44411/
```

### SpecFlow

SpecFlow is used for Business Driven Development (BDD). Specification feature files are used to execute tests.

The **SpecFlow Visual Studio extension** must be downloaded to create and use SpecFlow features files.

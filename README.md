## A1VehicleInventory-Microservice
## PROG3176 A1VehicleInventory-Microservice  Robera Robbie Alazer

This project is a Vehicle Inventory Microservice built with ASP.NET Core Web API + DDD. It supports creating vehicles, viewing vehicles, updating vehicle status, deleting vehicles, and testing datbase.

## Features
- Create vehicles
- View vehicles (all or by ID)
- update vheicle status
- Delete vehicles
- Test database connection
- Clean Arhitecture structure
- Domain driven Desgin rules

--------

## Architecture

The solution follows Clean Architecture. Each layer has one responsibility.

## Domain
- Core business entities and rules
- Vehicle entity and validation logic
- No database or web dependencies

## Application
- Use cases and services
- DTOs
- Repository interfaces
- No EF Core or controller code

## Infrastructure
- EF Core DbContext
- Repository implementations
- Database mappings

## WebAPI
- Controllers and endpoints
- Swagger setup
- Dependency injection
- App configuration

----

## Vehicle Model

## Properties
- Id
-VehicleCode
-VehicleType
- LocationId
-  Status


## Business Rules
- VehicleCode is required
- VehicleType is required
- LocationId must be greater than 0
- Cannot rent a vehicle if it is:
  - Already rented
  - Reserved
  - Under service
  - Reserved vehicles will not be available without release
  - Invalid status will throw a domain excption
 

 
## Tech Stack

- ASP.NET Core Web API
- Entity Framework Core
- SQL Server / SQL Express
- Swagger

## Setup Instructions

## Prerequisites
- Visual Studio 2022
- ASP.NET Core Web API 
- SSMS

## Steps
1. Clone the repo or unzipp from Github
2. Open the solution in Visual Studio
3. Use Database provided FROM PROG3176 Instructor	Srdj Toholj
4.Example {
  "ConnectionStrings": {
    "VehicleInventoryDb": "Server=localhost\\SQLEXPRESS;Database=VehicleInventoryDb;Trusted_Connection=True;TrustServerCertificate=True;"
  },
5. Set the WEBAPI as startup prohect and run
6. Swagger will open  https://localhost:7288/swagger/index.html

---
## Example Endpoints

- GET /api/vehicles — list all vehicles
- GET /api/vehicles/{id} — get vehicle by id
- POST /api/vehicles — create vehicle
- PUT /api/vehicles/{id}/status — update status
- DELETE /api/vehicles/{id} — delete vehicle

---
## Notes
- Domain layer contains all rules
- Controller keeps logic simple and clean
-  The Database access goes through repositories only
  




## A2
## PROG3176 A2 Robera Robbie Alazer

## What Was Added in Assignment 2

### 1. API Gateway
- Built with Reverse Proxy
- Runs on port 7050
- All MVC traffic routes through the gateway only
- API Key authentication via X-Api-Key header
- Two routes: /gateway/inventory/ to VehicleInventory.WebAPI and /gateway/maintenance/ to Maintenance.WebAPI

### 2. Global Exception Handling
- Single ExceptionHandlingMiddleware added to both WebAPIs
- DomainException → 400 Bad Request with error message
- All other exceptions → 500 Internal Server Error
- All try to catch blocks removed from controller

### 3. DDD Fixes (Inventory Service)
- Added VehicleCode and VehicleType Value Objects
- Added Domain Events (VehicleStatusChangedEvent)
- Removed duplicate validation from VehicleService
- Added a Reconstitute() method to rebuild vehicles from database data.


## How to Run
- Start all 4 projects in this order:
1. VehicleInventory.WebAPI  https://localhost:7288
2. Maintenance.WebAPI  https://localhost:7270
3. ApiGateway  https://localhost:7050
4. 8768364RobbieAlazer_MVC https://localhost:7134


## Testing the Gateway
- All requests through the gateway require this header:
- X-Api-Key
-  Api key will be found in the APIGateway appsettings.json


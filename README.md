# Warehouse Management System API
This is a .NET Web API project for a warehouse management system that includes geocoding functionality.

### Features
- Warehouse management: add and delete warehouses.
- Geocoding functionality: with the help of geocoding, the system can determine the latitude and longitude of an address, which allows it to calculate the distance between two points.
- Find the closest warehouses: manager users can access a functionality to display the three closest warehouses to a given address.

# Installation
1. Clone the repository to your local machine.
2. Open the solution file wms-api.sln in Visual Studio.
3. Build the solution to restore dependencies and compile the project.
4. Run `Update-Database` from Package Manager Console (A SQL daatabse connection string is needed. See appsettings.json).
5. Run the project locally.

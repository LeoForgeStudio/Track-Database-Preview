# 🚚 TruckDatabase API

A backend system for managing truck data using ASP.NET Core Web API and MongoDB.

---

## 🔧 Features

- CRUD operations for:
  - Trucks
  - Engines
  - Gearboxes
  - Manufacturers
  - Users
- MongoDB NoSQL data persistence
- Repository & Service layer separation
- Dependency Injection
- User password hashing with salt
- Authorization-ready with user registration and login
- Integrated Swagger UI for testing
- Unit tests and MongoDB integration tests

---

## 📆 Technologies

- ASP.NET Core 8 Web API
- MongoDB.Driver
- MSTest (Unit + Integration testing)
- Swagger (Swashbuckle.AspNetCore)

---

## 🗂️ Project Structure

```
Truck_WebApi/           → REST API controllers
Truck_BusnessLogic/     → Services and business logic
Truck_DataAccess/       → Repositories and MongoDB interaction
Truck_Shared/           → DTOs, Enums, Entities
Truck_ConsoleApp/       → Manual data population (console-based)
TruckService_Tests/     → Unit and integration tests
```

---

## ▶️ Running the Project

1. Start MongoDB locally (`mongodb://localhost:27017`)
2. Launch `Truck_ConsoleApp` project to generate base data (e.g. admin user, engine, gearbox, manufacturer)
3. Launch `Truck_WebApi` project
4. Open Swagger UI at: `https://localhost:<port>/swagger`
5. Use Swagger "Authorize" button and log in using username: `admin`, password: `admin`

---



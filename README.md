﻿# EmployeeAdminPortal

A simple **Employee Management API** built with **ASP.NET Core (.NET 6+)**, demonstrating full CRUD operations using Entity Framework Core.


## 🚀 Features

- ✅ Create Employee (`POST /api/employee`)
- 📋 Read/Get All Employees (`GET /api/employee`)
- 🔍 Get Employee by ID (`GET /api/employee/{id}`)
- ✏️ Update Employee (`PUT /api/employee/{id}`)
- ❌ Delete Employee (`DELETE /api/employee/{id}`)

---

## 💻 Tech Stack

- ASP.NET Core Web API
- C#
- Entity Framework Core
- SQL Server (via ApplicationDbContext)
- RESTful API
- DTO Pattern for data transfer
- Dependency Injection

---

## 📁 Project Structure

```bash
EmployeeAdminPortal/
│
├── Controllers/
│   └── EmployeeController.cs         # API endpoints
│
├── Data/
│   └── ApplicationDbContext.cs       # EF Core DbContext
│
├── Models/
│   ├── Entities/
│   │   └── Employee.cs               # DB model
│   └── AddEmployeeDto.cs            # DTOs for API input
│
├── Program.cs                        # App startup
└── appsettings.json                  # Configuration

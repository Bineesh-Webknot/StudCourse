# 🎓 Student Course Management - .NET Application

A basic implementation of a **Student Course Management System** built with **ASP.NET Core**, demonstrating key backend concepts such as authentication, role management, database handling, and more.

---

## 📌 Overview

This project provides a clean and extensible structure for managing students, courses, and their enrollment. It is built using modern .NET practices and includes user authentication and role-based authorization.

---

## 🛠️ Tech Stack

| Component        | Technology          |
|------------------|---------------------|
| **Backend**      | ASP.NET Core        |
| **ORM**          | Entity Framework Core |
| **Database**     | PostgreSQL          |
| **Authentication** | JWT (JSON Web Tokens) |
| **Authorization** | Role-Based using Identity |
| **Mapping**      | AutoMapper          |
| **Validation**   | Data Annotations    |

---

## 🧩 Key Features

- 🔐 **JWT Authentication and Authorization**
- 👥 **User and Role Management** using ASP.NET Core Identity
- 📦 **Entity Framework Core** for database interaction
- 🔄 **AutoMapper** for DTO <-> Entity transformations
- ✅ **Model Validation** using annotation attributes
- 🎓 **Students**, **Courses**, and **Enrollments** CRUD operations

---

## 🧰 Concepts Implemented

### 1. **PostgreSQL Integration**
- Used as the primary relational database.
- EF Core migrations to handle schema changes.

### 2. **Entity Framework Core**
- Code-first approach.
- Repository or service pattern (if applicable).
- Relationships handled via navigation properties.

### 3. **ASP.NET Core Identity**
- Out-of-the-box user and role management.
- Secure password hashing and storage.
- Role-based access control.

### 4. **AutoMapper**
- Clean separation between entities and DTOs.
- Helps in minimizing repetitive mapping logic.

### 5. **Validation Annotations**
- Input validation using attributes like `[Required]`, `[StringLength]`, `[EmailAddress]`, etc.

### 6. **JWT Authentication**
- Token generation upon login.
- Middleware to validate and authorize requests.




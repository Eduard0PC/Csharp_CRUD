# SQL Server Setup Guide

This project uses **Microsoft SQL Server** as its database engine.
To run the application correctly, you must create the required database, tables, and initial data.

---

## Requirements

* SQL Server installed (Linux or Windows)
* .NET installed
* Access to SQL Server using a user (e.g., `sa`)

---

## Database Connection

The application connects using the following configuration:

```csharp
Server=localhost;
Database=tema5_3;
User Id=sa;
Password=Eduardo_123!;
TrustServerCertificate=True;
```

> [!IMPORTANT]
> * SQL Server is running
> * The database `tema5_3` exists
> * Credentials are correct

---

## Step 1: Create Database

Run the following in SQL Server:

```sql
CREATE DATABASE tema5_3;
GO
```

---

## Step 2: Create Tables

### Materias

```sql
CREATE TABLE Materias (
    ID_Materia VARCHAR(10) PRIMARY KEY,
    Nombre_Materia VARCHAR(100) NOT NULL,
    Creditos INT
);
GO
```

### Profesores

```sql
CREATE TABLE Profesores (
    ID_Profe VARCHAR(10) PRIMARY KEY,
    Nombre VARCHAR(50) NOT NULL,
    Apellido VARCHAR(50) NOT NULL,
    Email VARCHAR(100),
    ID_Materia VARCHAR(10),
    FOREIGN KEY (ID_Materia) REFERENCES Materias(ID_Materia)
);
GO
```

---

## Step 3: Insert Initial Data

Insert the following records into `Materias`:

```sql
INSERT INTO Materias (ID_Materia, Nombre_Materia, Creditos)
VALUES
('MAT101', 'Cálculo Diferencial', 5),
('PROG202', 'Programación Orientada a Objetos', 6),
('BD303', 'Bases de Datos', 4),
('RED404', 'Redes de Computadoras', 4),
('IA505', 'Inteligencia Artificial', 6);
GO
```

---

## Step 4: Run the Application

## Notes

* The project uses `Microsoft.Data.SqlClient`
* SSL certificate validation is bypassed with:

  ```
  TrustServerCertificate=True
  ```

  This is fine for development but not recommended for production.

---

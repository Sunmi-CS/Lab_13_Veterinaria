
-- =============================================
-- PROYECTO: VETERINARIA API
-- BASE DE DATOS: VeterinariaDB
-- =============================================

-- 1. CREAR BASE DE DATOS
IF DB_ID('VeterinariaDB') IS NULL
BEGIN
    CREATE DATABASE VeterinariaDB;
END
GO

USE VeterinariaDB;
GO

-- =============================================
-- 2. TABLA: PROPIETARIOS
-- =============================================

IF OBJECT_ID('dbo.Propietarios', 'U') IS NULL
BEGIN
    CREATE TABLE Propietarios
    (
        IdPropietario INT IDENTITY(1,1) PRIMARY KEY,
        Nombres NVARCHAR(80) NOT NULL,
        Apellidos NVARCHAR(100) NOT NULL,
        Telefono NVARCHAR(15) NOT NULL,
        Correo NVARCHAR(120) NOT NULL,

        CONSTRAINT UQ_Propietarios_Correo
            UNIQUE (Correo)
    );
END
GO

-- =============================================
-- 3. TABLA: MASCOTAS
-- =============================================

IF OBJECT_ID('dbo.Mascotas', 'U') IS NULL
BEGIN
    CREATE TABLE Mascotas
    (
        IdMascota INT IDENTITY(1,1) PRIMARY KEY,
        Nombre NVARCHAR(80) NOT NULL,
        Especie NVARCHAR(50) NOT NULL,
        Raza NVARCHAR(80) NOT NULL,
        Edad INT NOT NULL,
        IdPropietario INT NOT NULL,

        CONSTRAINT CK_Mascotas_Edad
            CHECK (Edad >= 0),

        CONSTRAINT FK_Mascotas_Propietarios
            FOREIGN KEY (IdPropietario)
            REFERENCES Propietarios(IdPropietario)
    );
END
GO

-- =============================================
-- 4. INSERTAR PROPIETARIOS DE EJEMPLO
-- =============================================

IF NOT EXISTS (SELECT 1 FROM Propietarios)
BEGIN
    INSERT INTO Propietarios
        (Nombres, Apellidos, Telefono, Correo)
    VALUES
        ('Carlos', 'Ramirez Lopez', '987654321', 'carlos@gmail.com'),
        ('Maria', 'Fernandez Torres', '987123456', 'maria@gmail.com'),
        ('Jose', 'Gutierrez Perez', '986123456', 'jose@gmail.com'),
        ('Lucia', 'Castro Mendoza', '985123456', 'lucia@gmail.com'),
        ('Andrea', 'Vargas Ruiz', '984123456', 'andrea@gmail.com');
END
GO

-- =============================================
-- 5. INSERTAR MASCOTAS DE EJEMPLO
-- =============================================

IF NOT EXISTS (SELECT 1 FROM Mascotas)
BEGIN
    INSERT INTO Mascotas
        (Nombre, Especie, Raza, Edad, IdPropietario)
    VALUES
        ('Max', 'Perro', 'Golden Retriever', 3, 1),
        ('Luna', 'Gato', 'Persa', 2, 1),
        ('Rocky', 'Perro', 'Bulldog', 5, 2),
        ('Michi', 'Gato', 'Siames', 1, 2),
        ('Toby', 'Perro', 'Beagle', 4, 3),
        ('Nina', 'Conejo', 'Mini Rex', 2, 4),
        ('Coco', 'Loro', 'Amazonas', 6, 5),
        ('Pelusa', 'Gato', 'Angora', 3, 5);
END
GO

-- =============================================
-- 6. CONSULTAR LOS DATOS
-- =============================================

SELECT * FROM Propietarios;
GO

SELECT * FROM Mascotas;
GO

-- =============================================
-- 7. CONSULTAR LA RELACION ENTRE LAS TABLAS
-- =============================================

SELECT
    M.IdMascota,
    M.Nombre AS Mascota,
    M.Especie,
    M.Raza,
    M.Edad,
    P.IdPropietario,
    P.Nombres + ' ' + P.Apellidos AS Propietario,
    P.Correo
FROM Mascotas M
INNER JOIN Propietarios P
    ON M.IdPropietario = P.IdPropietario;
GO

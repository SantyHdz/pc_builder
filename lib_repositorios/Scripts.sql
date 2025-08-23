-- 1. Crear la base de datos
CREATE DATABASE pc_builder;
GO

USE pc_builder;
GO

-- 2. Tablas principales

CREATE TABLE Categorias (
                            IdCategoria INT IDENTITY(1,1) PRIMARY KEY,
                            Nombre NVARCHAR(50) NOT NULL
);

CREATE TABLE Componentes (
                             IdComponente INT IDENTITY(1,1) PRIMARY KEY,
                             Nombre NVARCHAR(100) NOT NULL,
                             Marca NVARCHAR(50),
                             Precio DECIMAL(18,2) NOT NULL,
                             IdCategoria INT NOT NULL,
                             Especificaciones NVARCHAR(300),
                             FOREIGN KEY (IdCategoria) REFERENCES Categorias(IdCategoria)
);

CREATE TABLE Roles
(
    Id     INT PRIMARY KEY IDENTITY(1,1),
    Nombre NVARCHAR(100) NOT NULL
);

CREATE TABLE Usuarios
(
    Id             INT PRIMARY KEY IDENTITY(1,1),
    Nombre         NVARCHAR(100) NOT NULL,
    Correo         NVARCHAR(150) NOT NULL UNIQUE,
    ContrasenaHash NVARCHAR(255) NOT NULL,
    Direccion      NVARCHAR(255),
    RolId          INT NOT NULL,
    FOREIGN KEY (RolId) REFERENCES Roles (Id)
);

CREATE TABLE Builds (
                        IdBuild INT IDENTITY(1,1) PRIMARY KEY,
                        Nombre NVARCHAR(100) NOT NULL,
                        IdUsuario INT NOT NULL,
                        FechaCreacion DATETIME DEFAULT GETDATE(),
                        FOREIGN KEY (IdUsuario) REFERENCES Usuarios(Id)
);

CREATE TABLE BuildComponentes (
                                  IdBuildComponente INT IDENTITY(1,1) PRIMARY KEY,
                                  IdBuild INT NOT NULL,
                                  IdComponente INT NOT NULL,
                                  FOREIGN KEY (IdBuild) REFERENCES Builds(IdBuild),
                                  FOREIGN KEY (IdComponente) REFERENCES Componentes(IdComponente)
);

CREATE TABLE Auditorias
(
    Id            INT IDENTITY(1,1) PRIMARY KEY,
    Tabla         NVARCHAR(255) NOT NULL,
    Accion        NVARCHAR(50) NOT NULL,
    LlavePrimaria NVARCHAR(255) NOT NULL,
    Cambios       NVARCHAR(MAX) NOT NULL,
    Fecha         DATETIME NOT NULL DEFAULT GETDATE(),
    Usuario       NVARCHAR(255) NULL
);
GO

-- 3. Procedimientos almacenados

-- Insertar usuario
CREATE PROCEDURE sp_InsertarUsuario
    @Nombre NVARCHAR(100),
    @Correo NVARCHAR(150),
    @ContrasenaHash NVARCHAR(255),
    @Direccion NVARCHAR(255) = NULL, -- opcional
    @RolId INT
AS
BEGIN
INSERT INTO Usuarios (Nombre, Correo, ContrasenaHash, Direccion, RolId)
VALUES (@Nombre, @Correo, @ContrasenaHash, @Direccion, @RolId);
END;
GO

-- Insertar componente
CREATE PROCEDURE sp_InsertarComponente
    @Nombre NVARCHAR(100),
    @Marca NVARCHAR(50),
    @Precio DECIMAL(18,2),
    @IdCategoria INT,
    @Especificaciones NVARCHAR(300)
AS
BEGIN
INSERT INTO Componentes (Nombre, Marca, Precio, IdCategoria, Especificaciones)
VALUES (@Nombre, @Marca, @Precio, @IdCategoria, @Especificaciones);
END;
GO

-- Filtrar componentes por categoría
CREATE PROCEDURE sp_FiltrarComponentesPorCategoria
    @IdCategoria INT
AS
BEGIN
SELECT c.IdComponente, c.Nombre, c.Marca, c.Precio, c.Especificaciones
FROM Componentes c
WHERE c.IdCategoria = @IdCategoria;
END;
GO

-- Crear build
CREATE PROCEDURE sp_CrearBuild
    @Nombre NVARCHAR(100),
    @IdUsuario INT
AS
BEGIN
INSERT INTO Builds (Nombre, IdUsuario)
VALUES (@Nombre, @IdUsuario);
END;
GO

-- Agregar componente a build
CREATE PROCEDURE sp_AgregarComponenteABuild
    @IdBuild INT,
    @IdComponente INT
AS
BEGIN
INSERT INTO BuildComponentes (IdBuild, IdComponente)
VALUES (@IdBuild, @IdComponente);
END;
GO

-- Ver build completa con sus componentes
CREATE PROCEDURE sp_VerBuild
    @IdBuild INT
AS
BEGIN
SELECT b.Nombre AS Build,
       u.Nombre AS Usuario,
       c.Nombre AS Componente,
       c.Marca,
       c.Precio,
       c.Especificaciones
FROM Builds b
         INNER JOIN Usuarios u ON b.IdUsuario = u.Id
         INNER JOIN BuildComponentes bc ON b.IdBuild = bc.IdBuild
         INNER JOIN Componentes c ON bc.IdComponente = c.IdComponente
WHERE b.IdBuild = @IdBuild;
END;
GO

-- 4. Insertar categorías base
INSERT INTO Categorias (Nombre) VALUES
('Procesador'),
('Tarjeta Madre'),
('Memoria RAM'),
('Tarjeta Gráfica'),
('Almacenamiento'),
('Fuente de Poder'),
('Caja/Torre'),
('Refrigeración');
GO

-- 5. Insertar componentes de ejemplo (Colombia)
INSERT INTO Componentes (Nombre, Marca, Precio, IdCategoria, Especificaciones) VALUES
('Ryzen 5 5600X', 'AMD', 850000, 1, '6 núcleos, 12 hilos, 4.6GHz boost'),
('Core i5 12400F', 'Intel', 780000, 1, '6 núcleos, 12 hilos, 4.4GHz boost'),
('B550M Aorus Elite', 'Gigabyte', 520000, 2, 'Socket AM4, DDR4'),
('Z690 Tomahawk', 'MSI', 1200000, 2, 'Socket LGA1700, DDR5'),
('Corsair Vengeance 16GB', 'Corsair', 300000, 3, 'DDR4, 3200MHz'),
('HyperX Fury 8GB', 'Kingston', 150000, 3, 'DDR4, 2666MHz'),
('RTX 3060 Ti', 'NVIDIA', 1800000, 4, '8GB GDDR6'),
('RX 6600 XT', 'AMD', 1500000, 4, '8GB GDDR6'),
('SSD NVMe 1TB', 'Crucial', 320000, 5, 'PCIe 3.0'),
('HDD 2TB', 'Seagate', 250000, 5, '7200RPM'),
('EVGA 600W 80+ Bronze', 'EVGA', 250000, 6, 'Certificación 80+ Bronze'),
('Corsair 750W 80+ Gold', 'Corsair', 420000, 6, 'Certificación 80+ Gold'),
('Gabinete Gamemax F15', 'Gamemax', 180000, 7, 'ATX, vidrio templado'),
('Cooler Hyper 212', 'Cooler Master', 150000, 8, 'Air Cooler, 4 heatpipes');
GO

-- 6. Insertar Roles
INSERT INTO Roles (Nombre) VALUES ('Admin');
INSERT INTO Roles (Nombre) VALUES ('Lector');
INSERT INTO Roles (Nombre) VALUES ('Usuario');
GO

-- 7. Insertar Usuarios
-- Contraseña: admin123
INSERT INTO Usuarios (Nombre, Correo, ContrasenaHash, Direccion, RolId)
VALUES ('Admin', 'admin@empresa.com', '240be518fabd2724ddb6f04eeb1da5967448d7e831c08c8fa822809f74c720a9', 'Calle Falsa 123', 1);
GO

-- Contraseña: lector123
INSERT INTO Usuarios (Nombre, Correo, ContrasenaHash, Direccion, RolId)
VALUES ('Lector', 'lector@empresa.com', '3d75d79643ce69834f9ca9d28a82a7bc40934257b62a97f6ce2c9bd6af1bc932', 'Av. Siempre Viva 742', 2);
GO

-- 8. Insertar build de ejemplo
EXEC sp_CrearBuild 'Build Gamer Juan', 1;
EXEC sp_AgregarComponenteABuild 1, 1; -- Ryzen
EXEC sp_AgregarComponenteABuild 1, 3; -- Motherboard
EXEC sp_AgregarComponenteABuild 1, 5; -- RAM
EXEC sp_AgregarComponenteABuild 1, 7; -- GPU
EXEC sp_AgregarComponenteABuild 1, 9; -- SSD
EXEC sp_AgregarComponenteABuild 1, 11; -- Fuente
EXEC sp_AgregarComponenteABuild 1, 13; -- Torre
GO
-- 1. Crear la base de datos
CREATE DATABASE pc_builder;
GO

USE pc_builder;
GO

CREATE TABLE TiposComponentes
(
    Id INT PRIMARY KEY IDENTITY(1,1),
    Nombre NVARCHAR(50) NOT NULL UNIQUE
);

CREATE TABLE Componentes
(
    Id INT PRIMARY KEY IDENTITY(1,1),
    Nombre NVARCHAR(100) NOT NULL,
    Tipo INT NOT NULL, -- Referencia a TiposComponentes
    Marca NVARCHAR(100) NOT NULL,
    Precio DECIMAL(10, 2) NOT NULL,
    ConsumoEnergetico INT NOT NULL, -- en vatios
    Especificaciones NVARCHAR(MAX),
    Imagen NVARCHAR(255),
    Socket NVARCHAR(50), -- Para CPUs
    TipoRAM NVARCHAR(50), -- DDR4, DDR5, etc.
    Formato NVARCHAR(50), -- ATX, MicroATX, etc. para gabinetes
    FOREIGN KEY (Tipo) REFERENCES TiposComponentes (Id)
);

CREATE TABLE Compatibilidad
(
    Id INT PRIMARY KEY IDENTITY(1,1),
    ComponenteId INT NOT NULL,
    ComponenteCompatibleId INT NOT NULL,
    FOREIGN KEY (ComponenteId) REFERENCES Componentes (Id),
    FOREIGN KEY (ComponenteCompatibleId) REFERENCES Componentes (Id),
    UNIQUE (ComponenteId, ComponenteCompatibleId)
);

CREATE TABLE Roles
(
    Id INT PRIMARY KEY IDENTITY(1,1),
    Nombre NVARCHAR(100) NOT NULL
);

CREATE TABLE Usuarios
(
    Id INT PRIMARY KEY IDENTITY(1,1),
    Nombre NVARCHAR(100) NOT NULL,
    Correo NVARCHAR(150) NOT NULL UNIQUE,
    ContrasenaHash NVARCHAR(255) NOT NULL,
    Direccion NVARCHAR(255),
    RolId INT NOT NULL,
    FOREIGN KEY (RolId) REFERENCES Roles (Id)
);

CREATE TABLE Builds
(
    Id INT PRIMARY KEY IDENTITY(1,1),
    UsuarioId INT NOT NULL,
    Nombre NVARCHAR(100),
    PrecioTotal DECIMAL(10, 2) NOT NULL,
    ConsumoEnergeticoTotal INT NOT NULL,
    FechaCreacion DATETIME NOT NULL DEFAULT GETDATE(),
    FOREIGN KEY (UsuarioId) REFERENCES Usuarios (Id)
);

CREATE TABLE ComponentesEnBuild
(
    Id INT PRIMARY KEY IDENTITY(1,1),
    BuildId INT NOT NULL,
    ComponenteId INT NOT NULL,
    FOREIGN KEY (BuildId) REFERENCES Builds (Id),
    FOREIGN KEY (ComponenteId) REFERENCES Componentes (Id)
);

CREATE TABLE Auditorias
(
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Tabla NVARCHAR(255) NOT NULL,
    Accion NVARCHAR(50) NOT NULL,
    LlavePrimaria NVARCHAR(255) NOT NULL,
    Cambios NVARCHAR(MAX) NOT NULL,
    Fecha DATETIME NOT NULL DEFAULT GETDATE(),
    Usuario NVARCHAR(255) NULL
);
GO

-- Insertar Tipos de Componentes
INSERT INTO TiposComponentes (Nombre) VALUES 
('CPU'),
('GPU'),
('RAM'),
('Motherboard'),
('Power Supply');

-- Insertar Componentes
INSERT INTO Componentes (Nombre, Tipo, Marca, Precio, ConsumoEnergetico, Especificaciones, Imagen, Socket, TipoRAM, Formato) VALUES
                                                                                                                                 ('Intel Core i7-11700K', 1, 'Intel', 2500000.00, 125, '8 núcleos, 16 hilos', 'imagen_cpu.jpg', 'LGA1200', NULL, NULL),
                                                                                                                                 ('NVIDIA GeForce RTX 3060', 2, 'NVIDIA', 1800000.00, 170, '12GB GDDR6', 'imagen_gpu.jpg', NULL, NULL, NULL),
                                                                                                                                 ('Corsair Vengeance LPX 16GB', 3, 'Corsair', 400000.00, 75, 'DDR4 3200MHz', 'imagen_ram.jpg', NULL, 'DDR4', NULL),
                                                                                                                                 ('ASUS ROG Strix B550-F', 4, 'ASUS', 800000.00, 60, 'ATX, AM4', 'imagen_motherboard.jpg', NULL, NULL, 'ATX'),
                                                                                                                                 ('Cooler Master MWE 650W', 5, 'Cooler Master', 300000.00, 650, '80 Plus Bronze', 'imagen_psu.jpg', NULL, NULL, NULL);

-- Insertar Roles
INSERT INTO Roles (Nombre) VALUES ('Admin');
INSERT INTO Roles (Nombre) VALUES ('Lector');
INSERT INTO Roles (Nombre) VALUES ('Usuario');

-- Insertar Usuarios
INSERT INTO Usuarios (Nombre, Correo, ContrasenaHash, Direccion, RolId)
VALUES
    ('Admin', 'admin@empresa.com', '240be518fabd2724ddb6f04eeb1da5967448d7e831c08c8fa822809f74c720a9', 'Calle Falsa 123', 1),
    ('Lector', 'lector@empresa.com', '3d75d79643ce69834f9ca9d28a82a7bc40934257b62a97f6ce2c9bd6af1bc932', 'Av. Siempre Viva 742', 2);

-- Insertar Builds
INSERT INTO Builds (UsuarioId, Nombre, PrecioTotal, ConsumoEnergeticoTotal) VALUES
                                                                                (1, 'Build Gamer 1', 5000000.00, 400),
                                                                                (2, 'Build Oficina', 2000000.00, 150);

-- Insertar Componentes en Builds
INSERT INTO ComponentesEnBuild (BuildId, ComponenteId) VALUES
                                                           (1, 1), -- Build Gamer 1 con CPU
                                                           (1, 2), -- Build Gamer 1 con GPU
                                                           (1, 3), -- Build Gamer 1 con RAM
                                                           (2, 4), -- Build Oficina con Motherboard
                                                           (2, 5); -- Build Oficina con Power Supply 


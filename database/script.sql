create database ChallengeBGDB
go

use ChallengeBGDB
go

create table Usuario(
UsuarioId INT IDENTITY(1,1) PRIMARY KEY,
NombreUsuario VARCHAR(50) NOT NULL UNIQUE,
ClaveHash VARCHAR(100) NOT NULL,
NombreCompleto VARCHAR(150) NOT NULL,
FechaCreacion DATETIME DEFAULT GETDATE() NOT NULL
)

create table Proveedor(
ProveedorId INT IDENTITY(1,1) PRIMARY KEY,
Nombre VARCHAR(100) NOT NULL,
FechaCreacion DATETIME DEFAULT GETDATE() NOT NULL
)

create table Producto(
ProductoId INT IDENTITY(1,1) PRIMARY KEY,
Nombre VARCHAR(150) NOT NULL UNIQUE,
Descripcion VARCHAR(500) NULL,
FechaCreacion DATETIME DEFAULT GETDATE() NOT NULL
)

create table ProductoProveedor(
ProductoProveedorId INT IDENTITY(1,1) PRIMARY KEY,
ProductoId INT NOT NULL,
ProveedorId INT NOT NULL,
NumeroLote VARCHAR(50) NOT NULL,
Precio DECIMAL(18,2) NOT NULL,
Stock INT NOT NULL,
FOREIGN KEY (ProductoId) REFERENCES Producto(ProductoId) ON DELETE CASCADE,
FOREIGN KEY (ProveedorId) REFERENCES Proveedor(ProveedorId)
)

select * from Usuario
select * from Proveedor

insert into Usuario(NombreUsuario, ClaveHash, NombreCompleto) values
('admin', '$2b$11$ZcY5W0gASY/jogp0ybTiS.DwfMsZnm0E.ntG5BhngCqT2j/Rj7PwS', 'Administrador')

insert into Proveedor(Nombre) values
('Proveedor A'),
('Proveedor B'),
('Proveedor C')

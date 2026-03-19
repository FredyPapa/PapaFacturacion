CREATE TABLE [sch_facturacion].[Producto]
(
	[iProducto] INT NOT NULL PRIMARY KEY DEFAULT NEXT VALUE FOR seq_productos, 
    [vNombre] VARCHAR(50) NOT NULL, 
    [vDescripcion] VARCHAR(200) NOT NULL, 
    [iLaboratorioCat] INT NOT NULL, 
    [iCategoriaCat] INT NOT NULL, 
    [iMarcaCat] INT NOT NULL, 
    [dcPrecioUnitario] DECIMAL(18, 2) NOT NULL, 
    [iStock] INT NOT NULL,
    [bEstado] BIT NOT NULL DEFAULT 1, 
    [iUsuarioCreacion] INT NOT NULL DEFAULT 1, 
    [dFechaCreacion] DATETIME NOT NULL DEFAULT Getdate(), 
    [iUsuarioModificacion] INT NULL, 
    [dFechaModificacion] DATETIME NULL,
    CONSTRAINT [FK_Producto_ToLaboratorio] FOREIGN KEY ([iLaboratorioCat]) REFERENCES sch_maestro.CatalogoDetalle(iCatalogoDetalle),
    CONSTRAINT [FK_Producto_ToCategoria] FOREIGN KEY (iCategoriaCat) REFERENCES sch_maestro.CatalogoDetalle(iCatalogoDetalle), 
    CONSTRAINT [FK_Producto_ToMarca] FOREIGN KEY (iMarcaCat) REFERENCES sch_maestro.CatalogoDetalle(iCatalogoDetalle)
)

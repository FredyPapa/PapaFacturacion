CREATE TABLE [sch_maestro].[CatalogoDetalle]
(
	[iCatalogoDetalle] INT NOT NULL PRIMARY KEY DEFAULT NEXT VALUE FOR seq_catalogo_detalle, 
    [iCatalogo] INT NOT NULL,
    [vCodigo] VARCHAR(10) NOT NULL UNIQUE, 
    [vDescripcion] VARCHAR(50) NULL, 
    [bEstado] BIT NOT NULL DEFAULT 1, 
    [iUsuarioCreacion] INT NOT NULL DEFAULT 1, 
    [dFechaCreacion] DATETIME NOT NULL DEFAULT Getdate(), 
    [iUsuarioModificacion] INT NULL, 
    [dFechaModificacion] DATETIME NULL,
    CONSTRAINT [FK_CatalogoDetalle_ToCatalogo] FOREIGN KEY (iCatalogo) REFERENCES sch_maestro.Catalogo(iCatalogo),
)

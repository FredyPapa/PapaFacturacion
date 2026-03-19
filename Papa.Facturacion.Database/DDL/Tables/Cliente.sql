CREATE TABLE [sch_facturacion].[Cliente]
(
	[iCliente] INT NOT NULL PRIMARY KEY DEFAULT NEXT VALUE FOR seq_clientes, 
    [iTipoDocumentoCat] INT NOT NULL, 
    [vNumeroDocumento] CHAR(12) NOT NULL, 
    [vApellidoPaterno] VARCHAR(80) NOT NULL, 
    [vApellidoMaterno] VARCHAR(80) NOT NULL, 
    [vNombres] VARCHAR(100) NOT NULL, 
    [vDireccion] VARCHAR(200) NOT NULL, 
    [vCorreoElectronico] VARCHAR(200) NULL, 
    [vCelular] CHAR(9) NOT NULL, 
    [bEstado] BIT NOT NULL DEFAULT 1, 
    [iUsuarioCreacion] INT NOT NULL DEFAULT 1, 
    [dFechaCreacion] DATETIME NOT NULL DEFAULT Getdate(), 
    [iUsuarioModificacion] INT NULL, 
    [dFechaModificacion] DATETIME NULL,
    CONSTRAINT [FK_Cliente_ToTipoDocumento] FOREIGN KEY (iTipoDocumentoCat) REFERENCES sch_maestro.CatalogoDetalle(iCatalogoDetalle),
)

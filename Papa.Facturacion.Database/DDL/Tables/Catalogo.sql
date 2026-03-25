CREATE TABLE [sch_maestro].[Catalogo]
(
	[iId] INT NOT NULL PRIMARY KEY DEFAULT NEXT VALUE FOR seq_catalogo, 
    [vCodigo] VARCHAR(10) NOT NULL UNIQUE, 
    [vNombre] VARCHAR(50) NOT NULL, 
    [vDescripcion] VARCHAR(200) NULL,
    [bEstado] BIT NOT NULL DEFAULT 1, 
    [iUsuarioCreacion] INT NOT NULL DEFAULT 1, 
    [dFechaCreacion] DATETIME NOT NULL DEFAULT Getdate(), 
    [iUsuarioModificacion] INT NULL,
    [dFechaModificacion] DATETIME NULL, 
)

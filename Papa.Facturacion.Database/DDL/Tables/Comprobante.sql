CREATE TABLE [sch_facturacion].[Comprobante]
(
	[iComprobante] INT NOT NULL PRIMARY KEY DEFAULT NEXT VALUE FOR seq_comprobantes,  
    [iTipoComprobanteCat] INT NOT NULL,
    [iTipoPagoCat] INT NOT NULL,
    [iCliente] INT NOT NULL,
    [dcTotalBruto] DECIMAL(18, 2) NOT NULL, 
    [dcIgv] DECIMAL(18, 2) DEFAULT 0, 
    [dcTotaNeto] DECIMAL(18, 2) NOT NULL, 
    [bEstado] BIT NOT NULL DEFAULT 1, 
    [iUsuarioCreacion] INT NOT NULL DEFAULT 1, 
    [dFechaCreacion] DATETIME NOT NULL DEFAULT Getdate(), 
    [iUsuarioModificacion] INT NULL, 
    [dFechaModificacion] DATETIME NULL,
    CONSTRAINT [FK_Comprobante_ToTipoComprobante] FOREIGN KEY ([iTipoComprobanteCat]) REFERENCES sch_maestro.CatalogoDetalle(iCatalogoDetalle),
    CONSTRAINT [FK_Comprobante_ToTipoPago] FOREIGN KEY ([iTipoPagoCat]) REFERENCES sch_maestro.CatalogoDetalle(iCatalogoDetalle),
    CONSTRAINT [FK_Comprobante_ToCliente] FOREIGN KEY ([iCliente]) REFERENCES sch_facturacion.Cliente(iCliente)
)

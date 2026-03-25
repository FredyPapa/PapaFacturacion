CREATE TABLE [sch_facturacion].[ComprobanteDetalle]
(
	[iId] INT NOT NULL PRIMARY KEY DEFAULT NEXT VALUE FOR seq_comprobantes_detalle,
    [iComprobante] INT NOT NULL, 
    [iProducto] INT NOT NULL, 
    [iCantidad] DECIMAL(18, 2) NOT NULL, 
    [dcPrecioUnitario] DECIMAL(18, 2) NOT NULL, 
    [dcTotal] DECIMAL(18, 2) NOT NULL, 
    [bEstado] BIT NOT NULL DEFAULT 1, 
    [iUsuarioCreacion] INT NOT NULL DEFAULT 1, 
    [dFechaCreacion] DATETIME NOT NULL DEFAULT Getdate(), 
    [iUsuarioModificacion] INT NULL, 
    [dFechaModificacion] DATETIME NULL,
    CONSTRAINT [FK_ComprobanteDetalle_ToComprobante] FOREIGN KEY ([iComprobante]) REFERENCES sch_facturacion.Comprobante([iId]), 
    CONSTRAINT [FK_ComprobanteDetalle_ToProducto] FOREIGN KEY ([iProducto]) REFERENCES sch_facturacion.Producto([iId])
)

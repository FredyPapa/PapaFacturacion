

/*
Plantilla de script posterior a la implementación							
--------------------------------------------------------------------------------------
 Este archivo contiene instrucciones de SQL que se anexarán al script de compilación.		
 Use la sintaxis de SQLCMD para incluir un archivo en el script posterior a la implementación.			
 Ejemplo:      :r .\miArchivo.sql								
 Use la sintaxis de SQLCMD para hacer referencia a una variable en el script posterior a la implementación.		
 Ejemplo:      :setvar TableName miTabla							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/

-- LABORATORIO
DECLARE @iLaboratorioCat INT = NEXT VALUE FOR dbo.Seq_Catalogo;

INSERT INTO sch_maestro.Catalogo (iCatalogo, vCodigo, vNombre, vDescripcion)
VALUES (@iLaboratorioCat, 'MAE_LAB', 'Laboratorio', 'Catálogo de laboratorios de los productos');

INSERT INTO sch_maestro.CatalogoDetalle (iCatalogoDetalle, iCatalogo, vCodigo, vDescripcion)
VALUES
    (NEXT VALUE FOR dbo.seq_catalogo_detalle, @iLaboratorioCat, 'L_MFAR', 'Medifarma'),
    (NEXT VALUE FOR dbo.seq_catalogo_detalle, @iLaboratorioCat, 'L_FARI', 'Farmindustria'),
    (NEXT VALUE FOR dbo.seq_catalogo_detalle, @iLaboratorioCat, 'L_HERS', 'Hersil');



-- CATEGORÍA
DECLARE @iCategoriaCat INT = NEXT VALUE FOR dbo.Seq_Catalogo;

INSERT INTO sch_maestro.Catalogo (iCatalogo, vCodigo, vNombre, vDescripcion)
VALUES (@iCategoriaCat, 'MAE_CAT', 'Categoría', 'Catálogo de categorías de los productos');

INSERT INTO sch_maestro.CatalogoDetalle (iCatalogoDetalle, iCatalogo, vCodigo, vDescripcion)
VALUES
    (NEXT VALUE FOR dbo.seq_catalogo_detalle, @iCategoriaCat, 'C_ABIO', 'Antibióticos'),
    (NEXT VALUE FOR dbo.seq_catalogo_detalle, @iCategoriaCat, 'C_AGCO', 'Analgésicos'),
    (NEXT VALUE FOR dbo.seq_catalogo_detalle, @iCategoriaCat, 'C_VITA', 'Vitaminas'),
    (NEXT VALUE FOR dbo.seq_catalogo_detalle, @iCategoriaCat, 'C_CBUC', 'Cuidado Bucal'),
    (NEXT VALUE FOR dbo.seq_catalogo_detalle, @iCategoriaCat, 'C_DERM', 'Antigripal');



-- MARCA
DECLARE @iMarcaCat INT = NEXT VALUE FOR dbo.Seq_Catalogo;

INSERT INTO sch_maestro.Catalogo (iCatalogo, vCodigo, vNombre, vDescripcion)
VALUES (@iMarcaCat, 'MAE_MAR', 'Marca', 'Catálogo de marcas de los productos');

INSERT INTO sch_maestro.CatalogoDetalle (iCatalogoDetalle, iCatalogo, vCodigo, vDescripcion)
VALUES
    (NEXT VALUE FOR dbo.seq_catalogo_detalle, @iMarcaCat, 'C_AMOX', 'Amoxil'),
    (NEXT VALUE FOR dbo.seq_catalogo_detalle, @iMarcaCat, 'C_GENTA', 'Gentamicina'),
    (NEXT VALUE FOR dbo.seq_catalogo_detalle, @iMarcaCat, 'C_VITAM', 'Vitamic'),
    (NEXT VALUE FOR dbo.seq_catalogo_detalle, @iMarcaCat, 'C_NAST', 'Nastizol'),
    (NEXT VALUE FOR dbo.seq_catalogo_detalle, @iMarcaCat, 'C_DENT', 'Dentito'),
    (NEXT VALUE FOR dbo.seq_catalogo_detalle, @iMarcaCat, 'C_TAPS', 'Tapsin');



-- TIPO DE DOCUMENTO
DECLARE @iTipoDocumentoCat INT = NEXT VALUE FOR dbo.Seq_Catalogo;

INSERT INTO sch_maestro.Catalogo (iCatalogo, vCodigo, vNombre, vDescripcion)
VALUES (@iTipoDocumentoCat, 'MAE_TD', 'Tipo de documento', 'Catálogo de tipo de documento de los clientes');

INSERT INTO sch_maestro.CatalogoDetalle (iCatalogoDetalle, iCatalogo, vCodigo, vDescripcion)
VALUES
    (NEXT VALUE FOR dbo.seq_catalogo_detalle, @iTipoDocumentoCat, 'C_DNI', 'DNI'),
    (NEXT VALUE FOR dbo.seq_catalogo_detalle, @iTipoDocumentoCat, 'C_CE', 'Carnet de extranjería'),
    (NEXT VALUE FOR dbo.seq_catalogo_detalle, @iTipoDocumentoCat, 'C_PAS', 'Pasaporte');
    


-- TIPO DE COMPROBANTE
DECLARE @iTipoComprobanteCat INT = NEXT VALUE FOR dbo.Seq_Catalogo;

INSERT INTO sch_maestro.Catalogo (iCatalogo, vCodigo, vNombre, vDescripcion)
VALUES (@iTipoComprobanteCat, 'MAE_TC', 'Tipo de comprobante', 'Catálogo de tipo de comprobante de las ventas');

INSERT INTO sch_maestro.CatalogoDetalle (iCatalogoDetalle, iCatalogo, vCodigo, vDescripcion)
VALUES
    (NEXT VALUE FOR dbo.seq_catalogo_detalle, @iTipoComprobanteCat, 'C_FAC', 'Factura'),
    (NEXT VALUE FOR dbo.seq_catalogo_detalle, @iTipoComprobanteCat, 'C_BOL', 'Boleta');



-- TIPO DE PAGO
DECLARE @iTipoPagoCat INT = NEXT VALUE FOR dbo.Seq_Catalogo;

INSERT INTO sch_maestro.Catalogo (iCatalogo, vCodigo, vNombre, vDescripcion)
VALUES (@iTipoPagoCat, 'MAE_TP', 'Tipo de pago', 'Catálogo de tipo de pago de las ventas');

INSERT INTO sch_maestro.CatalogoDetalle (iCatalogoDetalle, iCatalogo, vCodigo, vDescripcion)
VALUES
    (NEXT VALUE FOR dbo.seq_catalogo_detalle, @iTipoPagoCat, 'C_CON', 'Contado'),
    (NEXT VALUE FOR dbo.seq_catalogo_detalle, @iTipoPagoCat, 'C_CRE', 'Crédito');



-- Consulta para validar los datos ingresados
SELECT 
    C.vNombre AS Catalogo_Padre,
    D.iCatalogoDetalle AS ID_Detalle,
    D.vCodigo AS Codigo_Hijo,
    D.vDescripcion AS Descripcion_Hijo
FROM sch_maestro.Catalogo C
INNER JOIN sch_maestro.CatalogoDetalle D ON C.iCatalogo = D.iCatalogo
ORDER BY C.iCatalogo, D.iCatalogoDetalle;

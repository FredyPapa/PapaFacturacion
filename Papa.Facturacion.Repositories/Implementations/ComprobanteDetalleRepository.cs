using Papa.Facturacion.DataAccess;
using Papa.Facturacion.DataAccess.Context;
using Papa.Facturacion.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Papa.Facturacion.Repositories.Implementations
{
    public class ComprobanteDetalleRepository(PapaFacturacionContext context) : BaseRepository<ComprobanteDetalle>(context) , IComprobanteDetalleRepository
    {
    }
}

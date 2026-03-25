using Papa.Facturacion.DataAccess;
using Papa.Facturacion.DataAccess.Context;
using Papa.Facturacion.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Papa.Facturacion.Repositories.Implementations
{
    public class ClienteRepository(PapaFacturacionContext context) : BaseRepository<Cliente>(context) , IClienteRepository
    {
    }
}

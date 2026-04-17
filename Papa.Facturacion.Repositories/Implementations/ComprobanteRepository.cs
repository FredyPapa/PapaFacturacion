using Microsoft.EntityFrameworkCore;
using Papa.Facturacion.DataAccess;
using Papa.Facturacion.DataAccess.Context;
using Papa.Facturacion.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Papa.Facturacion.Repositories.Implementations
{
    public class ComprobanteRepository(PapaFacturacionContext context) : BaseRepository<Comprobante>(context) , IComprobanteRepository
    {
        public async Task CreateAsync(Comprobante request)
        {
            await using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                await _context.Comprobantes.AddAsync(request);

                //Actualizar stock de los productos del pedido

                foreach (var item in request.ComprobanteDetalles)
                {
                    var product = await _context.Productos.FirstOrDefaultAsync(p => p.BEstado && p.IId == item.IProducto);
                    if (product != null)
                    {
                        product.IStock -= Convert.ToInt32(item.ICantidad);
                    }
                }

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }
    }
}

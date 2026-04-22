using System;
using System.Collections.Generic;
using System.Text;

namespace Papa.Facturacion.Business.Interfaces
{
    public interface IExcelService
    {
        MemoryStream ExportExcel<TData>(ICollection<TData> data, string namePage);
    }
}

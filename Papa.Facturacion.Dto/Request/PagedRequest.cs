using System;
using System.Collections.Generic;
using System.Text;

namespace Papa.Facturacion.Dto.Request
{
    public class PagedRequest
    {
        public int Page { get; set; } = 1;
        public int Rows { get; set; } = 10;
    }
}

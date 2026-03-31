using System;
using System.Collections.Generic;
using System.Text;

namespace Papa.Facturacion.Dto.Request
{
    public class SearchListRequest : PagedRequest
    {
        public string? Filter { get; set; }
    }
}

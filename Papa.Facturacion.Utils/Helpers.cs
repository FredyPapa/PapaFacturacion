using System;
using System.Collections.Generic;
using System.Text;

namespace Papa.Facturacion.Utils
{
    public static class Helpers
    {
        public static int CalculatePageCount(int totalItem, int pageSize)
        {
            if (pageSize <= 0) return 0;
            return (int)Math.Ceiling((double)totalItem / pageSize);
        }
    }
}

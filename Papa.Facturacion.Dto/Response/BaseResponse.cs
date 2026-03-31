using System;
using System.Collections.Generic;
using System.Text;

namespace Papa.Facturacion.Dto.Response
{
    public class BaseResponse
    {
        public string? Message { get; set; }
        public string? ErrorCode { get; set; }
        public bool? IsSuccess { get; set; }
    }

    public class BaseResponse<T> : BaseResponse
    {
        public T? Result { get; set; }

    }

    public class PagedResponse<T> : BaseResponse
    {
        public ICollection<T> Result { get; set; } = new List<T>();
        public int TotalPages { get; set; }
        public int TotalRowPerPages { get; set; }
    }
}

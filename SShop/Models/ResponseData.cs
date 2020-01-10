using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SShop.Models
{
    public class ResponseData<T> where T : class, new()
    {
        public string Message { get; set; }
        public T Data { get; set; }
        public int Code { get; set; }
    }
}

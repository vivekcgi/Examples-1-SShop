using SShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SShop.Services
{
    public interface IProductService
    {
        Task<Product> Add(Product newProduct);
        Task<Product> Get(int? id);
        Task<Product> Update(Product product);
        Task<int> Delete(int? id);
    }
}

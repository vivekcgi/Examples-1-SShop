using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SShop.Data;
using SShop.Models;

namespace SShop.Services
{
    public class ProductService : IProductService
    {
        private readonly ApplicationDbContext _context;

        public ProductService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Product> Add(Product newProduct)
        {
            Product product = _context.Products.Add(newProduct).Entity;            
            await _context.SaveChangesAsync();
            return product;
        }

        public async Task<int> Delete(int? id)
        {
            Product product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                throw new KeyNotFoundException("Product with id not found.");
            }
            else
            {
                _context.Entry<Product>(product).State = Microsoft.EntityFrameworkCore.EntityState.Deleted;
                int x = await _context.SaveChangesAsync();
                return x;
            }
        }

        public async Task<Product> Get(int? id)
        {
            Product product = await _context.Products.FindAsync(id);            
            if (product == null)
            {
                throw new KeyNotFoundException("Product with id not found.");
            }
            else
            {
                product = _context.Products.Include(a => a.Customer).First(a => a.Id == product.Id);
                return product;
            }
        }

        public async Task<Product> Update(Product product)
        {
            _context.Entry<Product>(product).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            await _context.SaveChangesAsync();

            product = _context.Products.Include(a => a.Customer).First(a=>a.Id == product.Id);
            return product;
        }
    }
}

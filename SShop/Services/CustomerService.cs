using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SShop.Data;
using SShop.Models;

namespace SShop.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ApplicationDbContext _context;

        public CustomerService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Customer> Add(Customer newCustomer)
        {
            Customer customer = _context.Customers.Add(newCustomer).Entity;
            await _context.SaveChangesAsync();
            return customer;
        }

        public async Task<int> Delete(int? id)
        {
            Customer customer = await _context.Customers.FindAsync(id);
            if (customer == null)
            {
                throw new KeyNotFoundException("Customer with id not found.");
            }
            else
            {
                _context.Entry<Customer>(customer).State = Microsoft.EntityFrameworkCore.EntityState.Deleted;
                int x = await _context.SaveChangesAsync();
                return x;
            }
        }

        public async Task<Customer> Get(int? id)
        {
            Customer customer = await _context.Customers.FindAsync(id);
            if (customer == null)
            {
                throw new KeyNotFoundException("Customer with id not found.");
            }
            else
            {
                return customer;
            }
        }

        public async Task<Customer> Update(Customer customer)
        {
            _context.Entry<Customer>(customer).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            await _context.SaveChangesAsync();
            return customer;

        }
    }
}

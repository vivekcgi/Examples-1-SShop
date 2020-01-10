using SShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SShop.Services
{
    public interface ICustomerService
    {
        Task<Customer> Add(Customer newCustomer);
        Task<Customer> Get(int? id);
        Task<Customer> Update(Customer customer);
        Task<int> Delete(int? id);
    }
}

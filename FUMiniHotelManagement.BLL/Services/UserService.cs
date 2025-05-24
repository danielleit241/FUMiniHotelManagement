using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FUMiniHotelManagement.DAL.Entities;
using FUMiniHotelManagement.DAL.Repositories;

namespace FUMiniHotelManagement.BLL.Services
{
    public class UserService
    {
        private CustomerRepo repo = new();

        public Customer? Login(string email, string password)
        {
            var user = repo.Authorization(email.Trim(), password.Trim());
            return user;
        }

        public List<Customer> GetCustomers()
        {
            return repo.GetAll();
        }

        public Customer? GetCustomerById(int id)
        {
            return repo.GetOne(id);
        }

        public void CreateCustomer(Customer customer)
        {
            repo.Create(customer);
        }

        public void UpdateCustomer(Customer customer)
        {
            repo.Update(customer);
        }
        public void DeleteCustomer(Customer customer)
        {
            repo.Delete(customer);
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FUMiniHotelManagement.DAL.Entities;
using FUMiniHotelManagement.DAL.Repositories;
using Microsoft.IdentityModel.Tokens;

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

        public List<Customer> SearchCustomerByFullNameAndTelephone(string fullName, string telephone)
        {
            List<Customer> customers = repo.GetAll();
            if (fullName.IsNullOrEmpty() && telephone.IsNullOrEmpty())
            {
                return customers;
            }

            if (!fullName.IsNullOrEmpty())
            {
                customers = customers.Where(x => x.CustomerFullName.ToLower().Contains(fullName.ToLower())).ToList();
            }

            if(!telephone.IsNullOrEmpty())
            {
                customers = customers.Where(x => x.Telephone != null && x.Telephone.Contains(telephone)).ToList();
            }
            return customers;
        }

        public List<Customer> SearchCustomerByFullNameOrTelephone(string fullName, string telephone)
        {
            List<Customer> customers = repo.GetAll();
            if (fullName.IsNullOrEmpty() && telephone.IsNullOrEmpty())
            {
                return customers;
            }

            if(!fullName.IsNullOrEmpty() && !telephone.IsNullOrEmpty())
            {
                customers = customers.Where(x => (!fullName.IsNullOrEmpty() && x.CustomerFullName.ToLower().Contains(fullName.ToLower()) ||
                    (!telephone.IsNullOrEmpty() && x.Telephone.ToLower().Contains(telephone.ToLower()))
                )).ToList();
            } 
            else if (!fullName.IsNullOrEmpty())
            {
                customers = customers.Where(x => (!fullName.IsNullOrEmpty() && x.CustomerFullName.ToLower().Contains(fullName.ToLower()))).ToList();
            }
            else if (!telephone.IsNullOrEmpty())
            {
                customers = customers.Where(x => (!telephone.IsNullOrEmpty() && x.Telephone.ToLower().Contains(telephone.ToLower())
                )).ToList();
            }

            return customers;
        }

        //public int GetMaxCusId()
        //{
        //    List<Customer> customerList = repo.GetAll();
        //    int maxId = customerList.Max(x => x.CustomerId);
        //    return maxId;
        //}
    }
}

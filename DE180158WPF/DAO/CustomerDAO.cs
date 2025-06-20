using System;
using System.Collections.Generic;
using System.Linq;
using DE180158WPF.Models;

namespace DE180158WPF.DAO
{
    public class CustomerDAO
    {
        private readonly InMemoryDatabase _db = InMemoryDatabase.Instance;

        public List<Customer> GetAll() =>
            _db.Customers.Where(c => c.CustomerStatus == 1).ToList();

        public Customer? GetById(int id) =>
            _db.Customers.FirstOrDefault(c => c.CustomerID == id);

        public Customer? GetByEmail(string email) =>
            _db.Customers.FirstOrDefault(c => c.EmailAddress.Equals(email, StringComparison.OrdinalIgnoreCase));

        public bool Add(Customer customer)
        {
            if (_db.Customers.Any(c => c.EmailAddress.Equals(customer.EmailAddress, StringComparison.OrdinalIgnoreCase)))
            {
                return false;
            }
            customer.CustomerID = _db.NextCustomerId();
            _db.Customers.Add(customer);
            return true;
        }

        public bool Update(Customer customer)
        {
            var existing = GetById(customer.CustomerID);
            if (existing != null)
            {
                existing.CustomerFullName = customer.CustomerFullName;
                existing.Telephone = customer.Telephone;
                existing.EmailAddress = customer.EmailAddress;
                existing.CustomerBirthday = customer.CustomerBirthday;
                existing.Password = customer.Password;
                existing.CustomerStatus = customer.CustomerStatus;
                return true;
            }
            return false;
        }

        public void Delete(int id)
        {
            var customer = GetById(id);
            if (customer != null)
            {
                customer.CustomerStatus = 2; // Mark as deleted
            }
        }

        public List<Customer> Search(string keyword) =>
            _db.Customers
               .Where(c => c.CustomerStatus == 1 &&
                          (c.CustomerFullName.Contains(keyword, StringComparison.OrdinalIgnoreCase) ||
                           c.EmailAddress.Contains(keyword, StringComparison.OrdinalIgnoreCase)))
               .ToList();
    }
}

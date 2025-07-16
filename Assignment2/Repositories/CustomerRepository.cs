using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObject;
using DataAccessLayer;

namespace Repositories
{
    
    public class CustomerRepository
    {
        private CustomerDAO customerDAO;
        public CustomerRepository()
        {
            customerDAO = new CustomerDAO();
        }
        public Customer? GetByEmail(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                throw new ArgumentException("Email cannot be null or empty.", nameof(email));
            }
            return customerDAO.GetByEmail(email);
        }
    }
}

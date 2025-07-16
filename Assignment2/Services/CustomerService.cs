using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObject;
using Repositories;

namespace Services
{
    public class CustomerService
    {
        private CustomerRepository customerRepository;
        public CustomerService()
        {
            customerRepository = new CustomerRepository();
        }

        public Customer GetByEmail(String email)
        {
            return customerRepository.GetByEmail(email);      
        }
    }
}

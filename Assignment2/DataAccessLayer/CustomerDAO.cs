using BusinessObject;

namespace DataAccessLayer
{
    public class CustomerDAO
    {
        private static CustomerDAO instance = null;
        private static readonly object lockObj = new object();

        public static CustomerDAO Instance
        {
            get
            {
                lock (lockObj)
                {
                    if (instance == null)
                    {
                        instance = new CustomerDAO();
                    }
                    return instance;
                }
            }
        }

        public Customer? GetByEmail(string email)
        {
            using var context = new FuminiHotelManagementContext();
            return context.Customers
                .FirstOrDefault(c => c.EmailAddress == email);
        }

        public Customer? GetCustomerByEmailAndPassword(string email, string password)
        {
            using var context = new FuminiHotelManagementContext();
            return context.Customers
                .FirstOrDefault(c => c.EmailAddress == email && c.Password == password);
        }

        public List<Customer> GetAllCustomers()
        {
            using var context = new FuminiHotelManagementContext();
            return context.Customers.ToList();
        }

        public Customer? GetCustomerById(int id)
        {
            using var context = new FuminiHotelManagementContext();
            return context.Customers.FirstOrDefault(c => c.CustomerId == id);
        }

        public void AddCustomer(Customer customer)
        {
            using var context = new FuminiHotelManagementContext();
            context.Customers.Add(customer);
            context.SaveChanges();
        }

        public void UpdateCustomer(Customer customer)
        {
            using var context = new FuminiHotelManagementContext();
            context.Customers.Update(customer);
            context.SaveChanges();
        }

        public void DeleteCustomer(int id)
        {
            using var context = new FuminiHotelManagementContext();
            var customer = context.Customers.FirstOrDefault(c => c.CustomerId == id);
            if (customer != null)
            {
                context.Customers.Remove(customer);
                context.SaveChanges();
            }
        }
    }
}

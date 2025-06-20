namespace DE180158WPF.Models
{
    public class Customer
    {
        public int CustomerID { get; set; }
        public string CustomerFullName { get; set; } = string.Empty;
        public string Telephone { get; set; } = string.Empty;
        public string EmailAddress { get; set; } = string.Empty;
        public DateTime CustomerBirthday { get; set; }
        public int CustomerStatus { get; set; }
        public string Password { get; set; } = string.Empty;
    }
}

using FUMiniHotelManagement.DAL.Entities;


namespace FUMiniHotelManagement.DAL.Repositories
{
    public class CustomerRepo
    {
        private FuminiHotelManagementContext _context;

        public Customer? Authorization(string email, string password)
        {
            _context = new();
            var customer = _context.Customers.FirstOrDefault(c => c.EmailAddress.ToLower() == email.ToLower() && c.Password == password);
            return customer;
        }

        public List<Customer> GetAll()
        {
            _context = new();
            return _context.Customers.ToList();
        }

        public Customer? GetOne(int id)
        {
            _context = new();
            return _context.Customers.FirstOrDefault(c => c.CustomerId == id);
        }

        public void Create(Customer x)
        {
            _context = new();
            _context.Customers.Add(x);
            _context.SaveChanges();
        }

        public void Update(Customer x)
        {
            _context = new();
            _context.Customers.Update(x);
            _context.SaveChanges();
        }

        public void Delete(Customer x)
        {
            _context = new();
            _context.Customers.Update(x);
            _context.SaveChanges();
        }




    }
}

using System.Threading.Tasks;
using API.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly DataContext _context;
        public CustomerRepository(DataContext context)
        {
            _context = context;
        }
        public async Task<bool> CustomerExists(string username)
        {
            if (await _context.Customer.AnyAsync(x => x.UserName == username))
                return true;
            
            return false;
        }

        public async Task<Customer> DeleteCustomer(int Id)
        {
            var customer = await _context.Customer.FirstOrDefaultAsync(x => x.Id == Id);

            _context.Remove(customer);
            await _context.SaveChangesAsync();

            return customer;
        }

        public async Task<Customer> Login(string username, string password)
        {
            var customer = await _context.Customer.FirstOrDefaultAsync(x => x.UserName == username);

            if (customer == null)
                return null;
            
            if (!VerifyPasswordHash(password, customer.PasswordHash, customer.PasswordSalt))
                return null;

            return customer;
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using(var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for(int i = 0; i <  computedHash.Length; i++)
                {
                    if (computedHash[i] != passwordHash[i]) return false;
                }

                return true;
            }
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using(var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        public async Task<Customer> RegisterCustomer(Customer customer, string password)
        {
            byte[] passwordHash, paswordSalt;
            CreatePasswordHash(password, out passwordHash, out paswordSalt);

            customer.PasswordHash = passwordHash;
            customer.PasswordSalt = paswordSalt;

            await _context.Customer.AddAsync(customer);
            await _context.SaveChangesAsync();

            return customer;
        }

        public async Task<Customer> UpdateCustomer(Customer _customer, int Id)
        {
            var customer = await _context.Customer.FirstOrDefaultAsync(x => x.Id == Id);

            if (customer == null)
                return null;

            customer.FirstName = _customer.FirstName;
            customer.LastName = _customer.LastName;
            customer.Email = _customer.Email;
            customer.ContactNumber = _customer.ContactNumber;
            customer.UserName = _customer.UserName;
            
            await _context.SaveChangesAsync();

            return customer;
        }

        public async Task<Customer> ChangePassword(Customer _customer,string oldPassword, string newPassword)
        {
            var customer = await _context.Customer.FirstOrDefaultAsync(x => x.UserName == _customer.UserName);

            if (!VerifyPasswordHash(oldPassword, _customer.PasswordHash, _customer.PasswordSalt))
                return null;
            
            byte[] passwordHash, paswordSalt;
            CreatePasswordHash(newPassword, out passwordHash, out paswordSalt);

            customer.PasswordHash = passwordHash;
            customer.PasswordSalt = paswordSalt;

            await _context.SaveChangesAsync();

            return customer;
        }
    }
}
using System.Threading.Tasks;
using API.Entities;

namespace API.Data
{
    public interface ICustomerRepository
    {
        Task<Customer> RegisterCustomer(Customer customer, string password);
        Task<bool> CustomerExists(string username);
        Task<Customer> UpdateCustomer(Customer _customer, int Id);
        Task<Customer> DeleteCustomer(int Id);
        Task<Customer> Login (string username, string password);
        Task<Customer> ChangePassword (Customer _customer,string oldPassword, string newPassword);

    }
}
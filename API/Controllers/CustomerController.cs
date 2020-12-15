using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using API.Data;
using API.Dtos;
using API.Entities;
using API.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace API.Controllers
{
    public class CustomerController : BaseApiController
    {
        private readonly IConfiguration _config;
        private readonly DataContext _context;
        private readonly ITokenService _tokenService;
        public CustomerController(DataContext context, ITokenService tokenService)
        {
            _tokenService = tokenService;
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Customer>>> GetCustomers()
        {
            var customers = _context.Customer.ToListAsync();

            return await customers;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Customer>> GetCustomer(int id)
        {
            var customer = _context.Customer.FindAsync(id);

            return await customer;
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register(CustomerRegisterDto customerRegisterDto)
        {
            customerRegisterDto.Username = customerRegisterDto.Username.ToLower();

            if (await CustomerExists(customerRegisterDto.Username))
                return BadRequest("Username already exists");
    
            var hmac = new System.Security.Cryptography.HMACSHA512();

            var customerToCreate = new Customer
            {
                UserName = customerRegisterDto.Username,
                FirstName = customerRegisterDto.FirstName,
                LastName = customerRegisterDto.LastName,
                ContactNumber = customerRegisterDto.ContactNumber,
                Email = customerRegisterDto.Email,
                PasswordSalt = hmac.Key,
                PasswordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(customerRegisterDto.Password))
            };

            await _context.Customer.AddAsync(customerToCreate);
            await _context.SaveChangesAsync();

            return new UserDto
            {
                Username = customerToCreate.UserName,
                Token = _tokenService.CreateToken(customerToCreate)
            };
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(CustomerLoginDto customerLoginDto)
        {
            var customer = await _context.Customer.FirstOrDefaultAsync(x => x.UserName == customerLoginDto.Username);

            if (customer == null)
                return Unauthorized("Invalid username or password.");
            
            if (!VerifyPasswordHash(customerLoginDto.Password, customer.PasswordHash, customer.PasswordSalt))
                return null;      

            return new UserDto
            {
                Username = customer.UserName,
                Token = _tokenService.CreateToken(customer)
            };
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

        [HttpPost("update")]
        public async Task<IActionResult> UpdateCustomer(CustomerUpdateDto customerUpdateDto, int Id)
        {
            if (!await CustomerExists(Id))
                return BadRequest("Customer doesn't exists");

            var customer = await _context.Customer.FirstOrDefaultAsync(x => x.Id == Id);

            if (customer == null)
                return null;

            customer.FirstName = customerUpdateDto.FirstName;
            customer.LastName = customerUpdateDto.LastName;
            customer.Email = customerUpdateDto.Email;
            customer.ContactNumber = customerUpdateDto.ContactNumber;
            customer.UserName = customerUpdateDto.UserName;
            
            await _context.SaveChangesAsync();

            return StatusCode(200);
        }
        private async Task<bool> CustomerExists(int id)
        {
            if (await _context.Customer.AnyAsync(x => x.Id == id))
                return true;
            
            return false;
        }
        private async Task<bool> CustomerExists(string username)
        {
            if (await _context.Customer.AnyAsync(x => x.UserName == username))
                return true;
            
            return false;
        }
    }
}
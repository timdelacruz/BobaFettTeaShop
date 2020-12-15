using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    public class TransactionController : BaseApiController
    {
        private readonly DataContext _context;
        public TransactionController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Transaction>>> GetTransactions()
        {
            var transactions = _context.Transaction.ToListAsync();

            return await transactions;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Transaction>> GetTransaction(int id)
        {
            var transaction = _context.Transaction.FindAsync(id);

            return await transaction;
        }

        [HttpGet("customer/{customerId}")]
        public async Task<ActionResult<List<Transaction>>> GetCustomerTransaction(int customerId)
        {
            var customerTransaction = _context.Transaction.Where(x => x.CustomerId == customerId);

            return await customerTransaction.ToListAsync();
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddItem(Transaction transactionDto)
        {
            var transaction = new Transaction
            {
                CustomerId = transactionDto.CustomerId,
                TotalPrice = transactionDto.TotalPrice
            };

            await _context.Transaction.AddAsync(transaction);
            await _context.SaveChangesAsync();

            return StatusCode(201);
        }
        
        [HttpPost("edit")]
        public async Task<IActionResult> EditItem(Transaction transactionDto, int id)
        {
            var transaction = await _context.Transaction.FirstOrDefaultAsync(x => x.Id == id);

            if (transaction == null)
                return null;

            transaction.CustomerId = transactionDto.CustomerId;
            transaction.TotalPrice = transactionDto.TotalPrice;

            await _context.SaveChangesAsync();

            return StatusCode(200);
        }

        [HttpPost("delete")]
        public async Task<IActionResult> DeleteItem(int id)
        {
            var transaction = new Transaction {Id = id};
            _context.Transaction.Attach(transaction);
            _context.Transaction.Remove(transaction);

            await _context.SaveChangesAsync();

            return StatusCode(200);
        }
    }
}
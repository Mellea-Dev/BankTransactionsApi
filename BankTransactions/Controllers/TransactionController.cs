
using BankTransactions.Data;
using BankTransactions.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace BankTransactionsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly BankContext dbBankContext;
        public TransactionController(BankContext dbBankContext)
        {
            this.dbBankContext = dbBankContext;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetAllTransactions()
        {
            var transactions = dbBankContext.Transactions.ToList();

            return Ok(transactions);
        }

        [HttpGet]
        [Route("id:int")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetTransaction(int id)
        {
            var transaction = dbBankContext.Transactions.Find(id);

            return Ok(transaction);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult AddTransaction(TransactionModel addTransaction)
        {
            var transaction = new TransactionModel()
            {
                Amount = addTransaction.Amount,
                Description = addTransaction.Description,
                Type = addTransaction.Type,
                AccountNumber = addTransaction.AccountNumber,
                FromAccountNumber = addTransaction.FromAccountNumber,
                ToAccountNumber = addTransaction.ToAccountNumber
            };

            dbBankContext.Transactions.Add(transaction);
            dbBankContext.SaveChanges();

            return Ok("Transaction Successfuly Submited");
        }
    }
}
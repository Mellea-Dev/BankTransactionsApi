using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BankTransactions.Data;
using BankTransactions.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BankTransactions.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly BankContext dbBankContext;

        public AccountController(BankContext dbBankContext)
        {
            this.dbBankContext = dbBankContext;
        }

        [HttpGet]
        public IActionResult GetAllAccounts()
        {
            var accounts = dbBankContext.Accounts
                .Include(a => a.Customer)
                .ToList();

            if (accounts.Count == 0)
            {
                return NotFound("No Found Data");
            }

            return Ok(accounts);
        }

        [HttpGet]
        [Route("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetAccount(int id)
        {
            var accounts = dbBankContext.Accounts
                .Include(a => a.Customer)
                .FirstOrDefault(a => a.AccountID == id);
            if (accounts is null)
            {
                return NoContent();
            }

            return Ok(accounts);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult AddAccount(AccountModel addAccount)
        {

            try
            {
                // Check if the customer exists
                var customer = dbBankContext.Customers.Find(addAccount.CustomerID);
                if (customer == null)
                {
                    return BadRequest("Customer does not exist.");
                }

                var account = new AccountModel()
                {
                    AccountNumber = addAccount.AccountNumber,
                    AccountType = addAccount.AccountType,
                    Balance = addAccount.Balance,
                    CustomerID = addAccount.CustomerID
                };

                dbBankContext.Accounts.Add(account);
                dbBankContext.SaveChanges();

                // Optionally include customer details in the response
                account.Customer = customer;

                return CreatedAtAction(nameof(GetAccount), new { id = account.AccountID }, account);
            }
            catch (Exception ex)
            {
                // Log the detailed exception
                Console.WriteLine(ex.ToString());

                // Return a 500 Internal Server Error response
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request.");
            }
        }

        [HttpPut]
        [Route("{id:int}")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult UpdateAccount(int id, AccountModel updateAccount)
        {
            var account = dbBankContext.Accounts.Find(id);

            if (account is null)
            {
                return NotFound("Account Selected not Found");
            }

            account.AccountNumber = updateAccount.AccountNumber;
            account.AccountType = updateAccount.AccountType;
            account.Balance = updateAccount.Balance;
            account.CustomerID = updateAccount.CustomerID;

            dbBankContext.SaveChanges();

            return CreatedAtAction(nameof(GetAccount), account.AccountNumber + "is Successfuly Updated", account);
        }

        [HttpDelete]
        [Route("{id:int}")]
        public IActionResult DeleteAccount(int id)
        {
            var Account = dbBankContext.Accounts.Find(id);

            if (Account is null)
            {
                return NotFound("Selected Id Record Not Found");
            }

            dbBankContext.Accounts.Remove(Account);
            dbBankContext.SaveChanges();

            return Ok("Selected Account is Successfully Deleted");
        }
    }
}
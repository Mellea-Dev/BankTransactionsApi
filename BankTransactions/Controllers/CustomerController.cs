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
    public class CustomerController : ControllerBase
    {
        private readonly BankContext dbBankContext;
        public CustomerController(BankContext dbBankContext)
        {
            this.dbBankContext = dbBankContext;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetAllCustomers()
        {
            try
            {
                var allCustomers = dbBankContext.Customers
                    .Include(c => c.Accounts)
                    .ToList();

                return Ok(allCustomers);
            }
            catch (Exception ex)
            {
                // Log the exception (you can use a logging framework like Serilog or NLog)
                Console.WriteLine(ex.Message);

                // Return a 500 Internal Server Error response
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request.");
            }
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetCustomer(int id)
        {
            var Customers = dbBankContext.Customers
                .Include(c => c.Accounts)
                .FirstOrDefault(c => c.CustomerID == id);

            if (Customers is null)
            {
                return NotFound("No Data Exist");
            }

            return Ok(Customers);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult AddCustomer(CustomerModel addCustomer)
        {
            var Customer = new CustomerModel()
            {
                Fullname = addCustomer.Fullname,
                Email = addCustomer.Email,
                Phone = addCustomer.Phone,
                Address = addCustomer.Address,
            };
            dbBankContext.Customers.Add(Customer);
            dbBankContext.SaveChanges();

            return CreatedAtAction(nameof(GetCustomer), new { id = Customer.CustomerID }, Customer);
        }

        [HttpPut]
        [Route("{id:int}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status304NotModified)]
        public IActionResult UpdateCustomer(int id, CustomerModel updateCustomer)
        {
            var Customer = dbBankContext.Customers.Find(id);

            if (Customer is null)
            {
                return NotFound("Selected Id Record Not Found");
            }

            Customer.Fullname = updateCustomer.Fullname;
            Customer.Email = updateCustomer.Email;
            Customer.Phone = updateCustomer.Phone;
            Customer.Address = updateCustomer.Address;

            dbBankContext.SaveChanges();

            return Ok(Customer);
        }

        [HttpDelete]
        [Route("{id:int}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult DeleteCustomer(int id)
        {
            var Customer = dbBankContext.Customers.Find(id);

            if (Customer is null)
            {
                return NotFound("Selected Id Record Not Found");
            }

            dbBankContext.Customers.Remove(Customer);
            dbBankContext.SaveChanges();

            return Ok("Code: 200\n Message: Selected Id is Successfully Deleted");
        }
    }
}
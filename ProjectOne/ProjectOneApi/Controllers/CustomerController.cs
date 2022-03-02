using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectOneBL;
using ProjectOneModel;

namespace ProjectOneApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private ICustomerBL _custBL;
        public CustomerController(ICustomerBL c_custBL)
        {
            _custBL = c_custBL;
        }

        // GET: api/Customer
        [HttpGet("GetAllCustomers")]
        public async Task<IActionResult> GetAllCustomersAsync([FromQuery]string UserName, [FromQuery] string PassWord)
        {
            try
            {
                return Ok(await _custBL.GetAllCustomersAsync(UserName,PassWord));

            }
            catch (SqlException)
            {
                
                return NotFound();
            }
        }


        // GET: api/Customer/5
        [HttpGet("GetCustomer/{c_cate}/{name}")]
        public IActionResult GetCustomer(string c_cate,string name)
        {
            try
            {
                return Ok(_custBL.SearchCustomer(c_cate,name));
            }
            catch (System.Exception)
            {
                
                return NotFound();
            }
        }

        // GET: api/Customer/5
        [HttpGet("GetCustomerByID/{custID}")]
        public IActionResult GetCustomer(int custID)
        {
            try
            {
                return Ok(_custBL.SearchCustomer(custID));
            }
            catch (System.Exception)
            {
                
                return NotFound();
            }
        }

        // POST: api/Customer
        [HttpPost ("AddCustomer")]
        public IActionResult AddCustomer([FromBody] Cust c_cust)
        {
            try
            {
                return Created("Successfully created a Customer",_custBL.AddCust(c_cust));
            }
            catch (System.Exception)
            {
                
                return Conflict();
            }
        }

        // PUT: api/Customer/5
        [HttpPut("AuthorizeCustomer/{UserName}/{PassWord}/{CustomerID}")]
        public IActionResult Put(string UserName, string PassWord, int CustomerID)
        {
            try
            {
                return Accepted("Successfully updated a Customer",_custBL.GiveCustAuthentication(UserName, PassWord, CustomerID));
            }
            finally
            {
                Conflict();
            }
        }

        
    }
}

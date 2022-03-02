using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectOneBL;

namespace ProjectOneApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private ICustomerBL _custBL;
        public OrderController(ICustomerBL c_custBL)
        {
            _custBL = c_custBL;
        }
        // POST: api/Order
        [HttpPost("CreateNewOrder")]
        public IActionResult CreateNewOrder(int CustomerID, int StoreID)
        {
            try
            {
                return Created("Successfully created a new Order",_custBL.CreateOrder(CustomerID, StoreID));
            }
            catch (System.Exception)
            {
                
                return Conflict();
            }
        }
        // PUT: api/Order/5
        [HttpPut("AddToLatestOrder")]
        public IActionResult AddToLatestOrder(int StoreItemID, int Amount)
        {
            try
            {
                return Accepted(_custBL.AddToOrder(StoreItemID, Math.Abs(Amount)));
            }
            catch (System.Exception)
            {
                
                throw new Exception("Unable to add to order");
            }
        }
        // GET: api/Order
        [HttpGet("GetOrderByCustomerID")]
        public IActionResult GetOrdersByCustomerID(int CustomerID)
        {
            
            try
            {
                return Ok(_custBL.GetAllOrdersByCustomerID(CustomerID));
            }
            catch (System.Exception)
            {
                
                throw new Exception("No User Found");
            }
        }

        [HttpGet("GetOrderByStoreID")]
        public IActionResult GetOrdersByStoreID(int StoreID)
        {
            
            try
            {
                return Ok(_custBL.GetAllOrdersByStoreID(StoreID));
            }
            catch (System.Exception)
            {
                
                throw new Exception("No User Found");
            }
        }
        
    }
}

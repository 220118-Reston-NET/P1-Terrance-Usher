using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectOneBL;

namespace ProjectOneApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StoreController : ControllerBase
    {
        private ICustomerBL _custBL;
        public StoreController(ICustomerBL c_custBL)
        {
            _custBL = c_custBL;
        }
        // GET: api/Store
        [HttpGet("GetAllStores")]
        public IActionResult GetAllStores()
        {
            try
            {
                return Ok(_custBL.GetAllStores());

            }
            catch (SqlException)
            {
                
                return NotFound();
            }
        }

        // GET: api/Store/5
        [HttpGet("GetStoreByID/{storeID}")]
        public IActionResult GetStoreByName(int storeID)
        {
            try
            {
                return Ok(_custBL.SearchStores(storeID));
            }
            catch (System.Exception)
            {
                
                return NotFound();
            }
        }

        [HttpGet("GetStoreInventory")]
        public IActionResult GetStoreInventory(int storeID)
        {
            try
            {
                return Ok(_custBL.GetStoreInv(storeID));
            }
            catch (System.Exception)
            {
                
                throw;
            }
        }

        // PUT: api/Store/5
        [HttpPut("IncreaseStoreInventory")]
        public IActionResult IncreaseStoreInventory(string UserName, string PassWord, int StoreItemID, int Amount)
        {
            try
            {
                return Accepted(_custBL.ChangeInvQuantity(UserName,PassWord,StoreItemID,Amount));
            }
            catch (System.Exception)
            {
                
                throw;
            }
        }

        // DELETE: api/Store/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}

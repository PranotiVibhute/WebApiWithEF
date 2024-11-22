using Azure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using WebApiWithEF.Models;

namespace WebApiWithEF.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly EcommerceDbContext _dbContext;

        public CustomerController(EcommerceDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        [HttpGet]
        public IEnumerable<Customer> GetAll()
        {
            return _dbContext.Customers.ToList();
        }
        [HttpGet("{id}")]
        public IEnumerable<object> Get(int id)
        {
            return _dbContext.Customers.Where(obj => obj.CustomerId == id);
        }
        [HttpPost ("PostCustomer")]
        public ActionResult<Customer> PostCustomer([FromBody] Customer customer)//Body madhe content pass karto.
        {
            _dbContext.Customers.Add(customer);
            _dbContext.SaveChanges();
            return Ok();
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var Customer = _dbContext.Customers.FirstOrDefault(c => c.CustomerId == id);
            if (Customer == null)
            {
                return NotFound();
            }
            _dbContext.Customers.Remove(Customer);
            _dbContext.SaveChanges();
            return NoContent();
        }
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateCustomer(int id, Customer updatecustomer)
        {
            if (id != updatecustomer.CustomerId)
            {
                return BadRequest("Id is Mismatch");
            }
            var customer = await _dbContext.Customers.FindAsync(id);

            if (customer == null)
            {
                return BadRequest("customer is NotFound");
            }
            customer.CustomerName = updatecustomer.CustomerName;
            customer.CustomerEmail = updatecustomer.CustomerEmail;
            customer.City = updatecustomer.City;
            await _dbContext.SaveChangesAsync();
            return NoContent();
        }
        [HttpPatch("{id}")]
        public async Task<IActionResult> PartialUpdateCustomer(int id, [FromBody]
        JsonPatchDocument <Customer> PatchDoc)
        {
            if (PatchDoc == null)
                return BadRequest("Id is null");
            var customer = await _dbContext.Customers.FindAsync(id);
            if (customer == null)
            {
                return BadRequest("Customer is null");
            }
            await _dbContext.SaveChangesAsync();
            return NoContent();
        }
        public void UpdateData()
        {
            //Update a content;
        }
    }
}

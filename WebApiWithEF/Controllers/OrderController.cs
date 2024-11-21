using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApiWithEF.Models;

namespace WebApiWithEF.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly EcommerceDbContext _dbContext;

        public OrderController(EcommerceDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        [HttpGet]
        public IEnumerable<Order> Getall()
        {
            return _dbContext.Orders.ToList();
        }
        [HttpGet("{id}")]
        public IEnumerable<Order>Get(int id)
        {
            return _dbContext.Orders.Where(obj=>obj.OrderId == id).ToList();
        }
        [HttpPost("PostOrder")]
        public ActionResult<Order> PostOrder([FromBody] Order order)
        {
            _dbContext.Orders.Add(order);
            _dbContext.SaveChanges();
            return Ok();
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var Order = _dbContext.Orders.FirstOrDefault(obj => obj.OrderId == id);
            if (Order == null)
            {
                return NoContent();
            }
            _dbContext.Orders.Remove(Order);
            _dbContext.SaveChanges();
            return Ok();
        }
    }
}

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApiWithEF.Models;

namespace WebApiWithEF.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly EcommerceDbContext _dbContext;

        public ProductController(EcommerceDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        [HttpGet]
        public IEnumerable<Product> GetProducts()
        {
            return _dbContext.Products.ToList();
        }
        [HttpGet("{id}")]
        public IEnumerable<Product>Get(int id)
        {
            return _dbContext.Products.Where(obj=>obj.ProductId == id).ToList();
        }
        [HttpPost("PostProduct")]
        public ActionResult<Product> PostProduct([FromBody] Product product)
        {
            _dbContext.Products.Add(product);
            _dbContext.SaveChanges();
            return Ok();
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteProduct(int id)
        {
            var product = _dbContext.Products.FirstOrDefault(obj => obj.ProductId == id);
            if (product == null)
            {
                return NotFound();
            }
            _dbContext.Products.Remove(product);
            _dbContext.SaveChanges();
            return Ok();
        }
        public void getdata()
        {
            //to do
        }
    }
}

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApiWithEF.Models;

namespace WebApiWithEF.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SupplierController : ControllerBase
    {
        private readonly  EcommerceDbContext _dbContext;

        public SupplierController(EcommerceDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        [HttpGet]
        public IEnumerable<Supplier> GetAll()
        {
            return _dbContext.Suppliers;
        }

        [HttpGet("{id}")]
        public IEnumerable<Supplier> Get(int id)
        {
            return _dbContext.Suppliers.Where(obj => obj.SupplierId == id);
        }
        [HttpGet("{id}/{name}")]
        public IEnumerable<Supplier>Get(int id,string name )
        {
            return _dbContext.Suppliers.Where(obj => obj.SupplierId == id &&
            obj.SupplierName == name).ToList();
        }
        [HttpPost("PostSupplier")]
        public ActionResult<Supplier> PostSupplier([FromBody] Supplier supplier)
        {
            _dbContext.Suppliers.Add(supplier);
            _dbContext.SaveChanges();
            return Ok();
        }
        [HttpDelete("{id}")]
        public ActionResult DeleteSupplier(int id)
        {
            var Supplier = _dbContext.Suppliers.FirstOrDefault(obj => obj.SupplierId == id);
            if (Supplier == null)
            {
                return NotFound();
            }
            _dbContext.Suppliers.Remove(Supplier);
            _dbContext.SaveChanges();
            return NoContent();
        }
        [HttpPut("{id}")]
        public async Task<ActionResult>UpdateSupplier(int id, Supplier supplier)
        {
            return Ok();
        }

    }
}

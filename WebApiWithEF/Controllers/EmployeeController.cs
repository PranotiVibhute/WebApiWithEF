using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using WebApiWithEF.Models;

// For more information on enabling Web API for empty projects,
// visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApiWithEF.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly EcommerceDbContext _dbContext;

        public EmployeeController(EcommerceDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        [HttpGet(Name = "GetEmployee")]
        public IEnumerable<Employee> GetAll() 
        //only Support 200 result [HttpGet] return a collection.
        {
            return _dbContext.Employees.ToList();
        }
        // GET: api/<EmployeeController>
        [HttpGet("{id}")]
        public ActionResult<Employee> Get(int id)
        {
            var employee=_dbContext.Employees.FirstOrDefault(obj => obj.EmployeeId == id);
           
            if (employee == null)
            {
                return NotFound("Record Not Found");
            }
            return employee;
            }

        [HttpPost("PostEmployee")]
        public ActionResult<Employee> PostEmployee([FromBody] Employee employee)//Body madhe content pass karto.
        {
            _dbContext.Employees.Add(employee);//add data in a object.
            _dbContext.SaveChanges();//change it database.
            return Ok();
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var employee = _dbContext.Employees.FirstOrDefault(e => e.EmployeeId == id);
            if (employee == null)
            {
                return NotFound();
            }
            _dbContext.Employees.Remove(employee);
            _dbContext.SaveChanges();
            return NoContent();

        }


    }
    }

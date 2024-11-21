using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApiWithEF.Models;

namespace WebApiWithEF.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly EcommerceDbContext _dbContext;

        public DepartmentController(EcommerceDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        [HttpGet]
        public IEnumerable<Department> GetAll()
        {
            return _dbContext.Departments.ToList();
        }
        [HttpGet("{id}")]
        public IEnumerable<Department> Get(int id)
        {
            return _dbContext.Departments.Where(obj => obj.DepartmentId == id);
        }

        [HttpPost("PostDepartment")]
        public ActionResult<Department> PostDepartment([FromBody] Department department)
        {
            _dbContext.Departments.Add(department);
            _dbContext.SaveChanges();
            return Ok();
        }
        [HttpDelete("{id}")]
        public ActionResult DeleteDepartment(int id)
        {
            var Department = _dbContext.Departments.FirstOrDefault(d => d.DepartmentId == id);
            if (Department != null)
            {
                _dbContext.Departments.Remove(Department);
                _dbContext.SaveChanges();
                return Ok();
            }
            return NotFound();
        }


    }

}

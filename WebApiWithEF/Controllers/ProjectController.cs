using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApiWithEF.Models;

namespace WebApiWithEF.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private readonly EcommerceDbContext _dbContext;

        public ProjectController(EcommerceDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        [HttpGet]
        public IEnumerable<Project>GetAll()
        {
            return _dbContext.Projects.ToList();
        }
        [HttpGet("{id}")]
        public IEnumerable<Project> Get(int id)
        {
            return _dbContext.Projects.Where(obj => obj.ProjectId == id);
        }
        [HttpPost("PostProject")]
        public ActionResult PostProject([FromBody] Project project)
        {
            _dbContext.Projects.Add(project);
            _dbContext.SaveChanges();
            return Ok();
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var Project = _dbContext.Projects.FirstOrDefault(obj => obj.ProjectId == id);
            if (Project == null)
            {
                return NotFound();
            }
            _dbContext.Projects.Remove(Project);
            _dbContext.SaveChanges();
            return Ok();
        }
    }
}

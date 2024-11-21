using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using WebApiWithEF.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApiWithEF.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class categoryController : ControllerBase
    {
        private readonly EcommerceDbContext _dbContext;

        public categoryController(EcommerceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public IEnumerable<Category> Get()
        {
            return _dbContext.Categories;
        }
        // GET: api/<categoryController>
        [HttpGet("{id},{name}")]
        public IEnumerable<Category> Get(int id,string name)
        {
            return _dbContext.Categories.Where(obj => obj.CategoryId == id &&
            obj.CategoryName==name);
        }
        [HttpPost("PostCategory")]
        public ActionResult<Category> PostCategory([FromBody] Category category)
        {
            _dbContext.Categories.Add(category);//Add data in a table
            _dbContext.SaveChanges();
            return Ok();
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteCategory(int id)
        {
            var category = _dbContext.Categories.FirstOrDefault(obj => obj.CategoryId == id);
            if (category == null)
            {
                return NotFound();
            }
            _dbContext.Categories.Remove(category);
            _dbContext.SaveChanges();
            return NoContent();
        }
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateCategory
            (int id, Category updatecategory)
        {
            if (id != updatecategory.CategoryId)
                return BadRequest("Id is Mismatch");
            var category = await _dbContext.Categories.FindAsync(id);
            if (category == null)
                return NotFound("id is null");
            category.CategoryName = updatecategory.CategoryName;
            await _dbContext.SaveChangesAsync();
            return NoContent();
        }
        [HttpPatch("{id}")]
        public async Task<IActionResult> PartialUpdateCategory(int id, [FromBody]
            JsonPatchDocument<Category> PatchDoc)
        {
            if (PatchDoc == null)
                return BadRequest("Cannot Be null");

            var category = await _dbContext.Categories.FindAsync(id);
            if (category == null)
                return NotFound("id is null");
            await _dbContext.SaveChangesAsync();
            return NoContent();
        }
    }
    
}

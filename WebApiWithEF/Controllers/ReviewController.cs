using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApiWithEF.Models;

namespace WebApiWithEF.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        private readonly EcommerceDbContext _dbContext;

        public ReviewController(EcommerceDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        [HttpGet]
        public IEnumerable<Review> GetAll()
        {
            return _dbContext.Reviews.ToList();
        }
        [HttpGet("{id}")]
        public IEnumerable<Review> GetAll(int id)
        {
            return _dbContext.Reviews.Where(obj => obj.ReviewId == id);
        }
        [HttpPost("PostReview")]
        public ActionResult<Review> PostReview([FromBody] Review review)
        {
            _dbContext.Reviews.Add(review);
            return Ok();
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var Review = _dbContext.Reviews.FirstOrDefault(obj => obj.ReviewId == id);
            if (Review == null)
            {
                return NotFound();
            }
            _dbContext.Reviews.Remove(Review);
            _dbContext.SaveChanges();
            return Ok();
        }
        
    }    

}

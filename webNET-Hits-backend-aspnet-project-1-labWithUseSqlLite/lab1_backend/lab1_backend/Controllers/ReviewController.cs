using lab1_backend.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace lab1_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : ControllerBase
    {

        private readonly MovieDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public ReviewController(MovieDbContext context, UserManager<IdentityUser> genre)
        {
            _userManager = genre;
            _context = context;

        }
        [HttpPost("add")]
        public async Task<IActionResult> AddReview(string movieId, ReviewModel review)
        {
            var InDbMovieId = (from i in _context.Movie.Where(p => p.Id == movieId) select i.Id).FirstOrDefault();
            if (InDbMovieId != null)
            {
                await _context.ReviewModel.AddAsync(review);
                await _context.SaveChangesAsync();
            }
            else
            {
                return BadRequest("Фильм не найден");
            }

            return Ok();
        }

        [HttpPut("edit")]
        public async Task<IActionResult> EditReview(string movieId, string reviewId, ReviewModel review)
        {
            var InDbMovieId = (from i in _context.Movie.Where(p => p.Id == movieId) select i.Id).FirstOrDefault();
            if (InDbMovieId != null)
            {
                var InDbReviewId = _context.ReviewModel.Where(p => p.Id == reviewId).FirstOrDefault();
                _context.ReviewModel.Remove(InDbReviewId);
                await _context.SaveChangesAsync();
                await _context.ReviewModel.AddAsync(review);
                await _context.SaveChangesAsync();
            }
            else
            {
                return BadRequest("Фильм не найден");
            }

            return Ok();
        }

        [HttpDelete("Delete")]
        public async Task<IActionResult> DeleteReview(string movieId, string reviewId)
        {
            var InDbMovieId = (from i in _context.Movie.Where(p => p.Id == movieId) select i.Id).FirstOrDefault();
            
            if (InDbMovieId != null)
            {
                var InDbReviewId = _context.ReviewModel.Where(p => p.Id == reviewId).FirstOrDefault();
                _context.ReviewModel.Remove(InDbReviewId);
                await _context.SaveChangesAsync();
            }
            else
            {
                return BadRequest("Фильм не найден");
            }

            return Ok();
        }
    }
}

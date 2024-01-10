using lab1_backend.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace lab1_backend.Controllers
{
    [ApiController]
    [Route("api")]
    public class FavoriteMovieController : Controller
    {

        private readonly MovieDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        // private readonly SignInManager<IdentityUser> _signInManager;
        // private readonly JWTSettings _options;

        public FavoriteMovieController(MovieDbContext context, UserManager<IdentityUser> genre) // конструктор для приведения _context к актуальному состоянию
        {
            _userManager = genre;
            _context = context;

        }

       [HttpPost("favorites/id/add")]
        public async Task AddNewFavoriteMovie(FavoriteMovies movieIn)
        {
            //  var userId = _userManager.FindByIdAsync(User.Identity.Id);
            var userName = User.Identity.Name;
            movieIn.UserName = userName;
           // var finalCut = 
            await _context.FavoriteMovies.AddAsync(movieIn);
            await _context.SaveChangesAsync();

        }

        [HttpGet("favorites")]
        public async Task<IActionResult> GetFavoriteMovie()
        {
            var userName = User.Identity.Name;
          //  movieIn.UserName = userName;
          // while()
            var favoritesMovieId = _context.FavoriteMovies.FirstOrDefault(x => x.UserName == userName);
            
            var allFavoritesOfUser = _context.FavoriteMovies.Where(p => p.UserName == userName);
            var favoriteMovieId = (from s in _context.FavoriteMovies.Where(p => p.UserName == userName) select s.MovieId).ToList();
            var movieId = (from i in _context.Movie.Where(p  => p.name != null) select i.Id).ToList();
            var countFavorites = (from s in _context.FavoriteMovies.Where(p => p.UserName == userName) select s.MovieId).Count();
            var countMovies = _context.Movie.Count();
            var list_movie = new List<Movie>();
            var movi_temp = _context.Movie.Include(x => x.Genres).ToList();

            var firstDef = (from s in _context.FavoriteMovies.Where(p => p.UserName == userName) select s.MovieId).FirstOrDefault();

            for(int q = 0; q < countMovies ; q++)
            {
                for(int i = 0; i < countFavorites; i++)
                {
                    if(favoriteMovieId.ElementAt(i).Equals(movieId.ElementAt(q)))
                    {
                        list_movie.Add(movi_temp.ElementAt(q));
                    }
                }
            }


            /* for (var i = 1; i <= countF; i++)
             {
                 if ()
                   FavoriteMovieId.ElementAt(i);
                 list_movie.Add(movi_temp.ElementAt(i));
             }
             return Ok(list_movie);


               while (countF > 0)
                 {

                   countF--;
                 } */
            return Ok(list_movie);
            
        }
        // осталось разобраться только с тем, чтобы при удалении еще и учитывать имя пользователя ( в совсем крайнем случае решить можно циклом
        [HttpDelete]
        public async Task<IActionResult> DeleteFavoriteMovie(string id)
        {

            var userName = User.Identity.Name;
           // var favoriteMovie = _context.FavoriteMovies.Where(p => p.UserName== userName).FirstOrDefault();
            var favoriteMovie = _context.FavoriteMovies.Where((p => p.MovieId == id)).FirstOrDefault();
            _context.FavoriteMovies.Remove(favoriteMovie);
            await _context.SaveChangesAsync();

            return Ok("Легчайшее удаление");


        }
       
    }
}

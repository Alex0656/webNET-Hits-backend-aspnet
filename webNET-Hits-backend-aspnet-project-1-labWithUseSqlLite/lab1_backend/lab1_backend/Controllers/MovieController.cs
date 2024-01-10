using lab1_backend.Models;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PagedList;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Diagnostics.Contracts;
using System.Reflection.Metadata;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore.Query;
using Optivem.Framework.Core.Domain;
using Newtonsoft.Json;

namespace lab1_backend.Controllers
{
    [ApiController]
    [Route("api/movies")]
    public class MovieController : Controller
    {
        private readonly MovieDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
     // private readonly SignInManager<IdentityUser> _signInManager;
     // private readonly JWTSettings _options;

        public MovieController(MovieDbContext context, UserManager<IdentityUser> genre) // конструктор для приведения _context к актуальному состоянию
        {
            _userManager = genre;
            _context = context;

        }

     /*   public AuthController(UserManager<IdentityUser> user, SignInManager<IdentityUser> signIn, IOptions<JWTSettings> optAccess) // конструктор для приведения _context к актуальному состоянию
        {
            _userManager = user;
            _signInManager = signIn;
            _options = optAccess.Value;

        }
     */

        [HttpPost("add/movie")]
        public async Task AddNewMovie(AddMovieModel movieIn)
        {
            List<Genres> listGenres = _context.Genres.Where(p => movieIn.genresId.Contains(p.id)).ToList();
            Movie movie = new Movie()
            {
                Id = Guid.NewGuid().ToString(),
                name = movieIn.name,
                ageLimit = movieIn.ageLimit,
                budget = movieIn.budget,
                country = movieIn.country,
                description = movieIn.description,
                director = movieIn.director,
                fees = movieIn.fees,
                poster = movieIn.poster,
                tagline = movieIn.tagline,
                time = movieIn.time,
                year = movieIn.year,
                Genres = listGenres
                
                
            };

            await _context.Movie.AddAsync(movie);
            await _context.SaveChangesAsync();

        }


        [HttpGet("GetGenres")]
        public async Task<IActionResult> GetGenres()
        {
            List<Genres> genres = _context.Genres.ToList();
            List<GenresToMovies> genresToMovies = genres.Select(p => new GenresToMovies { Id = p.id, Name = p.name }).ToList();
            //return Ok(JsonSerializer.Serialize(genresToMovies));
            return Ok(GenresToMovies.ToJson(genresToMovies));
        }



        [HttpPost("add/Genre")]
        public async Task AddNewGenre(GenreModel genreIn)
        {
            Genres genresNew = new Genres();
            genresNew.name = genreIn.Name;
            genresNew.id = Guid.NewGuid().ToString();
            await _context.Genres.AddAsync(genresNew);
            await _context.SaveChangesAsync();


            int i = 0;
            

        }


        


        [HttpGet("movies/{page}")]
        public async Task<IActionResult> GetMoviePage(int page) //List<Movie> 
        {
            AllMovies allMovies = new AllMovies();

            PageInfo pageInfo = new PageInfo();

            pageInfo.pageSize = 6;

            int movieCount = _context.Movie.Count();

            //  int pageCount = sum_page;
            // list_movie.Add(pageSize);
            int pageCount =(int)Math.Ceiling((double)movieCount / pageInfo.pageSize);

            if (pageCount < page || page <= 0)
            {
                return BadRequest("Недопустимый номер страницы");
            }
            pageInfo.currentPage = page;
            pageInfo.pageCount = pageCount;

            var list_movie = new List<MovieGetByIdMeth>();
            var movi_temp = _context.Movie.Include(x => x.Genres).Skip(pageInfo.pageSize * (page - 1)).Take(pageInfo.pageSize).ToList(); //Здесь мы пропускаем мувики на страницах до той, которую хотим получить, а затем берем следующие шесть

            

            foreach (var movie in movi_temp)
            {
                list_movie.Add(new MovieGetByIdMeth()
                {
                    id = movie.Id,
                    name = movie.name,
                    ageLimit = movie.ageLimit,
                    budget = movie.budget,
                    country = movie.country,
                    description = movie.description,
                    director = movie.director,
                    fees = movie.fees,
                    poster = movie.poster,
                    tagline = movie.tagline,
                    time = movie.time,
                    year = movie.year,
                    Genres = movie.Genres.Select(p => new GenresToMovies { Id = p.id, Name = p.name }).ToList()


                });
            }
            allMovies.Movies = list_movie;
            allMovies.pageInfo = pageInfo;
            return Ok(allMovies);

           /* for (var q = 1; q <= sum2; q++)
            {
                if (q == page)
                {
                    for (var i = 1; i <= 6; i++)
                    {
                        _index = (page * 6) - i;
                        list_movie.Add(movi_temp.ElementAt(_index));
                    }
                    return Ok(list_movie);
                }
            }

            if (page == sum2 + 1)
            {
                int temp = movi_temp.Count() - sum2 * 6;

                for (var i = 0; i < temp; i++)
                {
                    _index = (sum_page * 6) + i;
                    list_movie.Add(movi_temp.ElementAt(_index));
                }
                return Ok(list_movie);
            }
            else
            {
                return Ok();
            }
           */
        }


        /*
        [HttpGet("details/movies/page_THIS")]
        public async Task<IActionResult> GetMoviePageExperements(int page) //List<Movie> 
        {

            var movies = _context.Movie.Include(x => x.Genres);

            var sum_page = movies.Count() / 6;
            var sum2 = sum_page;
            var list_movie = new List<Movie>();
            int _index = 0;
            var movi_temp = _context.Movie.Include(x => x.Genres).ToList();

            int pageSize = 6;
            int pageCount = sum_page;
            // list_movie.Add(pageSize);




            for (var q = 1; q <= sum2; q++)
            {
                if (q == page)
                {
                    for (var i = 1; i <= 6; i++)
                    {
                        _index = (page * 6) - i;
                        list_movie.Add(movi_temp.ElementAt(_index));
                    }
                    return Ok(list_movie);
                }
            }

            if (page == sum2 + 1)
            {
                int temp = movi_temp.Count() - sum2 * 6;

                for (var i = 0; i < temp; i++)
                {
                    _index = (sum_page * 6) + i;
                    list_movie.Add(movi_temp.ElementAt(_index));
                }
                return Ok(list_movie);
            }
            else
            {
                return Ok();
            }
        }
        */
       







        [HttpGet]
        [Route("details/{id}")]// жанры не сереализуються
        public MovieGetByIdMeth GetMovieByIdMeth([FromRoute]string id)
        {
            

            var movie = _context.Movie
                 .Include(x => x.Genres)
                 .FirstOrDefault(x => x.Id == id);


           MovieGetByIdMeth movieGetByIdMeth = new MovieGetByIdMeth()
              {
                  id = movie.Id,
                  name = movie.name,
                  ageLimit = movie.ageLimit,
                  budget = movie.budget,
                  country = movie.country,
                  description = movie.description,
                  director = movie.director,
                  fees = movie.fees,
                  poster = movie.poster,
                  tagline = movie.tagline,
                  time = movie.time,
                  year = movie.year,
                  Genres = movie.Genres.Select(p => new GenresToMovies { Id = p.id, Name = p.name }).ToList()


               };
            return movieGetByIdMeth;
        }
  

    }
}

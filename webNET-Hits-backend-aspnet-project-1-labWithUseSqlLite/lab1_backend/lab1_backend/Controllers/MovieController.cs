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
        public MovieController(MovieDbContext context, UserManager<IdentityUser> genre) 
        {
            _userManager = genre;
            _context = context;

        }

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
        public async Task<IActionResult> GetMoviePage(int page)
        {
            AllMovies allMovies = new AllMovies();

            PageInfo pageInfo = new PageInfo();

            pageInfo.pageSize = 6;

            int movieCount = _context.Movie.Count();

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

        [HttpGet]
        [Route("details/{id}")]
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

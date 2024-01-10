using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace lab1_backend.Models
{
    public class MovieDbContext : DbContext
    {
        public DbSet<Movie> Movie { get; set; }
        public DbSet<Genres> Genres{ get; set; }
        public DbSet<FavoriteMovies> FavoriteMovies { get; set; }
        public DbSet<ReviewModel> ReviewModel { get; set; }



        public MovieDbContext(DbContextOptions<MovieDbContext> options)
            : base(options)
        {
          //  Console.WriteLine("###################### DbContext = {0}, мой второй параметр = {1} #############################", options.ToString(), "второй параметр");
            Database.EnsureCreated();
        }
    }
  
}



/*
 
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace lab1_backend.Models
{
    public class GenresDbContext : DbContext
    {
        public DbSet<Genres> Genres{ get; set; }
        public GenresDbContext(DbContextOptions<GenresDbContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
    }
  
* 
 * 
 * 
 
 */
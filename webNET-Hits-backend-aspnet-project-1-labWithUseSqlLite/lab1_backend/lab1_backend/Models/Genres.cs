using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace lab1_backend.Models
{
    public class Genres
    {
        public List<Movie> Movies { get; set; }
        [Required]
        public string id { get; set; }
        [Required]
        public string name { get; set; }
    }
}

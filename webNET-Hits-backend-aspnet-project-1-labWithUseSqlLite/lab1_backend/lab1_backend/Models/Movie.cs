using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace lab1_backend.Models
{
    public class Movie
    {
        [Required]
        public string Id { get; set; }
        [Required]
        public string name { get; set; }
        [Required]
        [MaybeNull]
        public string poster { get; set; }
        [Required]
        public int year { get; set; }
        [Required]
        [MaybeNull]
        public string country { get; set; }
        [Required]
        public List<Genres> Genres { get; set; } //
        [MaybeNull]
        public ReviewModel? Reviews { get; set; } //
        [Required]
        public int time { get; set; }
        [Required]
        public string tagline { get; set; }
        [Required]
        public string description { get; set; }
        [Required]
        public string director { get; set; }
        [Required]
        public int budget { get; set; }
        [Required]
        public int fees { get; set; }
        [Required]
        public int ageLimit { get; set; }
    }
}

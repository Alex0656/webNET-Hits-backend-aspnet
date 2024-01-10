using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace lab1_backend.Models
{
    
    public class FavoriteMovies
    {
        [Key]
        [MaybeNull]
        public string? Id { get; set; }
        
        [MaybeNull]
        public string? UserName { get; set; }
        [MaybeNull]
        public string? MovieId { get; set; }

    }
}

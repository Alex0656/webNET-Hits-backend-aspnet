using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace lab1_backend.Models
{
    public class AddMovieModel
    {

        public string name { get; set; }

        public string poster { get; set; }

        public int year { get; set; }

        public string country { get; set; }

        public List<string> genresId { get; set; }


        

        public int time { get; set; }

        public string tagline { get; set; }

        public string description { get; set; }

        public string director { get; set; }

        public int budget { get; set; }

        public int fees { get; set; }

        public int ageLimit { get; set; }
    }
}

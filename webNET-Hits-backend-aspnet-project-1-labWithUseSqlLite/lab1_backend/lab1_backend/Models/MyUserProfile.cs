using System.ComponentModel.DataAnnotations;

namespace lab1_backend.Models
{
    public class MyUserProfile
    {
        public string Id { get; set; }
        public string Email { get; set; }

        public string Name { get; set; }

        public string BirthDate { get; set; } 

        public string IsAdmin { get; set; } 

        public string Gender { get; set; }

        public string NickName { get; set; }

        public string Avatar { get; set; }
    }
}

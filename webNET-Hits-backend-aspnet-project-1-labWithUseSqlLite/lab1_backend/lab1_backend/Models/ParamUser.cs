using System.ComponentModel.DataAnnotations;

namespace lab1_backend.Models
{
    public class ParamUser
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

    }
}

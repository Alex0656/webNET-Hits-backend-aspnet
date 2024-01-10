using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace lab1_backend.Models
{
    public class OtherParamUser : ParamUser
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }


        public string Name { get; set; }


        public DateTime BirthDate { get; set; } = DateTime.UtcNow;

        [Required]
        public bool IsAdmin { get; set; } = false;

        [Required]
        public EnumGender Gender { get; set; }

    }
}

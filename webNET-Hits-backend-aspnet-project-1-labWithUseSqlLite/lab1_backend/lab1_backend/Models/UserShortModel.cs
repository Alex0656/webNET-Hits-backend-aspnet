using System.ComponentModel.DataAnnotations;

namespace lab1_backend.Models
{
    public class UserShortModel : OtherParamUser
    {
        public string id { get; set; }
        public string NickName { get; set; }
       
        public string Avatar { get; set; }
  
    }
}

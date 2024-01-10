using Microsoft.IdentityModel.Tokens;

namespace lab1_backend.Models
{
    public class CustomLifeTime
    {
        static public bool CustomLifetimeValidator(DateTime? notBefore, DateTime? expires,
                                                   SecurityToken tokenToValidate, TokenValidationParameters @param)
        {
            if(expires != null)
            {
                return expires > DateTime.UtcNow;
            }
            return false;
        }
    }
}

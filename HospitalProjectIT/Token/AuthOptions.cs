using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace HospitalProjectIT.Token
{
    public class AuthOptions
    {
        public const string ISSUER = "HospitalProjectIT";
        public const string AUDIENCE = "Hospital";
        const string KEY = "mysupersecret_secretkey!123";
        public const int LIFETIME = 10;
        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(KEY));
        }
    }
}

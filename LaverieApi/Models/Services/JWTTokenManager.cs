using LaverieDomain;
using System.Security.Claims;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using LaverieApi.Infrastructure.DAO;
using LaverieApi.Models.IDAO;

namespace LaverieApi.Models.Services
{
    public class JWTTokenManager
    {
        private JwtSecurityTokenHandler _jwtSecurityTokenHandler;
        private byte[] _key;
        private readonly ProprietaireDAOImp _proprietaireDAO;
        public JWTTokenManager()
        {
            _jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            _key = Encoding.ASCII.GetBytes("12345678901234567890123456789012");
            _proprietaireDAO = new ProprietaireDAOImp();
        }

        public bool Authenticate(string email, string pwd, out Proprietaire proprietaire)
        {
            proprietaire = _proprietaireDAO.GetProprietaireByEmail(email);
            if (proprietaire == null)
                return false;

            string hashedPassword = proprietaire.Password;
            bool isPasswordValid = BCrypt.Net.BCrypt.Verify(pwd, hashedPassword);

            return isPasswordValid;
        }



        //public bool Authenticate(string email, string pwd)
        //{

        //       Proprietaire proprietaire = _proprietaireDAO.GetProprietaireByEmail(email);
        //        string Email = proprietaire.Email;
        //        string Hashed = proprietaire.Password;

        //        string verifpwd = pwd;

        //    bool isPasswordValid = BCrypt.Net.BCrypt.Verify(verifpwd, Hashed);

        //        if (email == Email && isPasswordValid)
        //            return true;
        //        return false;

        //}

        //public string NewToken()
        //{
        //    var _key = Encoding.ASCII.GetBytes("votre_clé_secrète_256_bits_votre_clé_secrète_256_bits");

        //    var tokenDescriptor = new SecurityTokenDescriptor
        //    {
        //        Subject = new ClaimsIdentity(new Claim[] { new Claim(ClaimTypes.Name, "Mohamed") }),
        //        Expires = DateTime.UtcNow.AddMinutes(10),
        //        SigningCredentials = new SigningCredentials(
        //            new SymmetricSecurityKey(_key),
        //            SecurityAlgorithms.HmacSha256Signature)
        //    };

        //    var _jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
        //    var token = _jwtSecurityTokenHandler.CreateToken(tokenDescriptor);
        //    var tokenString = _jwtSecurityTokenHandler.WriteToken(token);

        //    return tokenString;
        //}
        public string NewToken(Proprietaire proprietaire)
        {
            var _key = Encoding.ASCII.GetBytes("votre_clé_secrète_256_bits_votre_clé_secrète_256_bits");

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
            new Claim(ClaimTypes.NameIdentifier, proprietaire.Id.ToString()), // Ajouter l'ID
            new Claim(ClaimTypes.Email, proprietaire.Email), // Ajouter l'email
            new Claim(ClaimTypes.Name, proprietaire.Nom) // Ajouter d'autres données si nécessaire
        }),
                Expires = DateTime.UtcNow.AddMinutes(10),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(_key),
                    SecurityAlgorithms.HmacSha256Signature)
            };

            var _jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            var token = _jwtSecurityTokenHandler.CreateToken(tokenDescriptor);
            return _jwtSecurityTokenHandler.WriteToken(token);
        }



        public ClaimsPrincipal VerifyToken(string token)
        {
            var _key = Encoding.ASCII.GetBytes("votre_clé_secrète_256_bits_votre_clé_secrète_256_bits");
            var tokenHandler = new JwtSecurityTokenHandler();

            try
            {
                var principal = tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    IssuerSigningKey = new SymmetricSecurityKey(_key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                return principal;
            }
            catch (Exception ex)
            {
                throw new Exception("Token validation failed: " + ex.Message);
            }
        }

        
       
    }
}

using Magic_Villa_VillaAPI.Data;
using Magic_Villa_VillaAPI.Models;
using Magic_Villa_VillaAPI.Models.DTO;
using Magic_Villa_VillaAPI.Repository.IRepositpory;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Magic_Villa_VillaAPI.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicatonDbContext _db;
        private string secretKey;
        public UserRepository(ApplicatonDbContext db , IConfiguration configuration) { 
            _db = db;
            secretKey = configuration.GetValue<string>("ApiSettings:Secret");
        }

        public bool IsUniqueUser(string username)
        {
           var user = _db.LocalUsers.FirstOrDefault(z=>z.UserName==username);
            if (user == null)
            {
                return true;
            }
            return false;
        }

        public async Task<LoginResponseDTO> Login(LoginRequestDTO loginRequestDTO)
        {
            var user = _db.LocalUsers.FirstOrDefault(u=>u.UserName.ToLower()==loginRequestDTO.Username.ToLower()
            &&u.Password==loginRequestDTO.Password
            );
            if (user == null)
            {
                return new LoginResponseDTO()
                {
                    Token="",
                    User=null,
                };
            }
            //if user was found generate JWT Token 
            var tokenHanddler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(secretKey);
            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString()),
                    new Claim(ClaimTypes.Role, user.Role)
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
            };
            var token = tokenHanddler.CreateToken(tokenDescriptor);
            LoginResponseDTO loginResponseDTO = new LoginResponseDTO()
            {
                Token= tokenHanddler.WriteToken(token),
                User=user,
            };
            return loginResponseDTO;
        }

        public async Task<LocalUser> Register(RegisterationRequestDTO registerationRequestDTO)
        {
            LocalUser user = new LocalUser()
            {
                UserName = registerationRequestDTO.UserName,
                Password = registerationRequestDTO.Password,
                Name    = registerationRequestDTO.Name,
                Role = registerationRequestDTO.Role,
            };
            _db.LocalUsers.Add(user);
            await _db.SaveChangesAsync();
            user.Password = "";
            return user;
         
        }
    }
}

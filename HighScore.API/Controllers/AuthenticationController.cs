using AutoMapper;
using HighScore.Data.Repositories;
using HighScore.Domain.Entities;
using HighScore.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace HighScore.API.Controllers
{
    [Route("api/authentication")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {

        private IRepository<UserEntity> _userRepository;
        private IMapper _mapper;
        private IConfiguration _config;

        public AuthenticationController(IRepository<UserEntity> userRepository, IMapper mapper, IConfiguration configuration)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _config = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }


        [HttpPost("authenticate")]
        public async Task<ActionResult<string>> Authenticate(AuthenticationDTO authentication)
        {
            // authenticate
            var user = await ValidateUserCredentials(authentication.UserName);

            if(user == null)
            {
                return Unauthorized();
            }

            // create token
            var securityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_config["Authentication:SecretForKey"]));
            var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            // claims - info about the user/app
            var claimsForToken = new List<Claim>();
            claimsForToken.Add(new Claim("username", user.Username));

            var jwtSecurityToken = new JwtSecurityToken(
                    _config["Authentication:Issuer"],
                    _config["Authentication:Audience"],
                    claimsForToken,
                    DateTime.UtcNow,
                    DateTime.UtcNow.AddHours(1),
                    signingCredentials
                    );
            var tokenToReturn = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);

            return Ok(tokenToReturn);

        }

        private async Task<UserReadDTO> ValidateUserCredentials(string? userName)
        {
            var user = (await _userRepository.Find((u) => u.Username == userName)).Single();

            return _mapper.Map<UserReadDTO>(user);
        }


    }
}

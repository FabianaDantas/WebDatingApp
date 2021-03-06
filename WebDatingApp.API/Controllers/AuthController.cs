using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using AutoMapper;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using WebDatingApp.API.Data;
using WebDatingApp.API.Domain.DTOs;
using WebDatingApp.API.Domain.Models;

namespace WebDatingApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _repo;
        private readonly IConfiguration _config;
        private readonly IMapper _mapper;

        public AuthController(IAuthRepository repo, IConfiguration config, IMapper mapper)
        {
            _repo = repo;
            _config = config;
            _mapper = mapper;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserRegisterDTO userRegister)
        {
            // validando o request
            userRegister.Username = userRegister.Username.ToLower();
            
            if(await _repo.UserExists(userRegister.Username))
                return BadRequest("Nome de usuário já existe");

            
            /*var userNew = new User
            {
                Username = userRegister.Username
            };*/

            var userNew = _mapper.Map(userRegister, new User());

            var userCreated = await _repo.Register(userNew, userRegister.Password);

            var userReturn = _mapper.Map<UserForDetailedDTO>(userCreated);

            return CreatedAtRoute("GetUser", new {controller = "Users", id = userCreated.Id}, userReturn);

        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(UserLoginDTO userLogin) 
        {
            var userExists = await _repo.Login(userLogin.Username.ToLower(), userLogin.Password);

            if(userExists == null)
                return Unauthorized();

            // conteudo do token
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, userExists.Id.ToString()),
                new Claim(ClaimTypes.Name, userExists.Username)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("AppSettings:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);

            var user = _mapper.Map<UserForListDTO>(userExists);

            return Ok(new {
                token = tokenHandler.WriteToken(token),
                user
            });

        }
    }
}
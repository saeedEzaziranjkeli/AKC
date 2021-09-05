using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using AK.Application.DTOs;
using AK.Domain.Interfaces;
using AK.Domain.Models;
using Microsoft.Extensions.Logging;

namespace AK.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _authRepository;
        private readonly IConfiguration _config;
        private readonly IMapper _mapper;
        private readonly ILogger<AuthController> _logger;

        public AuthController(IAuthRepository authRepository, IConfiguration confgig, IMapper mapper, ILogger<AuthController> logger)
        {
            _authRepository = authRepository;
            _config = confgig;
            _mapper = mapper;
            _logger = logger;
        }
        /// <summary>
        /// please register to get a JWT-Token and use this token in Authorization with Bearer , model : {"username":"AKC","password":"123"}
        /// </summary>
        /// <param name="userForRegisterDto">model to create user</param>
        /// <returns></returns>
        [HttpPost("register")]
        public async Task<IActionResult> Register(UserDto userForRegisterDto)
        {
            try
            {
                if (userForRegisterDto is null) return BadRequest("model is empty");
                userForRegisterDto.Username = userForRegisterDto.Username.ToLower();
                //Check if user exist
                if (await _authRepository.UserExists(userForRegisterDto.Username))
                    return BadRequest("Username already exists");

                userForRegisterDto.PasswordByte = Encoding.UTF8.GetBytes(userForRegisterDto.Password);

                //Add User to Databe
                var userToCreate = _mapper.Map<User>(userForRegisterDto);
                var createdUser = await _authRepository.Register(userToCreate, userForRegisterDto.Password);
                var userToReturn = _mapper.Map<UserDto>(createdUser);

                //redirect to UserController
                return CreatedAtRoute("GetUser", new { controller = "User", id = createdUser.Id }, userToReturn);
            }
            catch (Exception e)
            {
                _logger.LogError("Cannot get drug with exception {0}", e.Message);
                throw new ArgumentException("cannot get drugs");
            }
          
        }

        /// <summary>
        /// Send registered username and password , then you get a JWT-Token
        /// </summary>
        /// <param name="userForLoginDto"></param>
        /// <returns></returns>
        [HttpPost("login")]
        public async Task<IActionResult> Login(UserDto userForLoginDto)
        {
            try
            {
                if (userForLoginDto is null) return BadRequest("model is empty");
                var userFromRepo = await _authRepository.Login(userForLoginDto.Username.ToLower(), userForLoginDto.Password);
                //if find the user
                if (userFromRepo == null)
                    return Unauthorized();

                //Id and name to genrate Token
                var claims = new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, userFromRepo.Id.ToString()),
                    new Claim(ClaimTypes.Name, userFromRepo.Username)
                };

                //generate token by JWT Token Libarary
                var key = new SymmetricSecurityKey(Encoding.UTF8
                    .GetBytes(_config.GetSection("AppSettings:Token").Value));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(claims),
                    Expires = DateTime.Now.AddDays(1),
                    SigningCredentials = creds
                };

                var tokenHandler = new JwtSecurityTokenHandler();

                var token = tokenHandler.CreateToken(tokenDescriptor);
                return Ok(new
                {
                    token = tokenHandler.WriteToken(token),
                    userFromRepo
                });
            }
            catch (Exception e)
            {
                _logger.LogError("Cannot get drug with exception {0}", e.Message);
                throw new ArgumentException("cannot get drugs");
            }
            
        }
    }
}

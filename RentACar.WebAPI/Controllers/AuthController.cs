using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RentACar.Business.Abstract;
using RentACar.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RentACar.Core.Entities.Concrete;
using RentACar.Entities.Dtos;

namespace RentACar.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }


        [HttpPost("login")]
        public IActionResult Login(UserForLoginDto userForLoginDto)
        {
            var userToLogin = _authService.Login(userForLoginDto);

            if (!userToLogin.IsSuccees)
                return BadRequest(userToLogin.Message);

            var result = _authService.CreateAccessToken(userToLogin.Data);

            if (!result.IsSuccees)
                return BadRequest(result.Message);

            return Ok(result.Data);
        }


        [HttpPost("register")]
        public IActionResult Register(UserForRegisterDto userForRegisterDto)
        {
            var userExist = _authService.UserExists(userForRegisterDto.Email);

            if (!userExist.IsSuccees)
                return BadRequest(userExist.Message);

            var registerResult = _authService.Register(userForRegisterDto);

            var result = _authService.CreateAccessToken(registerResult.Data);

            if (!result.IsSuccees)
                return BadRequest(result.Message);

            return Ok(result.Data);

        }

    }
}

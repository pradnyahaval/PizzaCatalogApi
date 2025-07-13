using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PizzaCatalog.WebApi.Model.DTOs;
using Microsoft.AspNetCore.Identity;
using PizzaCatalog.WebApi.Repositories;

namespace PizzaCatalog.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IJWTTokenRepository _jWTTokenRepository;

        public AuthController(UserManager<IdentityUser> userManager, IJWTTokenRepository jWTTokenRepository)
        {
            _userManager = userManager;
            _jWTTokenRepository = jWTTokenRepository;
        }

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> RegisterUser([FromBody] UserCredDTO userCredDTO)
        {
            var identityUser = new IdentityUser
            {
                UserName = userCredDTO.Username,
                Email = userCredDTO.Username
            };

            //create user
            var result = await _userManager.CreateAsync(identityUser, userCredDTO.Password);

            if (result.Succeeded)
            {
                //add role to user
                if(userCredDTO.roles != null && userCredDTO.roles.Any())
                {
                    result = await _userManager.AddToRolesAsync(identityUser, userCredDTO.roles);

                    if (result.Succeeded)
                        return Ok("User registered!! please login :)");
                }
                    
            }
            return BadRequest("Something went wrong :(");
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] LoginCredDTO loginCredDTO)
        {
            var user = await _userManager.FindByEmailAsync(loginCredDTO.Username);

            if(user != null)
            {
                var result = await _userManager.CheckPasswordAsync(user, loginCredDTO.Password);

                if (result)
                {
                    var roles = await _userManager.GetRolesAsync(user);
                    if(roles != null)
                    {
                        //create token
                        var token = _jWTTokenRepository.CreateJWTToken(user, roles.ToList());

                        var response = new TokenResponseDTO
                        {
                            Token = token,
                        };  
                        return Ok(response);
                    }
                }
            }
            return BadRequest("Username or password is wrong :(");
        }
    }
}

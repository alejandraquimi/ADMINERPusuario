using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using backend.Services;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

namespace ESPOL_Alert.Controllers.Authentication
{
    [ApiController]
    [Route("api/[controller]")]
    [EnableCors]
    public class LoginController : ControllerBase
    {
        private readonly LoginService loginService;
        private readonly IConfiguration config;
        public LoginController(LoginService loginService,IConfiguration config)
        {
            this.loginService=loginService;
            this.config=config;
        }

        [HttpPost("Athetication")]
        [AllowAnonymous]
        public async Task<IActionResult> Login(Usuario usuario)
        {
            var usua=await loginService.GetUser(usuario);
            if(usua is null)
                 return BadRequest("User Not Found");
            string jwtToken=GenerateJwtToken(usua);
            return Ok(new {token=jwtToken});

        }

        private string GenerateJwtToken(Usuario usuario)
        {
            var claims=new[]
            {
                new Claim("CorreoElectronico", usuario.CorreoElectronico),
                new Claim("Nombre", usuario.Nombre??""),
                new Claim("IDUsuario", usuario.IDUsuario.ToString())


            };
            var key=new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config.GetSection("JWT:KEY").Value??""));

            
            var credecs=new SigningCredentials(key,SecurityAlgorithms.HmacSha256Signature);
            var seecurityToken=new JwtSecurityToken(
                claims:claims,
                expires:DateTime.Now.AddMinutes(60000),
                signingCredentials:credecs
            );
            string token=new JwtSecurityTokenHandler().WriteToken(seecurityToken);
            return token;
        }
    }
}

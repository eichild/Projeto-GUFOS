using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Backend.Domains;
using Backend.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Backend.Controllers {
    [Route ("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase {
        //Chamamos nosso contexto da base de dados
        BDGUFOSContext _context = new BDGUFOSContext ();

        //Definimos uma variável para percorrer nossos métodos com as configurações obtidas no appsettings.json
        private IConfiguration _config;

        //Definimos um método construtor para poder acessa estas configs
        public LoginController (IConfiguration config) {
            _config = config;
        }

        //Chamamos nosso método para validar o usuario na aplicação
        private Usuario ValidaUsuario (LoginViewModel login) {
            //Comparando se o usuario esta no banco
            var usuario = _context.Usuario.FirstOrDefault (u => u.Email == login.Email && u.Senha == login.Senha);
            return usuario;
        }
        
        //Gerando token de acesso
        private string GerarToken (Usuario userInfo) {
            //Definimos a criptografia do nosso Token
            var securityKey = new SymmetricSecurityKey (Encoding.UTF8.GetBytes (_config["Jwt:Key"]));

            var credentials = new SigningCredentials (securityKey, SecurityAlgorithms.HmacSha256);

            //Definimos nossas Claims(Dados da sessão)
            var claims = new [] {
                new Claim (JwtRegisteredClaimNames.NameId, userInfo.Nome),
                new Claim (JwtRegisteredClaimNames.Email, userInfo.Email),
                new Claim (JwtRegisteredClaimNames.Jti, Guid.NewGuid ().ToString ())
            };
            //Conferindo nosso token e seu tempo de vida
            var token = new JwtSecurityToken (
                _config["Jwt:Issuer"],
                _config["Jwt:Issuer"],
                claims,
                expires : DateTime.Now.AddMinutes (120),
                signingCredentials : credentials
            );
            return new JwtSecurityTokenHandler ().WriteToken (token);
        }

        //Usamos essa anotação para ignorar a autenticação nesse metodo
        [AllowAnonymous]
        [HttpPost]
        public IActionResult Login ([FromBody] LoginViewModel login) {
            IActionResult response = Unauthorized ();
            var user = ValidaUsuario (login);

            if (user != null) {
                var tokenString = GerarToken (user);
                response = Ok (new { token = tokenString });
            }
            return response;
        }
    }
}
using System.Threading.Tasks;
using IdentityProject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using IdentityProject.Services;

namespace IdentityProject.Controllers
{
    [Route("account")]
    public class UserController : Controller
    {
        [HttpPost]
        [Route("login")]
        [AllowAnonymous]
        public async Task<ActionResult<dynamic>> Autenticate([FromBody]User model)
        {
            var user = UserRepository.Get(model.UserName, model.Password);

            if (user == null) return NotFound(new { message = "Usuário ou senha inválida" });

            var token = TokenService.GenerateToken(user);
            user.Password = "";
            return new
            {
                user = user,
                token = token
            };
        }

        [HttpGet]
        [Route("anonymous")]
        [AllowAnonymous]
        public string Anonymous() => "Anônimo";

        [HttpGet]
        [Route("authenticated")]
        [Authorize]
        public string Authenticated() => string.Format("Autenticado - {0}", User.Identity.Name);

        [HttpGet]
        [Route("employee")]
        [Authorize(Roles = ("employee,manager"))]
        public string Employee() => "Funcionário";

        [HttpGet]
        [Route("manager")]
        [Authorize(Roles = ("manager"))]
        public string Manager() => "Gerente";

    }
}
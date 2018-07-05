using System.Threading.Tasks;
using MicroServicosPoc.Security.UsuarioCommands.Request;
using MicroServicosPoc.Security.Handlers.Interfaces;
using MicroServicosPoc.Security.Handlers;
using Microsoft.AspNetCore.Mvc;
using MicroServicosPoc.Matricula.Domain.Commands.Interfaces;
using MicroServicosPoc.Security.UsuarioCommands.Response;

namespace MicroServicosPoc.Security.Controllers
{
    [Route("api/[controller]")]
    public class LoginController : Controller
    {
        private readonly UsuarioHandler _usuarioHandler;

        public LoginController(UsuarioHandler usuarioHandler)
        {
            _usuarioHandler = usuarioHandler;
        }

        [HttpPost]
        public async Task<ICommandResult> Login([FromBody] AutenticarUsuarioCommand command){

                var result  = (AutenticarUsuarioCommandResult) await _usuarioHandler.HandleAsync(command);

                return result;
        }
           
    }
}
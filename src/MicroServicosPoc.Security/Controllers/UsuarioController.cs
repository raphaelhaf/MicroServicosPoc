using MicroServicosPoc.Matricula.Domain.Commands.Interfaces;
using MicroServicosPoc.Security.Handlers;
using MicroServicosPoc.Security.UsuarioCommands.Request;
using MicroServicosPoc.Security.UsuarioCommands.Response;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace MicroServicosPoc.Security.Controllers
{
    [Route("api/[controller]")]
    public class UsuarioController : Controller
    {

        private readonly UsuarioHandler _handler;

        public UsuarioController(UsuarioHandler handler)
        {
            _handler = handler;
        }

        [HttpPost("")]
        public async Task<ICommandResult> Post([FromBody]CriarUsuarioCommand command)
        {
            
            var result =  (CriarUsuarioCommandResult) await _handler.HandleAsync(command);

            return result;
        }
    }
}

using MicroServicosPoc.Matricula.Domain.Commands.Interfaces;

namespace MicroServicosPoc.Security.UsuarioCommands.Request
{
    public class AutenticarUsuarioCommand : ICommand
    {
        public string Email { get; set; }
        public string Senha { get; set; }
    }
}
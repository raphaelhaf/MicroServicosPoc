using MicroServicosPoc.Matricula.Domain.Commands.Interfaces;

namespace MicroServicosPoc.Security.UsuarioCommands.Request
{
    public class CriarUsuarioCommand : ICommand
    {
        public string Email { get; set; }
        public string Senha { get; set; }
        public string Nome { get; set; }
    }
}
using MicroServicosPoc.Matricula.Domain.Commands.Interfaces;

namespace MicroServicosPoc.Security.UsuarioCommands.Response
{
    public class CriarUsuarioCommandResult : ICommandResult
    {
        public bool Success { get ; set; }
        public string Message { get; set ; }
        public object Data { get; set; }
        
    }
}
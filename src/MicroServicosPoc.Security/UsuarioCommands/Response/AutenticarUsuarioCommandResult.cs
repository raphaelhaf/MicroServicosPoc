using MicroServicosPoc.Matricula.Domain.Commands.Interfaces;

namespace MicroServicosPoc.Security.UsuarioCommands.Response
{
    public class AutenticarUsuarioCommandResult : ICommandResult
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }

        public AutenticarUsuarioCommandResult(bool sucess, string message, object data)
        {
            Success = sucess;
            Message = message;
            Data = data;
        }
    }
}
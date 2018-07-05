using MicroServicosPoc.Matricula.Domain.Commands.Interfaces;

namespace MicroServicosPoc.Matricula.Domain.Commands
{
    public class GerarMatriculaCommandResult : ICommandResult
    {

        public bool Success { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }

        public GerarMatriculaCommandResult(bool sucess, string message, object data)
        {
            Success = sucess;
            Message = message;
            Data = data;
        }
    }
}
using MicroServicosPoc.Matricula.Domain.Commands.Interfaces;

namespace MicroServicosPoc.Matricula.Domain.Commands
{
    public class GerarMatriculaCommand : ICommandAuthenticated
    {
        public string Cpf { get; set; }
        public string IdUsuarioEmail { get; set; }


        public GerarMatriculaCommand(string cpf, string idUsuarioEmail)
        {
            Cpf = cpf;
            IdUsuarioEmail = idUsuarioEmail;
        }
    }
}
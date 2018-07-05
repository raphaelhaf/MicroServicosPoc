using System.Threading.Tasks;
using MicroServicosPoc.Matricula.Domain.Commands.Interfaces;

namespace MicroServicosPoc.Security.Handlers.Interfaces
{
     public interface ICommandHandler<in T> where T : ICommand
    {
        Task<ICommandResult> HandleAsync(T command);
    }
}
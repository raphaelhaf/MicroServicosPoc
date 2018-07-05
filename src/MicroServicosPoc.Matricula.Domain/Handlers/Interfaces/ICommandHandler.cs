using System.Threading.Tasks;
using MicroServicosPoc.Matricula.Domain.Commands.Interfaces;

namespace MicroServicosPoc.Matricula.Domain.Handlers.Interfaces
{
    public interface ICommandHandler<in T> where T : ICommand
    {
        Task<ICommandResult> Handle(T command);
    }
}
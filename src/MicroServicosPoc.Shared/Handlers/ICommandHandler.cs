using System.Threading.Tasks;
using MicroServicosPoc.Matricula.Domain.Commands.Interfaces;

namespace MicroServicosPoc.Shared.Handlers
{
    public interface ICommandHandler<in T> where T : ICommand
    {
         Task HandleAsync(T command);
    }
}
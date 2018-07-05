using System.Threading.Tasks;
using MicroServicosPoc.Matricula.Domain.Queries;

namespace MicroServicosPoc.Matricula.Domain.Repositories
{
    public interface IMatriculaRepository
    {
        Task Salvar(MicroServicosPoc.Matricula.Domain.Entities.Matricula matricula);

        Task<bool> CpfExiste(string cpf);
         
        Task<BuscarMatriculaQueryResult> ObterPorCpf(string cpf);
    }
}
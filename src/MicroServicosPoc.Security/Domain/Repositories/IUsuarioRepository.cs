using System;
using System.Threading.Tasks;
using MicroServicosPoc.Security.Domain.Entities;

namespace MicroServicosPoc.Security.Domain.Repositories
{
    public interface IUsuarioRepository
    {
        Task<Usuario> GetAsync(Guid id);
        Task<Usuario> GetAsync(string email);
        Task AdicionarAsync(Usuario usuario);
    }
}
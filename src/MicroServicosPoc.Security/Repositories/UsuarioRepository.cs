using System;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using MicroServicosPoc.Security.Domain.Entities;
using MicroServicosPoc.Security.Domain.Repositories;
using MicroServicosPoc.Security.Repositories.DataContext;

namespace MicroServicosPoc.Security.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly MicroServicosPocSecurityDataContext _context;

        public UsuarioRepository(MicroServicosPocSecurityDataContext context)
        {
            _context = context;
        }

        public async Task AdicionarAsync(Usuario usuario)
        {
            var sqlCommand = new StringBuilder();
            
            sqlCommand.Append("INSERT INTO `db_security`.`usuario`");
            sqlCommand.Append(" SET `Id` = @ID");
            sqlCommand.Append(", `Email`= @EMAIL");
            sqlCommand.Append(", `Nome`= @NOME");
            sqlCommand.Append(", `Senha`= @SENHA");
            sqlCommand.Append(", `Salt`= @SALT");
            sqlCommand.Append(", `DataCriacao`= @DATACRIACAO");
            sqlCommand.Append(", IsAtivo= @ISATIVO");

             await _context.Connection.ExecuteAsync(sqlCommand.ToString(),
                new 
                {
                   ID = usuario.Id,
                   EMAIL = usuario.Email,
                   NOME = usuario.Nome,
                   SENHA = usuario.Senha,
                   SALT = usuario.Salt,
                   DATACRIACAO = usuario.DataCriacao,
                   ISATIVO = usuario.IsAtivo
                });

        }

        public async Task<Usuario> GetAsync(Guid id)
        {
             return
                await _context
                .Connection
                    .QueryFirstOrDefaultAsync<Usuario>(
                        "SELECT Id, Email, Nome, Senha, Salt, DataCriacao, IsAtivo FROM db_security.usuario  WHERE Id = @ID", new {ID = id});
                
        }

        public async Task<Usuario> GetAsync(string email)
        {
             return
                await _context
                .Connection
                    .QueryFirstOrDefaultAsync<Usuario>(
                        "SELECT Id, Email, Nome, Senha, Salt, DataCriacao, IsAtivo FROM db_security.usuario WHERE Email = @EMAIL", new {EMAIL = email});
                
        }

    }
}
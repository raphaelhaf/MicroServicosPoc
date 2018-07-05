using System.Data;
using System.Text;
using Dapper;
using MicroServicosPoc.Matricula.Domain.Commands;
using MicroServicosPoc.Matricula.Domain.Entities;
using MicroServicosPoc.Matricula.Domain.Queries;
using MicroServicosPoc.Matricula.Infra.DataContext;
using System.Linq;
using MicroServicosPoc.Matricula.Domain.Repositories;
using System.Threading.Tasks;

namespace MicroServicosPoc.Matricula.Infra.Repositories
{
    public class MatriculaRepository : IMatriculaRepository
    {

        private readonly MicroServicosPocMatriculaDataContext _context;
        
        public MatriculaRepository(MicroServicosPocMatriculaDataContext context)
        {
            _context = context;
        }

        public async Task<bool> CpfExiste(string cpf)
        {
            return await _context.Connection.ExecuteScalarAsync<bool>(
                "SELECT count(*) FROM db_matricula.matricula WHERE Cpf = @CPF", new{CPF = cpf} );
        }

        public async Task<BuscarMatriculaQueryResult> ObterPorCpf(string cpf)
        {
            return
                await _context.Connection
                .QueryFirstAsync<BuscarMatriculaQueryResult>(
                    "SELECT Id, DataHora, Cpf, Ra, IsAtivo FROM db_matricula.matricula WHERE Cpf = @CPF", new{CPF = cpf});
        }

        public async Task Salvar(Domain.Entities.Matricula matricula)
        {

            var sqlCommand = new StringBuilder();

            sqlCommand.Append("INSERT INTO `db_matricula`.`matricula`");
            sqlCommand.Append(" SET `Id` = @ID");
            sqlCommand.Append(", `Cpf`= @CPF");
            sqlCommand.Append(", `Ra`= @RA");
            sqlCommand.Append(", `DataHora`= @DATAHORA");
            sqlCommand.Append(", IsAtivo= @ISATIVO");
            sqlCommand.Append(", IdUsuarioEmail = @IDUSUARIOEMAIL");

            
            await _context.Connection.ExecuteAsync(sqlCommand.ToString(),
                new 
                {
                    ID = matricula.Id,
                    CPF = matricula.Cpf,
                    RA = matricula.Ra ,
                    DATAHORA = matricula.DataHora,
                    ISATIVO = matricula.IsAtivo,
                    IDUSUARIOEMAIL = matricula.IdUsuarioEmail
                });
        }
    }
}
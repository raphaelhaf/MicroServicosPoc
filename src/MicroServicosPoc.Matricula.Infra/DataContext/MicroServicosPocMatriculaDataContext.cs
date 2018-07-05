using System.Data;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;

namespace MicroServicosPoc.Matricula.Infra.DataContext
{
    public class MicroServicosPocMatriculaDataContext
    {
        public MySqlConnection Connection { get; set; }

        private IConfiguration _configuracoes;

        public MicroServicosPocMatriculaDataContext(IConfiguration configuracoes)
        {
            _configuracoes = configuracoes;

            Connection = new MySqlConnection(_configuracoes.GetConnectionString("DatabaseMatricula"));

            Connection.Open();
        }

        public void Dispose()
        {
            if (Connection.State != ConnectionState.Closed)
                Connection.Close();
        }
    }
}
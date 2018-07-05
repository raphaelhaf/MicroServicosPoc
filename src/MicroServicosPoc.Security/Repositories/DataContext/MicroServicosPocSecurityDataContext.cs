using System.Data;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;

namespace MicroServicosPoc.Security.Repositories.DataContext
{
    public class MicroServicosPocSecurityDataContext
    {
        public MySqlConnection Connection { get; set; }

        private IConfiguration _configuracoes;

        public MicroServicosPocSecurityDataContext(IConfiguration configuracoes)
        {
            _configuracoes = configuracoes;

            Connection = new MySqlConnection(_configuracoes.GetConnectionString("DatabaseSecurity"));

            Connection.Open();
        }

        public void Dispose()
        {
            if (Connection.State != ConnectionState.Closed)
                Connection.Close();
        }
    }
}
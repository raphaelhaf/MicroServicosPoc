using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MicroServicosPoc.Financeiro.Cobranca.Sender.Commands
{
    public class EnviaCobrancaSmsCommand
    {
        public string Numero { get; set; }

        public string Mensagem { get; set; }
    }
}

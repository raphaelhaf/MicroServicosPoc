using MicroServicosPoc.Matricula.Domain.Commands.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MicroServicosPoc.Financeiro.Cobranca.Sender.Commands
{
    public class EnviaCobrancaSmsCommandResult : ICommandResult
    {
        public bool Success { get ; set ; }
        public string Message { get; set; }
        public object Data { get; set; }
    }
}

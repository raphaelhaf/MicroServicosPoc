using RestSharp.Serializers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MicroServicosPoc.Financeiro.Cobranca.Sender.Commands
{
    public class EnviaSmsApiCommand
    {
        public string phone_number { get; set; }

        public string message { get; set; }

        public int device_id { get; set; }

        public EnviaSmsApiCommand(){}

        public EnviaSmsApiCommand(string telefoneNumero, string mensagem, int idDispositivoEnvio)
        {
            phone_number = telefoneNumero;
            message = mensagem;
            device_id = idDispositivoEnvio;
        }

    }
}

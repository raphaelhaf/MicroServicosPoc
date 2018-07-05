using RestSharp.Deserializers;

namespace MicroServicosPoc.Financeiro.Cobranca.Sender.Commands
{
    public class EnviaSmsApiCommandResult
    {
        [DeserializeAs(Name = "id")]
        public int IdMensagem { get; set; }

        [DeserializeAs(Name = "device_id")]
        public string IdDispositivoEnvio { get; set; }

        [DeserializeAs(Name = "phone_number")]
        public string TelefoneNumero { get; set; }

        [DeserializeAs(Name = "status")]
        public string Status { get; set; }

        [DeserializeAs(Name = "created_at")]
        public string DataHoraCriacao { get; set; }


        public override string ToString()
        {
            return $"\nIdMensagem: {IdMensagem} \nIdDispositivoEnvio: {IdDispositivoEnvio} \nTelefoneNumero: {TelefoneNumero} \nStatus: {Status} \nDataHoraCriacao:{DataHoraCriacao}";
        }


    }
}

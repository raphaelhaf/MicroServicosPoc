namespace MicroServicosPoc.Matricula.Domain.Commands
{
    public class IntegrarMatriculaCommandResponse
    {
        public string Id { get; set; }
        public string DataHora { get; set; }
        public string Cpf { get; set; }
        public long Ra { get; set; }
        public bool IsAtivo { get; set; }
        public string OrigemIntegracao { get; set; }
        public string Mensagem { get; set; }
        public string IdUsuarioEmail { get; set; }

        public override string ToString(){
            return $"Id: {Id} \nDataHora: {DataHora} \nCPF: {Cpf} \nRA: {Ra} \nAtivo: {IsAtivo} \nOrigemIntegracao: {OrigemIntegracao} \nMensagem: {Mensagem} \nIdUsuarioEmail: {IdUsuarioEmail}";
        }
    }
}
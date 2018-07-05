using System;
using MicroServicosPoc.Matricula.Domain.Commands.Interfaces;
using MicroServicosPoc.Matricula.Domain.Queries;

namespace MicroServicosPoc.Matricula.Domain.Commands
{
    public class IntegrarMatriculaCommand : ICommandAuthenticated
    {

        public Guid Id { get; set;}

        public string DataHora { get; set;}

        public string Cpf { get; set;}

        public long Ra { get; set;}

        public bool IsAtivo { get; set;}

        public string OrigemIntegracao {get; set;}

        public string IdUsuarioEmail { get; set; }

        // É recomendável criar um construtor em branco para serializar o objeto
        public IntegrarMatriculaCommand(){}
        
        public override string ToString(){
            return $"Id:{Id} \nDataHora:{DataHora} \nCPF:{Cpf} \nRA:{Ra} \nAtivo: {IsAtivo} \nOrigemIntegracao: {OrigemIntegracao} \nIdUsuarioEmail: {IdUsuarioEmail}";
        }
    }
}
using System;
using Flunt.Validations;
using Flunt.Br.Validation;

namespace MicroServicosPoc.Matricula.Domain.Entities
{
    public class Matricula : Entity
    {
        public string Cpf { get; }

        public long Ra { get; }

        public bool IsAtivo { get; }

        

         public Matricula(string cpf, bool isAtivo, string idUsuarioEmail)
         {
            Cpf = cpf;
            IsAtivo = isAtivo;
            IdUsuarioEmail = idUsuarioEmail;

            // Realizando as validações no construtor do objeto para garantir a integridade
            AddNotifications( new Contract()
                .Requires()
                .IsCpf(Cpf, "matricula.cpf","Cpf Inválido")
                .IsNotNullOrEmpty(IdUsuarioEmail, "Segurança", "Entidade não possui usuário criador associado"));                ;
         }
    }
}
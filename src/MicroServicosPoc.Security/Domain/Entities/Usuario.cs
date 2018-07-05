using System;
using Flunt.Validations;
using MicroServicosPoc.Matricula.Domain.Entities;
using MicroServicosPoc.Security.Domain.Services;

namespace MicroServicosPoc.Security.Domain.Entities
{
    public class Usuario : Entity
    {
        public string Email { get; protected set; }
        public string Nome { get; protected set; }
        public string Senha { get; protected set; }
        public string Salt { get; protected set; }
        public DateTime DataCriacao { get; protected set; }
        public Boolean IsAtivo { get; protected set; }

        protected Usuario(){}
        
        public Usuario(string email, string nome)
        {
            
            AddNotifications(new Contract()
                .IsEmail(email, "usuario.email", "E-mail inválido")
                .IsNotNullOrEmpty(nome, "usuario.nome", "Nome deve ser preenchido"));
            
            Email = email.ToLowerInvariant();
            Nome = nome;
            DataCriacao = DateTime.UtcNow;
            IsAtivo = true;
        }

        public void SetSenha(string senha, IEncrypter encrypter)
        {
             AddNotifications(new Contract()
                .IsNotNullOrEmpty(senha, "usuario.senha", "Senha deve ser preenchida"));
                  
            Salt = encrypter.GetSalt();
            Senha = encrypter.GetHash(senha, Salt);
            
        }

        public bool ValidarSenha(string senha, IEncrypter encrypter)
            => Senha.Equals(encrypter.GetHash(senha, Salt));
    }
}
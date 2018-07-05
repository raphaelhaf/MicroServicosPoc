using System;
using Flunt.Notifications;

namespace MicroServicosPoc.Matricula.Domain.Entities
{
    public abstract class Entity : Notifiable
    {
        // Todas as entidades do sistema devem possuir um id unico e data/hora para auditoria
        
        public Entity()
        {
            Id = Guid.NewGuid();
            DataHora = DateTime.Now;
        }

        public Guid Id { get; }

        public DateTime DataHora { get; }

        public string IdUsuarioEmail { get; set; }
    }
}
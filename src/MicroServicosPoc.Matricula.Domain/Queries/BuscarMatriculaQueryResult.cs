using System;

namespace MicroServicosPoc.Matricula.Domain.Queries
{
    // Entidadade utilizada como VO para os dados da matrícula
    public class BuscarMatriculaQueryResult
    {
        public Guid Id { get; set; }

        public DateTime DataHora { get; set; }

        public string Cpf { get; set; }

        public long Ra { get; set; }

        public bool IsAtivo { get; set; }
    }
}
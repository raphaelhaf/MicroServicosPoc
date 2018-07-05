using System;
using System.Collections.Generic;
using System.Text;

namespace MicroServicosPoc.Matricula.Domain.Commands.Interfaces
{
    interface ICommandAuthenticated : ICommand
    {
        string IdUsuarioEmail { get; set; }
    }
}

using System;
using System.Threading.Tasks;
using Flunt.Notifications;
using Flunt.Validations;
using MicroServicosPoc.Matricula.Domain.Commands;
using MicroServicosPoc.Matricula.Domain.Commands.Interfaces;
using MicroServicosPoc.Matricula.Domain.Handlers.Interfaces;
using MicroServicosPoc.Matricula.Domain.Repositories;

namespace MicroServicosPoc.Matricula.Domain.Handlers
{
    public class MatriculaHandler : Notifiable, ICommandHandler<GerarMatriculaCommand>
    {

        private readonly IMatriculaRepository _matriculaRepository;

        public MatriculaHandler(IMatriculaRepository matriculaRepository)
        {
            _matriculaRepository = matriculaRepository;
        }

        public async Task<ICommandResult> Handle(GerarMatriculaCommand command)
        {
           
           var matricula = new MicroServicosPoc.Matricula.Domain.Entities.Matricula(command.Cpf, true, command.IdUsuarioEmail);
           
           AddNotifications(matricula);

           if ( await _matriculaRepository.CpfExiste(command.Cpf))
           {
               AddNotification("matricula.cpf", "Já existe matrícula com este CPF");
           }

           if (Invalid){
               return new GerarMatriculaCommandResult(false, "Inconsistências encontradas", Notifications);
           } 

            await _matriculaRepository.Salvar(matricula);

            var matriculaQueryResult = await _matriculaRepository.ObterPorCpf(matricula.Cpf);

            return new GerarMatriculaCommandResult(true, "Matricula inserida com sucesso!", matriculaQueryResult);
            
        }
    }
}
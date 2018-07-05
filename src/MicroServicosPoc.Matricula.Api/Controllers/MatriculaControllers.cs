using System;
using System.Threading.Tasks;
using MicroServicosPoc.Matricula.Domain.Commands;
using MicroServicosPoc.Matricula.Domain.Commands.Interfaces;
using MicroServicosPoc.Matricula.Domain.Handlers;
using MicroServicosPoc.Matricula.Domain.Queries;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RawRabbit;

namespace MicroServicosPoc.Matricula.Api.Controllers
{
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class MatriculaController : Controller
    {
        private readonly IBusClient _busClient;
        private readonly MatriculaHandler _matriculaHandler;

        private const string ORIGEM_INTEGRACAO = "MENSAGERIA_MATRICULA_MICROSERVICO";

        public MatriculaController(IBusClient busClient, MatriculaHandler matriculaHandler)
        {
            _busClient = busClient;
            _matriculaHandler = matriculaHandler;
        }
        
        [HttpGet]
        [Route("{cpfAluno}/ra")]
        public async Task<IActionResult> Get(string cpfAluno)
        {
            // para atender os requisitos de segurança atribui o id do usuário ao comando
            GerarMatriculaCommand command = new GerarMatriculaCommand(cpfAluno, User.Identity.Name);
            
            // realiza a lógica da geração e gravação do ra no banco
            var result = (GerarMatriculaCommandResult) await _matriculaHandler.Handle(command);
           
           // se houve alguma inconsitência
            if (!result.Success)
                return BadRequest(result);

            // caso matricula gerada e gravada no banco posta a mensagem no barramento
            // para que o serviço de integração a consuma

            IntegrarMatriculaCommand integrarMatriculaCommand = new IntegrarMatriculaCommand();
            
            var matriculaQueryResult = (BuscarMatriculaQueryResult) result.Data;

           integrarMatriculaCommand.Id = matriculaQueryResult.Id;
           integrarMatriculaCommand.DataHora = matriculaQueryResult.DataHora.ToString();
           integrarMatriculaCommand.Cpf = matriculaQueryResult.Cpf;
           integrarMatriculaCommand.Ra = matriculaQueryResult.Ra;
           integrarMatriculaCommand.IsAtivo = matriculaQueryResult.IsAtivo;
           integrarMatriculaCommand.OrigemIntegracao = ORIGEM_INTEGRACAO;
           integrarMatriculaCommand.IdUsuarioEmail = User.Identity.Name; ;

            await _busClient.PublishAsync(integrarMatriculaCommand);

            return Ok(result);
        }
        
    }
}
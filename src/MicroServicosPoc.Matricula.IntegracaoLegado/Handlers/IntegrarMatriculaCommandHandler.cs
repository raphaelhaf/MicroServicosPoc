using System;
using System.Threading.Tasks;
using MicroServicosPoc.Matricula.Domain.Commands;
using MicroServicosPoc.Shared.Handlers;
using MicroServicosPoc.Shared.Utils;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using RestSharp;

namespace MicroServicosPoc.Matricula.IntegracaoLegado.Handlers
{
    public class IntegrarMatriculaCommandHandler : ICommandHandler<IntegrarMatriculaCommand>
    {

        private IConfiguration _configuracoes;
        private ILogger _logger;
        
        public IntegrarMatriculaCommandHandler(IConfiguration configuracoes, 
            ILogger<IntegrarMatriculaCommandResponse> logger)
        {
              _configuracoes = configuracoes;
              _logger = logger;
        }

        public async Task HandleAsync(IntegrarMatriculaCommand command)
        {
            
            // recuperando as configurações de api de integracao do legado do arquivo appsetings.json
            var configuracoesApiLegado =   _configuracoes.GetSection("IntegracaoMatriculaLegado");
            string host = configuracoesApiLegado["Host"];
            string port = configuracoesApiLegado["Porta"];
            string action =  configuracoesApiLegado["Action"];
            
            string uriApiIntegracaoLegado = $"http://{host}:{port}/{action}";

            // utilizando a biblioteca restsharp para realizar a requisição ao barramento
            var client =  new RestClient($"http://{host}:{port}");
            var request = new RestRequest(action, Method.POST);

            // utilizando um serirador personalizado para poder converter as propriedades da classe
            // do padrão PascalCase para o padrão camelCase
            request.RequestFormat = DataFormat.Json;
            request.JsonSerializer = NewtonsoftJsonSerializer.Default;
            request.AddJsonBody(command);

            // realizando a requisição assíncrona e imprimindo o objeto
            // IntegrarMatriculaCommandResponse devolvido na request
            client.ExecuteAsync<IntegrarMatriculaCommandResponse>(request, response => {
                 _logger.LogInformation( $"Response da api de integração \n {response.Data.ToString()}");
            });

            await Task.CompletedTask;
        }
    }
}
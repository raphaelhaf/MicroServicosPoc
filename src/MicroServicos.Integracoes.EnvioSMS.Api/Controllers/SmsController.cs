using Flunt.Br.Validation;
using Flunt.Validations;
using MicroServicosPoc.Financeiro.Cobranca.Sender.Commands;
using MicroServicosPoc.Shared.Utils;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MicroServicos.Integracoes.EnvioSMS.Api.Controllers
{
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class SmsController : Controller
    {

        private readonly IConfiguration _configuracoes;

        private readonly ILogger _logger;

        public SmsController(IConfiguration configuration, ILogger<List<EnviaSmsApiCommandResult>> logger)
        {
            _configuracoes = configuration;
            _logger = logger;
        }


        [HttpPost]
        public async Task<IActionResult> Post([FromBody] EnviaCobrancaSmsCommand command)
        {

            var validationContract = new Contract()
                .Requires()
                .IsNotNullOrEmpty(command.Mensagem, "sms.mensagem", "Mensagem é obrigatória!")
                .IsPhone(command.Numero, "999999999999", "sms.telefone", "Numero de telefone inválido");
            
            // devolvendo a resposta com o código apropriado para facilitar a integração com outras APIs
            if (!validationContract.Valid)
                return BadRequest(new EnviaCobrancaSmsCommandResult
                {
                    Success = false,
                    Message = "Inconsistências Encontradas",
                    Data = validationContract.Notifications
                });

            // realizando as configurações para a api de envio de mensagem
            var configuracoesApiLegado = _configuracoes.GetSection("IntegracaoSMS");
            string host = configuracoesApiLegado["Host"];
            string action = configuracoesApiLegado["Action"];
            string apiToken = configuracoesApiLegado["ApiToken"];
            int idDispositivoEnvio = Convert.ToInt32(configuracoesApiLegado["DeviceId"]);

            // utilizando a biblioteca restsharp para realizar a requisição ao barramento
            var client = new RestClient(host);
            var request = new RestRequest(action, Method.POST);

            // configurando a chave de autorização para o envio do sms
            request.AddHeader("Authorization", apiToken);

            var enviaSmsApiCommand = new EnviaSmsApiCommand(command.Numero, command.Mensagem, idDispositivoEnvio);

            var listaSmsApiComands = new List<EnviaSmsApiCommand>();

            listaSmsApiComands.Add(enviaSmsApiCommand);

            // utilizando um serirador personalizado para poder converter as propriedades da classe
            // do padrão PascalCase para o padrão camelCase
            request.RequestFormat = DataFormat.Json;
            request.JsonSerializer = NewtonsoftJsonSerializer.Default;
            request.AddJsonBody(listaSmsApiComands);

            
            var response = await client.ExecuteTaskAsync<List<EnviaSmsApiCommandResult>>(request);

            // devolvendo a resposta com o código apropriado para facilitar a integração com outras APIs
            if (!response.IsSuccessful)
            {
                // realizando o log para que o problema na integração possa ser analisado
                _logger.LogError($"Erro na integração com o gateway SMS", response.Content);

                return BadRequest(
                    new EnviaCobrancaSmsCommandResult
                    {
                            Success = false,
                            Message = "Erro na API ",
                            Data = JObject.Parse(response.Content)
                    });

            }
                
            // devolvendo a resposta com o código apropriado para facilitar a integração com outras APIs
            return Ok(
                new EnviaCobrancaSmsCommandResult
                {
                            Success = true,
                            Message = "SMS enviado com sucesso!",
                            Data = response.Data[0]
                });
        }
    }
}

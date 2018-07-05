using System.Threading.Tasks;
using Flunt.Notifications;
using Flunt.Validations;
using MicroServicosPoc.Matricula.Domain.Commands.Interfaces;
using MicroServicosPoc.Security.Domain.Entities;
using MicroServicosPoc.Security.Domain.Repositories;
using MicroServicosPoc.Security.Domain.Services;
using MicroServicosPoc.Security.Handlers.Interfaces;
using MicroServicosPoc.Security.UsuarioCommands.Request;
using MicroServicosPoc.Security.UsuarioCommands.Response;

using MicroServicosPoc.Shared.Auth;
using Microsoft.Extensions.Logging;

namespace MicroServicosPoc.Security.Handlers
{
    public class UsuarioHandler : Notifiable, 
        ICommandHandler<CriarUsuarioCommand>,
        ICommandHandler<AutenticarUsuarioCommand>
    {
        private readonly ILogger _logger;
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IEncrypter _encrypter;
        private readonly IJwtHandler _jwtHandler;

       

        public UsuarioHandler(
            IUsuarioRepository usuarioRepository,
            IEncrypter encrypter,
            IJwtHandler jwtHandler,
            ILogger<UsuarioHandler> logger)
        {
            _usuarioRepository = usuarioRepository;
            _encrypter = encrypter;
            _jwtHandler = jwtHandler;
            _logger = logger;
        }

        public async Task<ICommandResult> HandleAsync(CriarUsuarioCommand command)
        {      
            var usuario = await _usuarioRepository.GetAsync(command.Email);

            AddNotifications(new Contract()
                .Requires()
                .IsNull(usuario, "usuario.e-mail","E-mail já utilizado!"));

            if(Invalid)
                return new CriarUsuarioCommandResult
                {
                    Success = false,
                    Message = "Inconsistências encontradas",
                    Data = Notifications
                };

            usuario = new Usuario(command.Email, command.Nome);
            
            usuario.SetSenha(command.Senha, _encrypter);
            
            AddNotifications(usuario.Notifications);


            if (Invalid)
                return new CriarUsuarioCommandResult
                {
                    Success = false,
                    Message = "Inconsistências encontradas",
                    Data = Notifications
                };

            _logger.LogInformation($"Realizando o regirstro do usuario: {command.Email}");

            await _usuarioRepository.AdicionarAsync(usuario);

            return new CriarUsuarioCommandResult{
                Success = true, 
                Message = "Usuário registrado com sucesso",
                Data = command.Email};
   
        }

        public async Task<ICommandResult> HandleAsync(AutenticarUsuarioCommand command)
        {
            AddNotifications(new Contract()
                .Requires()
                .IsNotNullOrEmpty(command.Email,"usuario.e-mail","E-mail deve ser preenchido!")
                .IsNotNullOrEmpty(command.Senha,"usuario.senha","Senha deve ser preenchida!"));

            if(Invalid){
                 return new AutenticarUsuarioCommandResult(false,"Inconsitências encontradas", Notifications);
            }

            var usuario = await _usuarioRepository.GetAsync(command.Email);
            
             AddNotifications( new Contract()
                .Requires()
                .IsNotNull(usuario, "usuario.email","Usuário inválido"));
                
            
            if(Invalid){
                _logger.LogError(Notifications.ToString());
                return new AutenticarUsuarioCommandResult(false,"Inconsitências encontradas", Notifications);
            }
            
             AddNotifications( new Contract()
                .Requires()
                .IsTrue(usuario.ValidarSenha(command.Senha, _encrypter), "usuario.senha","Senha inválida"));
            
            if(Invalid){
                _logger.LogError(Notifications.ToString());
                return new AutenticarUsuarioCommandResult(false,"Inconsitências encontradas", Notifications);
            }

            _logger.LogInformation($"Credênciais válidas, retornando o usuário: {usuario.Email}");

            return new AutenticarUsuarioCommandResult(true,"sucess",_jwtHandler.Create(usuario.Email));

        }
    }
}
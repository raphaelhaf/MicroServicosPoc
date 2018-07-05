using System.Reflection;
using System.Threading.Tasks;
using MicroServicosPoc.Shared.Handlers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using RawRabbit;
using RawRabbit.Instantiation;
using RawRabbit.Pipe;
using MicroServicosPoc.Matricula.Domain.Commands.Interfaces;

namespace MicroServicosPoc.Shared.RabbitMQ
{
    public static class Extensions
    {
        public static Task WithCommandHandlerAsync<TCommand>(this IBusClient bus,
            ICommandHandler<TCommand> handler) where TCommand : ICommand
            => bus.SubscribeAsync<TCommand>(msg => handler.HandleAsync(msg),
                ctx => ctx.UseConsumerConfiguration(cfg => 
                cfg.FromDeclaredQueue(q => q.WithName(GetQueueName<TCommand>()))));

    
        private static string GetQueueName<T>()
            => $"{Assembly.GetEntryAssembly().GetName()}/{typeof(T).Name}";

        public static void AddRabbitMq(this IServiceCollection services, IConfiguration configuration)
        {
            var options = new RabbitMqOptions();
            var section = configuration.GetSection("rabbitmq");
            section.Bind(options);
            var client = RawRabbitFactory.CreateSingleton(new RawRabbitOptions
            {
                ClientConfiguration = options
            });
            services.AddSingleton<IBusClient>(_ => client);
        }
    }
}
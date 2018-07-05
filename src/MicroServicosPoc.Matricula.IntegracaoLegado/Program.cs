using MicroServicosPoc.Matricula.Domain.Commands;
using MicroServicosPoc.Shared.Services;

namespace MicroServicosPoc.Matricula.IntegracaoLegado
{
    public class Program
    {
        public static void Main(string[] args)
        {
             ServiceHost.Create<Startup>(args)
                .UseRabbitMq()
                .SubscribeToCommand<IntegrarMatriculaCommand>()
                .Build()
                .Run();
        }
    }
}

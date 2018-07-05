using MicroServicosPoc.Shared.Services;

namespace MicroServicosPoc.Matricula.Cobranca.Tasker
{
    public class Program
    {
         public static void Main(string[] args)
        {
             ServiceHost.Create<Startup>(args)
                .Build()
                .Run();

            
        }
    }
}

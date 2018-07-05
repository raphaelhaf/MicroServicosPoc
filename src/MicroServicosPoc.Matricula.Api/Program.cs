using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using MicroServicosPoc.Shared.Services;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace MicroServicosPoc.Matricula.Api
{
    public class Program
    {
         public static void Main(string[] args)
        {
             ServiceHost.Create<Startup>(args)
                .UseRabbitMq()
                .Build()
                .Run();
        }
    }
}

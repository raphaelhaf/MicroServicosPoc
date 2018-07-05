using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MicroServicosPoc.Matricula.Domain.Handlers;
using MicroServicosPoc.Matricula.Domain.Repositories;
using MicroServicosPoc.Matricula.Infra.DataContext;
using MicroServicosPoc.Matricula.Infra.Repositories;
using MicroServicosPoc.Shared.Auth;
using MicroServicosPoc.Shared.RabbitMQ;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace MicroServicosPoc.Matricula.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddJwt(Configuration);
            services.AddRabbitMq(Configuration);
            services.AddScoped<MicroServicosPocMatriculaDataContext, MicroServicosPocMatriculaDataContext>();
            services.AddTransient<IMatriculaRepository, MatriculaRepository>();
            services.AddTransient<MatriculaHandler, MatriculaHandler>();
            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            
            app.UseAuthentication();
            app.UseMvc();
        }
    }
}

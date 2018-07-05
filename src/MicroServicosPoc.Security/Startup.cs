using MicroServicosPoc.Security.Domain.Repositories;
using MicroServicosPoc.Security.Domain.Services;
using MicroServicosPoc.Security.Handlers;
using MicroServicosPoc.Security.Repositories;
using MicroServicosPoc.Security.Repositories.DataContext;
using MicroServicosPoc.Shared.Auth;
using MicroServicosPoc.Shared.RabbitMQ;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MicroServicosPoc.Security
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
            services.AddLogging();
            services.AddJwt(Configuration);
            services.AddScoped<MicroServicosPocSecurityDataContext, MicroServicosPocSecurityDataContext>();
            services.AddTransient<IUsuarioRepository, UsuarioRepository>();
            services.AddTransient<UsuarioHandler, UsuarioHandler>();
            services.AddSingleton<IEncrypter, Encrypter>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }
}

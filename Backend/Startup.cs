using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

//Instalamos o Entity Framework- vai pegar nosso banco e geral os models
//dotnet tool install --global dotnet-ef

//baixamos o pacote sqlserver do entity
//dotnet add package Microsoft.EntityFrameworkCore.SqlServer

//baixamos o pacote que irá escrever nossos códigos(no model)
//dotnet add package Microsoft.EntityFrameworkCore.Design

//Testamos se os pacotes foram instalados
//dotnet restore

//Testamos a instalaçao do entity framework
//dotnet ef

//Codigo que criara o nosso Contexto da base de dados e nossos models(conexão da nossa aplicação com o banco)
//dotnet ef dbcontext scaffold "Server= DESKTOP-444V57F\SQLEXPRESS; Database=Gufos; User Id= sa; Password=132" Microsoft.EntityFrameworkCore.SqlServer -o Models -d
//STRING DE CONEXÃO
//contexto do banco de dados
//-o cria diretorio do models/ -d data anotation(inclui no models as anotações do banco no model)

namespace Backend
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
            services.AddControllersWithViews().AddNewtonsoftJson(opt => opt.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}

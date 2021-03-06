using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using SisClientes.Data;
using SisClientes.HttpClients;
using SisClientes.Services;
using System;

namespace SisClientes
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
            services.AddDbContext<SisClientesDbContext>(opts => opts.UseSqlServer(Configuration.GetConnectionString("SisClientesConnection")));
            services.AddScoped<CidadesService, CidadesService>();
            services.AddScoped<ClientesService, ClientesService>();
            services.AddScoped<EnderecosService, EnderecosService>();

            services.AddHttpClient<CepApiClient>(client =>
            {
                client.BaseAddress = new Uri("https://viacep.com.br/ws/");
            });

            services.AddMvc(options =>
            {
                options.SuppressAsyncSuffixInActionNames = false;
            });

            services.AddControllers();
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "SisClientes", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "SisClientes v1"));
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

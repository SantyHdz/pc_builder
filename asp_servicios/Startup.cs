using asp_servicios.Controllers;
using lib_aplicaciones.Implementaciones;
using lib_aplicaciones.Interfaces;
using lib_repositorios;
using lib_repositorios.Implementaciones;
using lib_repositorios.Interfaces;
using Microsoft.AspNetCore.Server.Kestrel.Core;

namespace asp_servicios
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public static IConfiguration? Configuration { set; get; }

        public void ConfigureServices(WebApplicationBuilder builder, IServiceCollection services)
        {
            services.Configure<KestrelServerOptions>(x => { x.AllowSynchronousIO = true; });
            services.Configure<IISServerOptions>(x => { x.AllowSynchronousIO = true; });

            services.AddControllers();
            services.AddEndpointsApiExplorer();
            // services.AddSwaggerGen();

            // Repositorios
            services.AddScoped<IConexion, ConexionEF3.Conexion>();

            // Aplicaciones
            services.AddScoped<IUsuariosAplicacion, UsuariosAplicacion>();
            services.AddScoped<IRolesAplicacion, RolesAplicacion>();
            services.AddScoped<IAuditoriaAplicacion, AuditoriaAplicacion>();
            services.AddScoped<IBuildsAplicacion, BuildsAplicacion>();
            services.AddScoped<ICompatibilidadAplicacion, CompatibilidadAplicacion>();
            services.AddScoped<IComponentesAplicacion, ComponentesAplicacion>();
            services.AddScoped<ITiposComponentesAplicacion, TiposComponentesAplicacion>();
            services.AddScoped<IComponentesEnBuildAplicacion, ComponentesEnBuildAplicacion>();

            // Controladores
            services.AddScoped<TokenController, TokenController>();

            services.AddCors(options =>
            {
                options.AddPolicy("AllowFrontend", policy =>
                {
                    policy.WithOrigins("http://localhost:5173")
                          .AllowAnyHeader()                   
                          .AllowAnyMethod();                   
                });
            });
        }

        public void Configure(WebApplication app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                // app.UseSwagger();
                // app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseRouting();
            
            app.UseCors("AllowFrontend");

            app.UseAuthorization();

            app.MapControllers();
        }
    }
}

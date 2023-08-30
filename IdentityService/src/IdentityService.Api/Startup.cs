using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ControleDeAutenticaçao.Filters;
using ControleDeAutenticaçao.Middleware;
using ControleDeAutenticaçao.Services;

namespace ControleDeAutenticaçao
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            
            // Configuração de injeção de dependência
            services.AddScoped<IAuthenticationService, AuthenticationService>();
            services.AddScoped<IAuthorizationService, AuthorizationService>();
            // Adicione outros serviços necessários

            // Configuração do CORS (se necessário)
            services.AddCors();

            // Configuração do Swagger (se necessário)
            services.AddSwaggerGen();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            // Middleware para registrar informações de solicitação
            app.UseMiddleware<RequestLoggingMiddleware>();

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            
            // Configuração do CORS (se necessário)
            app.UseCors(builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

            app.UseRouting();
            
            // Autenticação e autorização
            app.UseAuthentication();
            app.UseAuthorization();

            // Middleware de exceção personalizado
            app.UseMiddleware<ExceptionFilter>();

            // Configuração do Swagger (se necessário)
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "API V1"));

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}

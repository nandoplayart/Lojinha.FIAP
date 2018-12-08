using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Lojinha.FIAP.Core.Services;
using Lojinha.FIAP.Infrastructure.Mappings;
using Lojinha.FIAP.Infrastructure.Redis;
using Lojinha.FIAP.Infrastructure.Storage;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.AzureAD.UI;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Lojinha.FIAP
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
                services.ConfigureApplicationCookie(options =>
                {
                    options.Cookie.SameSite = Microsoft.AspNetCore.Http.SameSiteMode.None;
                });
                services.AddAuthentication(AzureADDefaults.AuthenticationScheme)
                    .AddAzureAD(options => Configuration.Bind("AzureAd", options));

            //injeção de dependencia
            
            services.AddSingleton<IRedisCache, RedisCache>();
            services.AddScoped<IProdutoServices, ProdutoServices>();
            services.AddScoped<IAzureStorage, AzureStorage>();
            services.AddScoped<ICarrinhoServices, CarrinhoServices>();
            

            Mapper.Initialize(options => options.AddProfile<ProdutoProfile>());

            services.AddAutoMapper();
            services.AddMvc();
            }

            // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
            public void Configure(IApplicationBuilder app, IHostingEnvironment env)
            {
                if (env.IsDevelopment())
                {
                    app.UseBrowserLink();
                    app.UseDeveloperExceptionPage();
                }
                else
                {
                    app.UseExceptionHandler("/Home/Error");
                }

                app.UseStaticFiles();
                app.UseAuthentication(); //adicionei esta linha
                app.UseMvc(routes =>
                {
                    routes.MapRoute(
                        name: "default",
                        template: "{controller=Home}/{action=Index}/{id?}");
                });

            }
        }
    }

using ApplicationCore.Interfaces.Base;
using AutoMapper;
using DinkToPdf;
using DinkToPdf.Contracts;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.Loader;
using System.Text;
using WebAPI.Extensions;

namespace WebAPI
{
    public class Startup
    {
        private IServiceCollection _services;
        private IHostingEnvironment _hostingEnvironment;

        public Startup(IHostingEnvironment env)
        {
            _hostingEnvironment = env;
            var configuration = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();

            var builder = configuration.Build();

            Configuration = builder;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            // bool isWindows = RuntimeInformation.IsOSPlatform(OSPlatform.Windows);
            // if (isWindows)
            // {
            //     var wkHtmlToPdfPath = Path.Combine(_hostingEnvironment.ContentRootPath, $"wkhtmltox\\libwkhtmltox");
            //     CustomAssemblyLoadContext context = new CustomAssemblyLoadContext();
            //     context.LoadUnmanagedLibrary(wkHtmlToPdfPath);
            // }

            // // Add converter to DI
            // services.AddSingleton(typeof(IConverter), new SynchronizedConverter(new PdfTools()));

            services.AddMvc(opt =>
            {
                opt.Filters.Add(typeof(ValidatorActionFilter));
            }).AddFluentValidation(fvc =>
                        fvc.RegisterValidatorsFromAssemblyContaining<Startup>());

            services.AddResponseCaching();
            services.AddMemoryCache();
            services.AddSingleton(Configuration);
            services.AddCorsCustom();
            services.AddAutoMapper();
            services.AddMvcCustom(Configuration);
            services.AddAuthenticationCustom(Configuration);
            services.AddSwaggerDocumentation();
            
            services.AddMvc(options =>
            {
                options.Filters.Add(new CustomExceptionFilter());
            });
            services.AddResponseCompression();
            _services = services;
            services.AddSingleton<IEmailConfiguration>(Configuration.GetSection("EmailConfiguration").Get<EmailConfiguration>());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                ListAllRegisteredServices(app);
                app.UseDatabaseErrorPage();
                app.UseSwaggerDocumentation();
            }

            app.UseExceptionCustom(env);
            app.UseStaticFiles();
            app.UseResponseCaching();
            app.UseCorsCustom();            
            app.UseSpaCustom(env);
            app.UseAuthentication();
            app.UseResponseCompression();
            app.UseMvc();
        }

        private void ListAllRegisteredServices(IApplicationBuilder app)
        {
            app.Map("/allservices", builder => builder.Run(async context =>
            {
                var sb = new StringBuilder();
                sb.Append("<h1>All Services</h1>");
                sb.Append("<table><thead>");
                sb.Append("<tr><th>Type</th><th>Lifetime</th><th>Instance</th></tr>");
                sb.Append("</thead><tbody>");
                foreach (var svc in _services)
                {
                    sb.Append("<tr>");
                    sb.Append($"<td>{svc.ServiceType.FullName}</td>");
                    sb.Append($"<td>{svc.Lifetime}</td>");
                    sb.Append($"<td>{svc.ImplementationType?.FullName}</td>");
                    sb.Append("</tr>");
                }
                sb.Append("</tbody></table>");
                await context.Response.WriteAsync(sb.ToString());
            }));
        }
    }
}

internal class CustomAssemblyLoadContext : AssemblyLoadContext
{
    public IntPtr LoadUnmanagedLibrary(string absolutePath)
    {
        return LoadUnmanagedDll(absolutePath);
    }

    protected override IntPtr LoadUnmanagedDll(String unmanagedDllName)
    {
        return LoadUnmanagedDllFromPath(unmanagedDllName);
    }

    protected override Assembly Load(AssemblyName assemblyName)
    {
        throw new NotImplementedException();
    }
}
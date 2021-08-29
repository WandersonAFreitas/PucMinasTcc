using System.Net;
using System.Net.Mime;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using WebAPI.Helpers;
using WebAPI.Middlewares;

namespace WebAPI.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static void UseCorsCustom(this IApplicationBuilder application)
        {
            application.UseCors(Constants.Cors);
        }

        public static void UseExceptionCustom(this IApplicationBuilder application, IHostingEnvironment environment)
        {
            application.UseDeveloperExceptionPage();
            application.UseDatabaseErrorPage();

            if (environment.IsDevelopment())
            {
                application.UseDeveloperExceptionPage();
                application.UseDatabaseErrorPage();
            }
            else
            {
                UseExceptionHandler(application);
            }

            application.UseExceptionMiddleware();
        }

        private static void UseExceptionHandler(IApplicationBuilder application)
        {
            application.UseExceptionHandler(cfg => {
                cfg.Run(async ctx => {
                    ctx.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    ctx.Response.ContentType = MediaTypeNames.Text.Html;

                    var exceptionHandlerFeature = ctx.Features.Get<IExceptionHandlerFeature>();

                    if (exceptionHandlerFeature != null) { /* Log */ }

                    await ctx.Response.WriteAsync(string.Empty).ConfigureAwait(false);
                });
            });
        }

        private static void UseExceptionMiddleware(this IApplicationBuilder application)
        {
            application.UseMiddleware<ExceptionMiddleware>();
        }

        public static void UseSpaCustom(this IApplicationBuilder application, IHostingEnvironment environment)
        {
            if (environment.IsDevelopment())
            {
                application.UseDeveloperExceptionPage();
            }
        }
    }
}
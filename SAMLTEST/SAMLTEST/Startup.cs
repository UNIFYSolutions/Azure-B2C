using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace SAMLTEST
{
    public class Startup
    {

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                // Use Developer page error handling for development.
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                // For Production use Custom error handling and ensure it uses HTTPS
                app.UseExceptionHandler("/Error");
                var options = new RewriteOptions().AddRedirectToHttpsPermanent();
                app.UseRewriter(options);
            }

            // Use Status Code error handling to our custom page.
            app.UseStatusCodePagesWithRedirects("/Error?StatusCode={0}");
            // For the wwwroot folder
            app.UseStaticFiles(); 

            app.UseMvc();
        }
        
    }
}

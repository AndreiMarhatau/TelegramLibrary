using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sample
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
            services.AddControllers()
                .AddNewtonsoftJson();

            services.AddSingleton<TelegramLibrary.ITelegramService>(services =>
                new TelegramLibrary.Builders.TelegramServiceBuilder()
                // This sets the webhook url where telegram will send updates
                .UseWebHookUrl("https://679d-46-56-206-84.eu.ngrok.io/telegram/update")
                // This is the token of bot that you will use
                .UseToken("1654710052:AAGalH1UTr3VgT94ylvIqWdSRbc-WA0vFDg")
                // This is the registering of the repository for saving users with ids and windows
                .UseRepository(() => new Sample.Models.UserRepositoryMock())
                // This returns a builder for constructing global handlers working in all windows
                .UseMainControls()
                    .UseCommandControl("/start", (o, e) =>
                    {
                        e.TelegramInteractor.SendStartWindow();
                    })
                    // To return to previous builder you should call method .Save...()
                    .SaveControls()
                // Creating initial window (initial because it's the first call of .UseWindow()
                // To create others just call it as it's here)
                .UseWindow(new Sample.Models.MainWindow())
                    // Here you can create a message with text and (if you want) other controls (buttons, etc.)
                        .UseMessage()
                            .UseText("You're in main window!")
                        .SaveMessage()
                    .SaveWindow()
                // It creates the service with specified above options
                .GetService()
                .Result
            );
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            // app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Telegram}/{action=Up}");
            });
        }
    }
}

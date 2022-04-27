using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TelegramLibrary.Models.WindowControls;

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
                // Tip: This sets the webhook url where telegram will send updates
                .UseWebHookUrl("https://host.com/telegram/update")
                // Tip: This is the token of bot that you will use
                .UseToken("...")
                // Tip: By default the library uses in-memory repository of users
                // You can change this flow by using this method and class inherited from TelegramLibrary.Repositories.IUserRepository
                //.UseRepository(() => getUserRepository)
                // Tip: This returns a builder for constructing global handlers working in all windows
                .UseMainControls()
                    .UseCommandControl("/start", (o, e) =>
                    {
                        e.TelegramInteractor.SendStartWindow();
                    })
                // Tip: To return to previous builder you should call method .Save...()
                .SaveControls()
                // Tip: This handler will be called if there are no controls that can handle the request
                .UseDefaultHandler((o, e) => e.TelegramInteractor.SendText("This is the main default handler"))
                // Tip: Creating initial window (initial because it's the first call of .UseWindow()
                // Tip: To create others just call it as it's here)
                .UseWindow("MainWindow")
                        // Tip: Here you can create a message with text and (if you want) other controls (buttons, etc.)
                    .UseMessage()
                        .UseText("You're in main window!")
                        .UseKeyboardControls()
                            .CreateRow()
                                .UseKeyboardButtonControl("Phone number", (o, e) => { })
                                    .RequestPhoneNumber()
                                .UseKeyboardButtonControl("Second window", (o, e) => e.TelegramInteractor.SendWindow("Second window"))
                        .SaveControls()
                    .SaveMessage()
                    .UseWindowControls()
                        .UseContactInputControl(async (o, e) =>
                        {
                            await e.TelegramInteractor.SendText($"The phone number has been received: {e.TelegramInteractor.Message.Contact.PhoneNumber}");
                            await e.TelegramInteractor.SendText($"The user id: {e.TelegramInteractor.Message.Contact.UserId}");
                        })
                    .SaveControls()
                    // Tip: This handler will be called if there are no controls that can handle the request, except global default handler
                    // And the user is in the current window
                    .UseDefaultHandler((o, e) => e.TelegramInteractor.SendText("This is the default handler of main window"))
                .SaveWindow()
                .UseWindow("Second window")
                    .UseMessage()
                        .UseText("Second window")
                        .UseCallbackControls()
                            // Here you can add controls without calling .CreateRow - the first row will be created automatically
                            .UseCallbackButtonControl("MainWindow", (o, e) => e.TelegramInteractor.SendStartWindow())
                        .SaveControls()
                    .SaveMessage()
                    // Tip: This handler will be called if there are no controls that can handle the request, except global default handler
                    // And the user is in the current window
                    .UseDefaultHandler((o, e) => e.TelegramInteractor.SendText("DefaultHandler of second window"))
                .SaveWindow()
                // Tip: It creates the service with specified above options
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
                    pattern: "{controller=Telegram}/{action=WarmUp}");
            });

            // Tip: Warm up the app in order to create required services and register webhook
            Sample.Extensions.WarmUpper.WarmUpApp();
        }
    }
}

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using PhotoMarathon.Data;
using PhotoMarathon.Data.Entities;
using PhotoMarathon.Data.Infrastructure;
using PhotoMarathon.Data.Repository;
using PhotoMarathon.Service.Services;
using System.Globalization;

namespace PhotoMarathon
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();

            if (env.IsDevelopment())
            {
                // This will push telemetry data through Application Insights pipeline faster, allowing you to view results immediately.
                builder.AddApplicationInsightsSettings(developerMode: true);
            }
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<BaseDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("PhotoMarathonConnectionString")));

            //services.AddDbContext<AccountDbContext>(options =>
            //   options.UseSqlServer(Configuration.GetConnectionString("PhotoMarathonConnectionString")));

            // Add framework services.
            services.AddApplicationInsightsTelemetry(Configuration);
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddTransient<INewsLetterService, NewsLetterService>();
            services.AddTransient<IGeneralService, GeneralService>();
            services.AddTransient<IAccountService, AccountService>();
            services.AddTransient<IBlogService, BlogService>();
            services.AddTransient<IContactService, ContactService>();
            //Repositories
            services.AddTransient<IEntityBaseRepository<Newsletter>, EntityBaseRepository<Newsletter>>();
            services.AddTransient<IEntityBaseRepository<WorkShop>, EntityBaseRepository<WorkShop>>();
            services.AddTransient<IEntityBaseRepository<Photographer>, EntityBaseRepository<Photographer>>();
            services.AddTransient<IEntityBaseRepository<BlogItem>, EntityBaseRepository<BlogItem>>();
            services.AddTransient<IEntityBaseRepository<RegisterStatus>, EntityBaseRepository<RegisterStatus>>();
            services.AddTransient<IEntityBaseRepository<ContactMessage>, EntityBaseRepository<ContactMessage>>();
            //identity
            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<BaseDbContext>()
                .AddDefaultTokenProviders();
            services.AddMvc();
            services.AddSession();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseApplicationInsightsRequestTelemetry();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseApplicationInsightsExceptionTelemetry();

            app.UseStaticFiles();

            app.UseIdentity();
            app.UseSession();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                LoginPath = "/account/login",

                AuthenticationScheme = "Cookies",
                AutomaticAuthenticate = true,
                AutomaticChallenge = true
            });
            var supportedCultures = new[]
              {
                  new CultureInfo("ro-RO")
              };

            app.UseRequestLocalization(new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture("ro-RO"),
                // Formatting numbers, dates, etc.
                SupportedCultures = supportedCultures,
                // UI strings that we have localized.
                SupportedUICultures = supportedCultures
            });
        }
    }
}

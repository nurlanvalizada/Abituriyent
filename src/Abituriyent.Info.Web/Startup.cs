using Abituriyent.Info.Core.Domain;
using Abituriyent.Info.DataAccess;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Abituriyent.Info.Web.Infrastructure;
using Abituriyent.Info.Web.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace Abituriyent.Info.Web
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile("config.json", optional: true, reloadOnChange: true);

            if (env.IsDevelopment())
            {
                // This will push telemetry data through Application Insights pipeline faster, allowing you to view results immediately.
                builder.AddApplicationInsightsSettings(developerMode: true);
            }

            builder.AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddApplicationInsightsTelemetry(Configuration);

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DataConnection")));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            services.Configure<IISOptions>(options => { options.AutomaticAuthentication = true; });

            // Configure Identity
            services.Configure<IdentityOptions>(options =>
            {
                // User settings
                options.User.RequireUniqueEmail = true;
            });

            services.Configure<AppSecretOptions>(Configuration.GetSection("userSecrets").GetSection("facebook"));

            services.AddMvc(options =>
            {
                //options.SslPort = 44397;
                //options.Filters.Add(new RequireHttpsAttribute());
            });

            //extesion method
            services.BindServiceImplementations();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory,
            ApplicationDbContext authcontext, RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager, IOptions<AppSecretOptions> appSecretOptions)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseApplicationInsightsRequestTelemetry();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStatusCodePages();

            app.UseStatusCodePagesWithRedirects("/Home/Error/{0}");

            app.UseStaticFiles();
            app.UseIdentity();

            //Add external authentication middleware below.
            app.UseFacebookAuthentication(new FacebookOptions
            {
                AppId = appSecretOptions.Value.AppId,
                AppSecret = appSecretOptions.Value.Secret,
                Fields = { "birthday", "first_name", "gender", "last_name", "email", "picture", "hometown" },
                Scope = { "public_profile", "email", "user_birthday", "user_hometown" }
            });

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

           // DbInitializer.Initialize(context);
            DbInitializer.Initialize(authcontext, roleManager).Wait();
        }
    }
}

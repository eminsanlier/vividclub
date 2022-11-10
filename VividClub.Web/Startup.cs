using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PaulMiami.AspNetCore.Mvc.Recaptcha;
using VividClub.Data;
using VividClub.Data.Entities;
using VividClub.Services;
using VividClub.Web.Extensions;
using VividClub.Web.Hub;
using VividClub.Services.Models;

namespace VividClub.Web
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
            services.AddDbContext<VividClubDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")
                ));

            services.AddIdentity<User, IdentityRole>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 2;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;
            })
                .AddEntityFrameworkStores<VividClubDbContext>()
                .AddDefaultTokenProviders();

            IConfiguration config = new ConfigurationBuilder().AddJsonFile("appsettings.json").AddEnvironmentVariables().Build();


            services.AddScoped<IClubRequestService, ClubRequestService>();
            services.AddScoped<IClubService, ClubService>();
            services.AddScoped<ICommentService, CommentService>();
            services.AddScoped<IEventService, EventService>();
            services.AddScoped<IEmailService>(x => ActivatorUtilities.CreateInstance<EmailService>(x, config.GetSection("EmailSettings").Get<EmailSettingsModel>()));
            services.AddScoped<IMessangerService, MessangerService>();
            services.AddScoped<IPhotoService, PhotoService>();
            services.AddScoped<IPostService, PostService>();
            services.AddScoped<IUserService, UserService>();

            services.AddAutoMapper(typeof(Startup).Assembly);

            services.AddSignalR();

            services.AddMvc(options =>
            {
                options.Filters.Add<AutoValidateAntiforgeryTokenAttribute>();
            });

            services.AddRecaptcha(new RecaptchaOptions
            {
                SiteKey = "6LeHeLIaAAAAAHgRy0UaddXU-sYdZ0dHdH1KxMJx",
                SecretKey = "6LeHeLIaAAAAADWQkTWzelWWN8Rc3DPkbTpFzK6u",
                ValidationMessage = "Are you a robot?"
            });
            
            services.AddDatabaseDeveloperPageExceptionFilter();
        }

        public void Configure(IApplicationBuilder app, IHostEnvironment env)
        {
            app.UseDatabaseMigrations();
            
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(routes =>
            {
                routes.MapHub<ChatHub>("/chat");
                
                routes.MapControllerRoute(
                  "areas",
                  "{area:exists}/{controller=Home}/{action=Index}/{id?}");

                routes.MapControllerRoute(
                    "default",
                    "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
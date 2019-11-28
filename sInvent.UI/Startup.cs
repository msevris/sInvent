using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using sInvent.Application;
using sInvent.Database;

namespace sInvent.UI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            _cfg = configuration;
        }

        public IConfiguration _cfg { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(opt => opt.UseSqlServer(_cfg["DefaultConnection"], ma => ma.MigrationsAssembly("sInvent.Database")));

            services.Configure<CookiePolicyOptions>(opt =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                opt.CheckConsentNeeded = context => true;
                opt.MinimumSameSitePolicy = SameSiteMode.None;
            });
            services.AddIdentity<IdentityUser, IdentityRole>(opt =>
             {
                 opt.Password.RequireDigit = false;
                 opt.Password.RequiredLength = 3;
                 opt.Password.RequireNonAlphanumeric = false;
                 opt.Password.RequireUppercase = false;
             })
             .AddEntityFrameworkStores<ApplicationDbContext>();

            services.ConfigureApplicationCookie(opt =>
            {
                opt.LoginPath = "/Accounts/Login";
            });

            services.AddAuthorization(opt =>
            {
                opt.AddPolicy("Admin", policy => policy.RequireClaim("Role", "Admin"));
                opt.AddPolicy("Manager", policy => policy.RequireAssertion(cond =>
                     cond.User.HasClaim("Role", "Manager") || cond.User.HasClaim("Role", "Admin")));
            });
            services.AddMvc()
                .AddRazorPagesOptions(opt =>
                {
                    opt.Conventions.AuthorizeFolder("/Admin");
                    opt.Conventions.AuthorizePage("/Admin/ConfigureUsers", "Admin");
                })
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddAplicationServices();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseAuthentication();
            app.UseMvcWithDefaultRoute();
        }
    }
}

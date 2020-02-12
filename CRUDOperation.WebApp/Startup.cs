using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

using CRUDOperation.Configurations;
using AutoMapper;
using System;
using Microsoft.AspNetCore.Identity;
using CRUDOperation.DatabaseContext;
using CRUDOperation.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace CRUDOperation.WebApp
{
    public class Startup
    {
        
        public void ConfigureServices(IServiceCollection services)
        {
            /* Using for Regisration user...login and logout */

            //services.AddDbContext<CRUDOperationDbContext>(options =>
            //options.UseSqlServer("Server=(local);Database=EcommerceWebApplication; Integrated Security=true"));

            services.AddDbContext<CRUDOperationDbContext>();

            services.AddIdentity<ApplicationUser,IdentityRole>()
                .AddRoles<IdentityRole>()
                .AddClaimsPrincipalFactory<UserClaimsPrincipalFactory<ApplicationUser,IdentityRole>>()
                .AddDefaultUI()
                .AddEntityFrameworkStores<CRUDOperationDbContext>()
                .AddDefaultTokenProviders();
            //services.AddScoped<RoleManager<IdentityRole>>();

            services.AddAuthentication()
                    .AddCookie(options =>
                    {
                        options.LoginPath = "/Account/Unauthorized/";
                        options.AccessDeniedPath = "/Account/Forbidden/";
                    })

                    .AddJwtBearer(options => {
                    options.Audience = "http://localhost:53605/";
                    options.Authority = "http://localhost:53605/";
                });
            /*--------End--------*/

            //services.ServicesConfigurations();
            ServicesConfigurations.ConfigureServices(services);
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());


            /* Using for Regisration user...login and logout */
            services.Configure<IdentityOptions>(options =>
            {
                // Password settings.
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 6;
                //options.Password.RequiredUniqueChars = 1;

                // Lockout settings.
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = true;

                // User settings.
                options.User.AllowedUserNameCharacters =
                "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                options.User.RequireUniqueEmail = false;
            });

            services.ConfigureApplicationCookie(options =>
            {
                // Cookie settings
                options.Cookie.HttpOnly = true;
                options.ExpireTimeSpan = TimeSpan.FromMinutes(5);

                options.LoginPath = "/Identity/Account/Login";
                options.LogoutPath = "/Identity/Account/Logout";
                options.AccessDeniedPath = "/Identity/Account/AccessDenied";
                options.SlidingExpiration = true;
            });

            services.AddAuthentication()
                .AddGoogle(options =>
                {
                    options.ClientId = "884097505482-j0tufq4fveclaqjalqhnfhj3hecl974o.apps.googleusercontent.com";
                    options.ClientSecret = "rvCRqaEYDZjDDlm9IhgWRpq0";
                })
                .AddFacebook(options =>
                {
                    options.AppId = "492985484738653";
                    options.AppSecret = "5db592e96a8b9e439ec0b8883d1321c6";
                });

            /*-------End-----------*/


            /*-------add Policy for browser accept all CORS(Cross Origin Resource Sharing)-----------*/
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", builder =>
                {
                    builder.AllowAnyHeader();
                    builder.AllowAnyOrigin();
                    builder.AllowAnyMethod();
                });
            });
            /*-------End-----------*/

            services.AddMvc(options =>
            {
                var policy = new AuthorizationPolicyBuilder()
                             .RequireAuthenticatedUser()
                             .Build();
                options.Filters.Add(new AuthorizeFilter(policy));
            })

               .AddJsonOptions(options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
            services.Configure<MvcJsonOptions>(config =>
            {
                config.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            });
            services.AddSession(); //using for AddToCart


            //services.AddTransient<TotalItemService>();
            //services.AddTransient<GetUserService>();
            services.AddMvc(options =>
            {
            //    options.FormatterMappings.SetMediaTypeMappingForFormat
            //         ("xml", MediaTypeHeaderValue.Parse("application/xml"));
            //    options.FormatterMappings.SetMediaTypeMappingForFormat
            //         ("config", MediaTypeHeaderValue.Parse("application/xml"));
            //    options.FormatterMappings.SetMediaTypeMappingForFormat
            //        ("js", MediaTypeHeaderValue.Parse("application/json"));

            })
                     .AddXmlSerializerFormatters()
                     .AddJsonOptions(options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            //services.AddPaging();
        }
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSession(); //using for AddToCart
            app.UseStaticFiles();
            app.UseIdentity(); //using for Identity
            app.UseAuthentication();
            app.UseMvcWithDefaultRoute();

            app.UseCors("AllowAll"); //add Policy for browser accept all CORS(Cross Origin Resource Sharing)


            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "Default",
                    template: "{controller=Product}/{action=ProductListIndex}/{id?}");
            });

        }
    }
}
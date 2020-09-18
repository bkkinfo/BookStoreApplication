using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStoreApplication.Models.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.AzureAD.UI;
using Microsoft.AspNetCore.Authentication.OAuth.Claims;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using System.Security.Claims;

namespace BookStoreApplication
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
            /**     services.Configure<CookiePolicyOptions>(options =>
                  {
                      // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                      options.CheckConsentNeeded = context => true;
                      options.MinimumSameSitePolicy = SameSiteMode.None;
                  });

                  services.AddAuthentication(AzureADDefaults.AuthenticationScheme)
                      .AddAzureAD(options => Configuration.Bind("AzureAd", options));

                  services.Configure<OpenIdConnectOptions>(AzureADDefaults.OpenIdScheme, options =>
                  {
                      options.Authority = options.Authority + "/v2.0/";

                      // Per the code below, this application signs in users in any Work and School
                      // accounts and any Microsoft Personal Accounts.
                      // If you want to direct Azure AD to restrict the users that can sign-in, change 
                      // the tenant value of the appsettings.json file in the following way:
                      // - only Work and School accounts => 'organizations'
                      // - only Microsoft Personal accounts => 'consumers'
                      // - Work and School and Personal accounts => 'common'

                      // If you want to restrict the users that can sign-in to only one tenant
                      // set the tenant value in the appsettings.json file to the tenant ID of this
                      // organization, and set ValidateIssuer below to true.

                      // If you want to restrict the users that can sign-in to several organizations
                      // Set the tenant value in the appsettings.json file to 'organizations', set
                      // ValidateIssuer, above to 'true', and add the issuers you want to accept to the
                      // options.TokenValidationParameters.ValidIssuers collection
                      options.TokenValidationParameters.ValidateIssuer = false;
                  }); **/
            services.AddHttpClient();

            services.AddControllersWithViews();
            services.AddSession(options =>
            {
                // Set a short timeout for easy testing.
                options.IdleTimeout = TimeSpan.FromMinutes(10);
                options.Cookie.HttpOnly = true;
                // Make the session cookie essential
                options.Cookie.IsEssential = true;
            });

            services.AddControllersWithViews();
            services.AddDbContext<LibraryDbContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("DevConnection")));
            /**   services.AddMvc(options =>
               {
                   var policy = new AuthorizationPolicyBuilder()
                       .RequireAuthenticatedUser()
                       .Build();
                   options.Filters.Add(new AuthorizeFilter(policy));
               })
               .SetCompatibilityVersion(CompatibilityVersion.Version_3_0);   **/
            services.AddAuthentication(options =>
            {
                options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
            }).AddCookie(CookieAuthenticationDefaults.AuthenticationScheme)
               .AddOpenIdConnect(OpenIdConnectDefaults.AuthenticationScheme, options =>
               {
                   options.SignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                   options.Authority = "https://login.microsoftonline.com/cf4fb524-5b71-4254-8071-b7d5899e935a/v2.0";
                   options.ClientId = "276c3479-0462-4d72-bbb6-c5daf4d4aaf1";
                   options.ClientSecret = "0GfL0Dh_a_.rWB2zbJH3w.e_j5QSP25w~p";
                   //options.ResponseType = "id_token";
                   options.ResponseType = "code";
                   options.Scope.Add("api://276c3479-0462-4d72-bbb6-c5daf4d4aaf1/FullAccess");
                   options.SaveTokens = true;
               }
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
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}

using System;
using System.Collections.Generic;
using System.Globalization;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using TCRB.BLL;
using TCRB.DAL;
using TCRB.DAL.Model.Appsetting;
using TCRB.WEB.Helpers;
using TCRB.WEB.ModelBinder;

namespace TCRB.WEB
{
    public class Startup
    {
        public ILoggerFactory LoggerFactory;
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration, ILoggerFactory loggerFactory)
        {
            Configuration = configuration;
            LoggerFactory = loggerFactory;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddOptions();
            services.AddControllersWithViews();
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddLogging(builder =>
            {
                builder.AddFilter("Microsoft", LogLevel.None).AddFilter(nameof(System), LogLevel.Warning);
            });
            services.Configure<AppsittingModel>(Configuration.GetSection("AppSettings"));
            services.AddDbContext<TCRBDBContext>();
            //services.AddScoped(typeof(ILoggerHelper<>), typeof(LoggerHelper<>));
            services.AddScoped<ConfigurationDataService>();
            services.AddScoped<UserLogin>();
            services.AddHttpContextAccessor();
            services.AddScoped<IDataAccessWrapper, DataAccessWrapper>();
            services.Configure<RequestLocalizationOptions>(options =>
            {
                options.RequestCultureProviders.Clear();
                options.DefaultRequestCulture = new RequestCulture("en-GB");
                options.SupportedUICultures = new List<CultureInfo> {
                    new CultureInfo("th-TH"),
                    new CultureInfo("en-GB"),
                    new CultureInfo("en-US"),
                    new CultureInfo("en-AU")
                };
                options.SupportedCultures = new List<CultureInfo> {
                    new CultureInfo("th-TH"),
                    new CultureInfo("en-GB"),
                    new CultureInfo("en-US"),
                    new CultureInfo("en-AU")
                };
            });
            services.AddAutoMapper(typeof(Startup));
            services.AddControllersWithViews();
            services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.PropertyNamingPolicy = null;
                options.JsonSerializerOptions.Converters.Add(new CustomJsonConverterDateTime());
            });
            services.AddMvc(options =>
            {
                options.ModelBinderProviders.Insert(0, new ModelBinderProvider(LoggerFactory));
            });
            services.AddScoped<CustomCookieAuthenticationEvents>();
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options =>
            {
                options.Cookie.IsEssential = true;
                options.SlidingExpiration = true;
                //options.Cookie.Name = ".AspNetCore.Cookies";
                //options.Cookie.Name = Guid.NewGuid().ToString();
                options.ExpireTimeSpan = TimeSpan.FromSeconds(30);
                options.LoginPath = new PathString("/Login/Index");
                options.LogoutPath = new PathString("/Login/Logout");
                options.Cookie.Path = "/";
                options.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;
                options.Cookie.HttpOnly = true;
                options.EventsType = typeof(CustomCookieAuthenticationEvents);
            });

#if DEBUG
            services.AddMvc().AddControllersAsServices().AddRazorRuntimeCompilation().SetCompatibilityVersion(CompatibilityVersion.Version_3_0);
#endif
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_3_0);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseCookiePolicy();
            app.UseAuthentication();
            app.UseResponseCaching();
            app.UseRequestLocalization();
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthorization();
            System.Web.HttpContext.Configure(app.ApplicationServices.GetRequiredService<IHttpContextAccessor>());

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(name: "default", pattern: "{controller=Login}/{action=Index}/{id?}");
            });
        }
    }
}

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Serialization;
using Pharmix.Web.Services;
using Pharmix.Data.Entities.Context;
using Pharmix.Web.Entities;
using Pharmix.Web.Services.Mappers;
using Pharmix.Web.Services.Repositories;
using Pharmix.Web.WebHub;
using Microsoft.AspNetCore.SignalR.Core;

//using Pharmix.Web.WebHub;

namespace Pharmix.Web
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
            services.AddDbContext<PharmixEntityContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<PharmixEntityContext>().AddDefaultTokenProviders();
            
            // Add application services.
            services.AddTransient<IEmailSender, EmailSender>();
            services.AddTransient<IRepository, Repository>();

            services.AddTransient<ICustomerService, CustomerService>();
            services.AddTransient<IIsolatorService, IsolatorService>();
            services.AddTransient<IShiftService, ShiftService>();
            services.AddTransient<IProductionService, ProductionService>();
            services.AddTransient<IIntegrationOrderService, IntegrationOrderService>();
            services.AddTransient<ICacheService, CacheService>();
            services.AddTransient<IAuditInfoService, AuditInfoService>();

            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IModuleService, ModuleService>();
            services.AddTransient<IRolePermissionService, RolePermissionService>();
            services.AddTransient<ISiteService, SiteService>();
            services.AddTransient<ITrustService, TrustService>();
            services.AddTransient<IPermissionGroupService, PermissionGroupService>();
            services.AddTransient<ILookupService, LookupService>();
            services.AddTransient<IUsedProdService, UsedProdService>();

            services.AddTransient<IPatientService, PatientService>();
            services.AddTransient<IBusinessService, BusinessService>();

            services.AddTransient<IPackagePlanService, PackagePlanService>();
            services.AddTransient<IBusiness_SubscriptionService, Business_SubscriptionService>();
            services.AddSingleton<IPaypalServices, PaypalServices>();
            services.Configure<ConfigAuthOptions>(Configuration);

            services.AddMemoryCache();

            services.AddMvc().AddJsonOptions(options => options.SerializerSettings.ContractResolver = new DefaultContractResolver()); ;
            AutoMapperBootStrapper.RegisterMappings();
            services.AddSignalR();
            services.AddSingleton(typeof(IUserIdProvider), typeof(CustomUserIdProvider));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseAuthentication();

            app.UseSignalR(routes =>
            {
                routes.MapHub<ProductionHub>("/productionhub");
            });
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
                
            });
             
        }
    }
}

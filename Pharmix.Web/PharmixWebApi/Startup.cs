using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using PharmixWebApi.Context;
using PharmixWebApi.Model;
using PharmixWebApi.Repository;
using PharmixWebApi.Repository.Interface;

namespace PharmixWebApi
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

            services.AddDbContext<ApplicationContext>(opts => opts.UseSqlServer(Configuration["ConnectionStrings:PharmixApplicationDB"], b => b.UseRowNumberForPaging()));
            // services.AddDbContext<ApplicationContext>(options =>
            //options.UseSqlServer(Configuration.GetConnectionString("PharmixApplicationDB")));
            services.AddScoped<IDmdAmpHistoryRepository<Dmd_Amp_History, int>, DmdAmpHistoryRepository>();
            services.AddScoped<IDmdChangeSetDetailsRepository<Dmd_ChangeSetDetails, int>, DmdChangeSetDetailsRepository>();
            services.AddScoped<IUploadedFilesRepository<UploadedFiles, int>, UploadedFilesRepository>();
            services.AddScoped<IDmdBusinessChangeSetDetailsRepository<Dmd_BusinessChangeSetDetails, string>, DmdBusinessChangeSetDetailsRepository>();
            services.AddScoped<IDmd_FTPFileDownloadDetailsRepository<Dmd_FTPFileDownloadDetails, int>, Dmd_FTPFileDownloadDetailsRepository>();
            services.AddScoped<IExportDataToCSVDetailsRepository<ExportDataToCSVDetails, int>, ExportDataToCSVDetailsRepository>();
            services.AddMvc();
            //var connection = Configuration.GetConnectionString("PharmixApplicationDB");
            // UseRowNumberForPaging for Using Skip and Take in .Net Core
            //services.AddDbContext<ApplicationContext>(options => options.UseSqlServer(connection, b => b.UseRowNumberForPaging()));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }
}

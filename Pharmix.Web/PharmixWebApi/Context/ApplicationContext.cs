using PharmixWebApi.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PharmixWebApi.Context
{
    public class ApplicationContext : DbContext, IDisposable
    {
        public ApplicationContext()
        {

        }
        //public ApplicationContext(DbContextOptions opts) : base(opts)
        //{

        //}

        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {

        }

        public DbSet<Dmd_Amp_History> Dmd_Amp_History { get; set; }
        public DbSet<Dmd_Ampp_History> Dmd_Ampp_History { get; set; }
        public DbSet<Dmd_Vmp_History> Dmd_Vmp_History { get; set; }
        public DbSet<Dmd_Vmpp_History> Dmd_Vmpp_History { get; set; }
        public DbSet<Dmd_Vtm_History> Dmd_Vtm_History { get; set; }
        public DbSet<Dmd_ROUTE_History> Dmd_ROUTE_History { get; set; }
        public DbSet<Dmd_Form_History> Dmd_Form_History { get; set; }
        public DbSet<Dmd_SUPPLIER_History> Dmd_SUPPLIER_History { get; set; }
        public DbSet<Dmd_Gtin_History> Dmd_Gtin_History { get; set; }

        public DbSet<Dmd_ChangeSetDetails> Dmd_ChangeSetDetails { get; set; }
        public DbSet<UploadedFiles> UploadedFiles { get; set; }
        public DbSet<Dmd_BusinessChangeSetDetails> Dmd_BusinessChangeSetDetails { get; set; }
        public DbSet<Dmd_FTPFileDownloadDetails> Dmd_FTPFileDownloadDetails { get; set; }
        public DbSet<UniversalSearchResults> UniversalSearchResults { get; set; }
        public DbSet<DmdReferenceCombinedDataset> DmdReferenceCombinedDataset { get; set; }
        public DbSet<ExportDataToCSVDetails> ExportDataToCSVDetails { get; set; }

    }
}

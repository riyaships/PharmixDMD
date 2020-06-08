using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Pharmix.Data.Entities.Context;
using Pharmix.Web.Entities.Context;

namespace Pharmix.Web.Entities
{
    public interface IPharmixEntityContext
    {
        DbSet<Gender> Genders { get; set; }
        DbSet<Patient> Patients { get; set; }
        DbSet<Address> Addresses { get; set; }
        DbSet<Contact> Contacts { get; set; }
        DbSet<Trust> Trusts { get; set; }
        DbSet<TrustAddress> TrustAddresses { get; set; }
        DbSet<TrustContact> TrustContacts { get; set; }
        DbSet<AddressType> AddressTypes { get; set; }

        DbSet<IntegrationOrder> IntegrationOrders { get; set; }
        DbSet<IntegratedSystem> IntegratedSystems { get; set; }
        DbSet<IntegrationOrderProgress> IntegrationOrderProgresses { get; set; }
        DbSet<IntegrationOrderLocation> IntegrationOrderLocations { get; set; }
        DbSet<IntegrationOrderTracking> IntegrationOrderTrackings { get; set; }
        DbSet<OrderTrackingDevice> OrderTrackingDevices { get; set; }
        DbSet<IntegrationOrderClassification> IntegrationOrderClassification { get; set; }

        DbSet<Isolator> Isolators { get; set; }
        DbSet<IsolatorStaffAllocation> IsolatorStaffAllocations { get; set; }
        DbSet<IsolatorShift> IsolatorShifts { get; set; }
        DbSet<IsolatorProcedure> IsolatorProcedures { get; set; }
        DbSet<IsolatorMappedProcedure> IsolatorMappedProcedures { get; set; }

        DbSet<IntegrationOrderPreperation> IntegrationOrderPreperations { get; set; }

        DbSet<SupervisorRequest> SupervisorRequest { get; set; }
        DbSet<SupervisorRequestTracking> SupervisorRequestTracking { get; set; }
        DbSet<SupervisorRequestType> SupervisorRequestType { get; set; }

        //Reminder Models
        DbSet<Reminder> Reminders { get; set; }
        DbSet<ReminderInvitation> ReminderInvitations { get; set; }
        DbSet<ReminderInvitationMember> ReminderInvitationMembers { get; set; }
        DbSet<ReminderProgress> ReminderProgresses { get; set; }
        DbSet<ReminderProgressStatus> ReminderProgressStatuses { get; set; }

        //Used products
        DbSet<UsedProduct> UsedProducts { get; set; }
        DbSet<UsedProductStock> UsedProductStocks { get; set; }
        DbSet<StorageLocation> StorageLocations { get; set; }
        DbSet<StockStatus> StockStatuses { get; set; }
        DbSet<StockEvidence> StockEvidences { get; set; }
        DbSet<DmdSupplier> DmdSuppliers { get; set; }
        DbSet<DmdRoute> DmdtRoutes { get; set; }
        DbSet<Vtm> Vtms { get; set; }
        //end

        DbSet<Site> Sites { get; set; }
        DbSet<Module> Modules { get; set; }
        //DbSet<UserModule> UserModules { get; set; }
        DbSet<Permission> Permissions { get; set; }
        DbSet<PermissionGroup> PermissionGroups { get; set; }
        DbSet<RolePermission> RolePermissions { get; set; }
        DbSet<TrustModule> TrustModules { get; set; }
        DbSet<UserTrust> UserTrusts { get; set; }
        DbSet<Group> Groups { get; set; }
        DbSet<AuditInfo> AuditInfos { get; set; }

        DbSet<IdentityUserRole<string>> UserRoles { get; set; }
        DbSet<IdentityRole> Roles { get; set; }
        DbSet<IdentityRoleClaim<string>> RoleClaims { get; set; }
        DbSet<ApplicationUser> Users { get; set; }
        DbSet<IdentityUserClaim<string>> UserClaims { get; set; }
        DbSet<IdentityUserLogin<string>> UserLogins { get; set; }
        DbSet<IdentityUserToken<string>> UserTokens { get; set; }

        //Pregnancy Form1 Tables
        DbSet<Pregnancy> Pregnancy { get; set; }
        DbSet<CommunicationNeed> CommunicationNeed { get; set; }
        DbSet<PlanOfCare> PlanOfCare { get; set; }
        DbSet<MaternityContact> MaternityContact { get; set; }
        DbSet<PrimaryCareContact> PrimaryCareContact { get; set; }
        DbSet<NextOfKin> NextOfKin { get; set; }
        DbSet<EmergencyContact> EmergencyContact { get; set; }

        DbSet<BusinessDetails> BusinessDetails { get; set; }

        DbSet<PackagePlan> PackagePlans { get; set; }
        DbSet<Business_Subscription> Business_Subscription { get; set; }


        DatabaseFacade Database { get; }
        ChangeTracker ChangeTracker { get; }
        IModel Model { get; }
        int SaveChanges();
        int SaveChanges(bool acceptAllChangesOnSuccess);
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
        Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken);
        void Dispose();
        EntityEntry Entry(object entity);

        EntityEntry Add(object entity);
        Task<EntityEntry> AddAsync(object entity, CancellationToken cancellationToken);
        EntityEntry Attach(object entity);
        EntityEntry Update(object entity);
        EntityEntry Remove(object entity);
        void AddRange(params object[] entities);
        Task AddRangeAsync(params object[] entities);
        void AttachRange(params object[] entities);
        void UpdateRange(params object[] entities);
        void RemoveRange(params object[] entities);
        void AddRange(IEnumerable<object> entities);
        Task AddRangeAsync(IEnumerable<object> entities, CancellationToken cancellationToken);
        void AttachRange(IEnumerable<object> entities);
        void UpdateRange(IEnumerable<object> entities);
        void RemoveRange(IEnumerable<object> entities);
        object Find(Type entityType, params object[] keyValues);
        Task<object> FindAsync(Type entityType, params object[] keyValues);
        Task<object> FindAsync(Type entityType, object[] keyValues, CancellationToken cancellationToken);
    }

    public class PharmixEntityContext : IdentityDbContext<ApplicationUser>, IPharmixEntityContext
    {

        private List<string> EntitiesToLog = new List<string>() { "IntegrationOrder" };
        public PharmixEntityContext(DbContextOptions<PharmixEntityContext> options)
            : base(options)
        {
            
        }
        public virtual DbSet<Gender> Genders { get; set; }
        public virtual DbSet<Patient> Patients { get; set; }
        public virtual DbSet<Address> Addresses { get; set; }
        public virtual DbSet<Contact> Contacts { get; set; }
        public virtual DbSet<Trust> Trusts { get; set; }
        public virtual DbSet<TrustAddress> TrustAddresses { get; set; }
        public virtual DbSet<TrustContact> TrustContacts { get; set; }
        public virtual DbSet<AddressType> AddressTypes { get; set; }

        public virtual DbSet<IntegrationOrder> IntegrationOrders { get; set; }
        public virtual DbSet<IntegratedSystem> IntegratedSystems { get; set; }
        public virtual DbSet<IntegrationOrderProgress> IntegrationOrderProgresses { get; set; }
        public virtual DbSet<IntegrationOrderLocation> IntegrationOrderLocations { get; set; }
        public virtual DbSet<IntegrationOrderTracking> IntegrationOrderTrackings { get; set; }
        public virtual DbSet<OrderTrackingDevice> OrderTrackingDevices { get; set; }
        public virtual DbSet<IntegrationOrderClassification> IntegrationOrderClassification { get; set; }


        public DbSet<Isolator> Isolators { get; set; }
        public DbSet<IsolatorStaffAllocation> IsolatorStaffAllocations { get; set; }
        public DbSet<IsolatorShift> IsolatorShifts { get; set; }
        public DbSet<IsolatorProcedure> IsolatorProcedures { get; set; }
        public DbSet<IsolatorMappedProcedure> IsolatorMappedProcedures { get; set; }
        public DbSet<IntegrationOrderPreperation> IntegrationOrderPreperations { get; set; }

        public DbSet<SupervisorRequest> SupervisorRequest { get; set; }
        public DbSet<SupervisorRequestTracking> SupervisorRequestTracking { get; set; }
        public DbSet<SupervisorRequestType> SupervisorRequestType { get; set; }
        public DbSet<Reminder> Reminders { get; set; }
        public DbSet<ReminderInvitation> ReminderInvitations { get; set; }
        public DbSet<ReminderInvitationMember> ReminderInvitationMembers { get; set; }
        public DbSet<ReminderProgress> ReminderProgresses { get; set; }
        public DbSet<ReminderProgressStatus> ReminderProgressStatuses { get; set; }

        //Used products
        public DbSet<UsedProduct> UsedProducts { get; set; }
        public DbSet<UsedProductStock> UsedProductStocks { get; set; }
        public DbSet<StorageLocation> StorageLocations { get; set; }
        public DbSet<StockStatus> StockStatuses { get; set; }
        public DbSet<StockEvidence> StockEvidences { get; set; }
        public DbSet<DmdSupplier> DmdSuppliers { get; set; }
        public DbSet<DmdRoute> DmdtRoutes { get; set; }
        public DbSet<Vtm> Vtms { get; set; }
        //end

        public DbSet<Site> Sites { get; set; }
        public DbSet<Module> Modules { get; set; }
        //public DbSet<UserModule> UserModules { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<PermissionGroup> PermissionGroups { get; set; }
        public DbSet<RolePermission> RolePermissions { get; set; }
        public DbSet<TrustModule> TrustModules { get; set; }
        public DbSet<UserTrust> UserTrusts { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<AuditInfo> AuditInfos { get; set; }

        //Pregnancy Form1 Tables
        public DbSet<Pregnancy> Pregnancy { get; set; }
        public DbSet<CommunicationNeed> CommunicationNeed { get; set; }
        public DbSet<PlanOfCare> PlanOfCare { get; set; }
        public DbSet<MaternityContact> MaternityContact { get; set; }
        public DbSet<PrimaryCareContact> PrimaryCareContact { get; set; }
        public DbSet<NextOfKin> NextOfKin { get; set; }
        public DbSet<EmergencyContact> EmergencyContact { get; set; }
        public DbSet<BusinessDetails> BusinessDetails { get; set; }
        public DbSet<EmailPasswordTokeneDetails> EmailPasswordTokeneDetails { get; set; }

        public DbSet<PackagePlan> PackagePlans { get; set; }
        public DbSet<Business_Subscription> Business_Subscription { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<IdentityUser>().HasKey(l => l.Id);
            modelBuilder.Entity<IdentityRole>().HasKey(r => r.Id);
            //modelBuilder.Entity<Gender>().Property(e => e.IsolatorId).UseSqlServerIdentityColumn();
            modelBuilder.Entity<Patient>().Property(e => e.Id).UseSqlServerIdentityColumn();
            modelBuilder.Entity<Address>().Property(e => e.Id).UseSqlServerIdentityColumn();
            modelBuilder.Entity<Contact>().Property(e => e.Id).UseSqlServerIdentityColumn();
            modelBuilder.Entity<Trust>().Property(e => e.Id).UseSqlServerIdentityColumn();
            modelBuilder.Entity<TrustAddress>().Property(e => e.Id).UseSqlServerIdentityColumn();
            modelBuilder.Entity<TrustContact>().Property(e => e.Id).UseSqlServerIdentityColumn();
            //modelBuilder.Entity<AddressType>().Property(e => e.IsolatorId).UseSqlServerIdentityColumn();
            modelBuilder.Entity<IntegrationOrder>().Property(e => e.Id).UseSqlServerIdentityColumn();
            modelBuilder.Entity<IntegrationOrderTracking>().Property(e => e.Id).UseSqlServerIdentityColumn();
            modelBuilder.Entity<IntegrationOrderLocation>().Property(e => e.Id).UseSqlServerIdentityColumn();
            modelBuilder.Entity<OrderTrackingDevice>().Property(e => e.Id).UseSqlServerIdentityColumn();
            modelBuilder.Entity<IntegrationOrderProgress>().Property(e => e.Id).UseSqlServerIdentityColumn();
            modelBuilder.Entity<IntegratedSystem>().Property(e => e.Id).UseSqlServerIdentityColumn();
            modelBuilder.Entity<SupervisorRequest>().Property(e => e.Id).UseSqlServerIdentityColumn();
            modelBuilder.Entity<SupervisorRequestTracking>().Property(e => e.Id).UseSqlServerIdentityColumn();
            modelBuilder.Entity<SupervisorRequestType>().Property(e => e.Id).UseSqlServerIdentityColumn();
            modelBuilder.Entity<IntegrationOrderClassification>().Property(e => e.Id).UseSqlServerIdentityColumn();

            modelBuilder.Entity<Site>().Property(e => e.Id).UseSqlServerIdentityColumn();

            modelBuilder.Entity<Module>().Property(e => e.Id).UseSqlServerIdentityColumn();
            //modelBuilder.Entity<UserModule>().Property(e => e.Id).UseSqlServerIdentityColumn();

            modelBuilder.Entity<Permission>().Property(e => e.Id).UseSqlServerIdentityColumn();
            //modelBuilder.Entity<Permission>().HasMany<PermissionGroup>();
            //modelBuilder.Entity<Permission>().HasMany<RolePermission>();

            modelBuilder.Entity<PermissionGroup>().Property(e => e.Id).UseSqlServerIdentityColumn();
            //modelBuilder.Entity<PermissionGroup>().HasMany<RolePermission>();

            modelBuilder.Entity<RolePermission>().Property(e => e.Id).UseSqlServerIdentityColumn();
            modelBuilder.Entity<TrustModule>().Property(e => e.Id).UseSqlServerIdentityColumn();

            modelBuilder.Entity<IsolatorStaffAllocation>()
                .HasOne(p => p.ParentStaffAllocation)
                .WithMany()
                .HasForeignKey(p => p.ParentAllocationId);

            modelBuilder.Entity<IntegrationOrderPreperation>()
                .HasOne(p => p.IsolatorStaffAllocation)
                .WithMany()
                .HasForeignKey(p => p.IsolatorStaffAllocationId);

            //Pregnancy Form1 Tables

            modelBuilder.Entity<Pregnancy>().Property(e => e.Id).UseSqlServerIdentityColumn();
            modelBuilder.Entity<CommunicationNeed>().Property(e => e.Id).UseSqlServerIdentityColumn();
            modelBuilder.Entity<PlanOfCare>().Property(e => e.Id).UseSqlServerIdentityColumn();
            modelBuilder.Entity<MaternityContact>().Property(e => e.Id).UseSqlServerIdentityColumn();
            modelBuilder.Entity<PrimaryCareContact>().Property(e => e.Id).UseSqlServerIdentityColumn();
            modelBuilder.Entity<NextOfKin>().Property(e => e.Id).UseSqlServerIdentityColumn();
            modelBuilder.Entity<EmergencyContact>().Property(e => e.Id).UseSqlServerIdentityColumn();
            modelBuilder.Entity<BusinessDetails>().Property(e => e.Id).UseSqlServerIdentityColumn();
            modelBuilder.Entity<PackagePlan>().Property(e => e.Id).UseSqlServerIdentityColumn();
            modelBuilder.Entity<Business_Subscription>().Property(e => e.Id).UseSqlServerIdentityColumn();
            modelBuilder.Entity<EmailPasswordTokeneDetails>().Property(e => e.Id).UseSqlServerIdentityColumn();
        }

        public int SaveChanges(string userId)
        {
            var modifiedEntities = ChangeTracker.Entries().Where(p => p.State != null && p.State == EntityState.Modified).ToList();
            var newEntities = ChangeTracker.Entries().Where(p => p.State != null && p.State == EntityState.Added).ToList();
            //var modifiedEntities = ChangeTracker.Entries().ToList();

            if (modifiedEntities != null && modifiedEntities.Count() > 0)
            {
                foreach (var entity in modifiedEntities)
                {
                    AddAuditInfoEntity(entity, userId);
                }
            }

            var saveResult = base.SaveChanges();

            if (newEntities != null && newEntities.Count() > 0)
            {
                foreach (var entity in newEntities)
                {
                    AddAuditInfoEntity(entity, userId);
                }
                base.SaveChanges();
            }
            

            return saveResult;
        }

        private void Test<T>(T entity, EntityEntry entityEntry)
        {

        }

        private string GetPrimaryKeyValue<T>(T entity, EntityEntry entityEntry)
        {
            var keyName = entityEntry.Context.Model.FindEntityType(entity.GetType()).FindPrimaryKey().Properties
                .Select(x => x.Name).Single();
            return Convert.ToString(entity.GetType().GetProperty(keyName).GetValue(entity, null));
        }

        public override int SaveChanges()
        {

            return base.SaveChanges();
        }

        private void AddAuditInfoEntity(EntityEntry entity, string userId)
        {
            var entityName = entity.Entity.GetType().Name;
            var primaryKey = GetPrimaryKeyValue(entity.Entity, entity);
            if (EntitiesToLog.Contains(entityName))
            {
                var auditInfo = new AuditInfo();
                auditInfo.Info = Newtonsoft.Json.JsonConvert.SerializeObject(entity.Entity);
                auditInfo.CreatedDate = DateTime.Now;
                auditInfo.CreatedUser = userId;
                auditInfo.Name = entityName;
                auditInfo.KeyId = primaryKey;
                auditInfo.Version = AuditInfos.Where(x => x.Name.Equals(entityName) && x.KeyId.Equals(primaryKey)).Count() + 1;
                AuditInfos.Add(auditInfo);
            }
        }
    }
}

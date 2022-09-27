using AUS2.Core.DAL;
using AUS2.Core.Utilities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AUS2.Core.DBObjects
{
    public class ApplicationContext : IdentityDbContext<ApplicationUser, ApplicationRole, string, IdentityUserClaim<string>,
        ApplicationUserRoles, IdentityUserLogin<string>,
        IdentityRoleClaim<string>, IdentityUserToken<string>>
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options): base(options) { }

        public DbSet<Application> Applications { get; set; }
        public DbSet<AppHistory> AppHistories { get; set; }
        public DbSet<Audit> AuditLogs { get; set; }
        public DbSet<ApplicationType> ApplicationTypes { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<ExtraPayment> ExtraPayments { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Facility> Facilities { get; set; }
        public DbSet<FieldOffice> FieldOffices { get; set; }
        public DbSet<FlowStage> flowStages { get; set; }
        public DbSet<InspectionForm> InspectionForms { get; set; }
        public DbSet<LGA> LGAs { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<MissingDocument> MissingDocuments { get; set; }
        public DbSet<Nationality> Nationalities { get; set; }
        public DbSet<OutOfOffice> OutOfOffices { get; set; }
        public DbSet<PhaseDocument> PhaseDocuments { get; set; }
        public DbSet<State> States { get; set; }
        public DbSet<SubmittedDocument> SubmittedDocuments { get; set; }
        public DbSet<UserLogin> UserLogins { get; set; }
        public DbSet<Phase> Phases { get; set; }
        public DbSet<Permit> Permits { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<ApplicationUserRoles>(userRole =>
            {
                userRole.HasKey(ur => new { ur.UserId, ur.RoleId });

                userRole.HasOne(ur => ur.Role)
                    .WithMany(r => r.UserRoles)
                    .HasForeignKey(ur => ur.RoleId)
                    .IsRequired();

                userRole.HasOne(ur => ur.User)
                    .WithMany(r => r.UserRoles)
                    .HasForeignKey(ur => ur.UserId)
                    .IsRequired();
            });

            builder.Entity<Phase>(item =>
            {
                item.Property(o => o.Fee)
                .HasColumnType("decimal(18,2)");

                item.Property(o => o.ServiceCharge)
               .HasColumnType("decimal(18,2)");
            });

            builder.Entity<ExtraPayment>(item =>
            {
                item.Property(e => e.Arrears)
                .HasColumnType("decimal(18,2)");

                item.Property(e => e.TxnAmount)
                .HasColumnType("decimal(18,2)");
            });
        }

        public virtual async Task<int> SaveChangesAsync(string userId = null)
        {
            var result = await base.SaveChangesAsync();
            await OnBeforeSaveChangesAsync(userId);
            return result;
        }

        private async Task OnBeforeSaveChangesAsync(string userId)
        {
            ChangeTracker.DetectChanges();
            var auditEntries = new List<AuditEntry>();
            foreach (var entry in ChangeTracker.Entries())
            {
                if (entry.Entity is Audit || entry.State.Equals(EntityState.Detached) || entry.State.Equals(EntityState.Unchanged))
                    continue;
                var auditEntry = new AuditEntry(entry);
                auditEntry.TableName = entry.Entity.GetType().Name;
                auditEntry.UserId = userId;
                auditEntries.Add(auditEntry);
                foreach (var property in entry.Properties)
                {
                    string propName = property.Metadata.Name;
                    if (property.Metadata.IsPrimaryKey())
                    {
                        auditEntry.KeyValues[propName] = property.CurrentValue;
                        continue;
                    }

                    switch (entry.State)
                    {
                        case EntityState.Added:
                            auditEntry.AuditType = AuditType.Create;
                            auditEntry.NewValues[propName] = property.CurrentValue;
                            break;
                        case EntityState.Deleted:
                            auditEntry.AuditType = AuditType.Delete;
                            auditEntry.OldValues[propName] = property.OriginalValue;
                            break;
                        case EntityState.Modified:
                            if (property.IsModified)
                            {
                                auditEntry.ChangedColumns.Add(propName);
                                auditEntry.AuditType = AuditType.Update;
                                auditEntry.OldValues[propName] = property.OriginalValue;
                                auditEntry.NewValues[propName] = property.CurrentValue;
                            }
                            break;
                    }
                }
            }

            if (auditEntries.Any())
            {
                var logs = auditEntries.Select(x => x.ToAudit());
                await AuditLogs.AddRangeAsync(logs);
            }
            await Task.CompletedTask;
        }
    }
}

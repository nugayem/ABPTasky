using Microsoft.EntityFrameworkCore;
using Volo.Abp.AuditLogging;
using Volo.Abp.AuditLogging.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.FeatureManagement;
using Volo.Abp.FeatureManagement.EntityFrameworkCore;
using Volo.Abp.PermissionManagement;
using Volo.Abp.PermissionManagement.EntityFrameworkCore;
using Volo.Abp.SettingManagement;
using Volo.Abp.SettingManagement.EntityFrameworkCore;
namespace Tasky.AdministrationService.EntityFrameworkCore;

[ConnectionStringName(AdministrationServiceDbProperties.ConnectionStringName)]
public class AdministrationServiceDbContext : AbpDbContext<AdministrationServiceDbContext>, 
    IPermissionManagementDbContext,
    ISettingManagementDbContext,
    IFeatureManagementDbContext,
    IAuditLoggingDbContext,
    IAdministrationServiceDbContext
{
    /* Add DbSet for each Aggregate Root here. Example:
     * public DbSet<Question> Questions { get; set; }
     */

    public AdministrationServiceDbContext(DbContextOptions<AdministrationServiceDbContext> options)
        : base(options)
    {

    }

    public DbSet<AuditLog> AuditLogs { get; set; }
    public DbSet<FeatureValue> FeatureValues { get; set; }
    public DbSet<PermissionGrant> PermissionGrants { get; set; }
    public DbSet<Setting> Settings { get; set; }
    public DbSet<PermissionGroupDefinitionRecord> PermissionGroups {get; set; }
    public DbSet<PermissionDefinitionRecord> Permissions  {get; set; }

    DbSet<FeatureGroupDefinitionRecord> IFeatureManagementDbContext.FeatureGroups  {get;  }
    DbSet<FeatureDefinitionRecord> IFeatureManagementDbContext.Features  {get; }
    DbSet<FeatureValue> IFeatureManagementDbContext.FeatureValues  {get; }
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ConfigureAdministrationService();
        builder.ConfigureAuditLogging();
            builder.ConfigureFeatureManagement();
            builder.ConfigurePermissionManagement();
            builder.ConfigureSettingManagement();
        }
}

using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.Modularity;
using Volo.Abp.Identity.EntityFrameworkCore;
using Volo.Abp.IdentityServer.EntityFrameworkCore;
using System;
using Volo.Abp.PermissionManagement.Identity;
using Volo.Abp.PermissionManagement.EntityFrameworkCore;

namespace Tasky.IdentityService.EntityFrameworkCore;

[DependsOn(
    typeof(IdentityServiceDomainModule),
    typeof(AbpEntityFrameworkCoreModule)
)]
[DependsOn(typeof(AbpIdentityEntityFrameworkCoreModule))]

    [DependsOn(typeof(AbpIdentityServerEntityFrameworkCoreModule))]
    //[DependsOn(typeof(AbpPermissionManagementEntityFrameworkCoreModule))]
    //[DependsOn(typeof(AbpPermissionManagementDomainIdentityModule))]
    
public class IdentityServiceEntityFrameworkCoreModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpDbContextOptions>(options =>
        {
            options.UseNpgsql();
        });
        
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        context.Services.AddAbpDbContext<IdentityServiceDbContext>(options =>
        {
            options.ReplaceDbContext<IIdentityDbContext>();
            options.ReplaceDbContext<IIdentityServerDbContext>();
            options.AddDefaultRepositories(includeAllEntities: true);

            options.AddDefaultRepositories(true);
        });
    
    }
}

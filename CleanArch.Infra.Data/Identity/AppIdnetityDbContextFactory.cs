using CleanArch.Infra.Data.Shared;
using Microsoft.EntityFrameworkCore;

namespace CleanArch.Infra.Data.Identity
{
    public class AppIdnetityDbContextFactory : DesignTimeDbContextFactoryBase<AppIdentityDbContext>
    {
        protected override AppIdentityDbContext CreateNewInstance(DbContextOptions<AppIdentityDbContext> options)
        {
            return new AppIdentityDbContext(options);
        }
    }
}

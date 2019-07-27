using CleanArch.Infra.Data.Shared;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArch.Infra.Data.Context
{
    class UniversityDbContextFactory : DesignTimeDbContextFactoryBase<UniversityDbContext>
    {
        protected override UniversityDbContext CreateNewInstance(DbContextOptions<UniversityDbContext> options)
        {
            return new UniversityDbContext(options);
        }
    }
}

using CleanArch.Domain.Interfaces;
using CleanArch.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CleanArch.Infra.Data.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly UniversityDbContext _context;

        public UnitOfWork(UniversityDbContext context)
        {
            _context = context;
        }

        public async Task Complete(CancellationToken cancellationToken)
        {

            foreach (var entry in _context.ChangeTracker.Entries())
            {
                if (entry.Entity is ITrackable trackable)
                {
                    var now = DateTime.UtcNow;
                    switch (entry.State)
                    {
                        case EntityState.Modified:
                            trackable.LastUpdatedAt = now;
                            break;

                        case EntityState.Added:
                            trackable.CreatedAt = now;
                            trackable.LastUpdatedAt = now;
                            break;
                    }
                }

            }

            try
            {
                await _context.SaveChangesAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}

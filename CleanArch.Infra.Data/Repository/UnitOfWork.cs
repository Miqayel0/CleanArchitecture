using CleanArch.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace CleanArch.Infra.Data.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DbContext _context;

        public UnitOfWork(DbContext context)
        {
            _context = context;
        }

        public async Task Complete()
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

            await _context.SaveChangesAsync();
        }
    }
}

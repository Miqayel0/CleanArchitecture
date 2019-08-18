using CleanArch.Domain.Entities;
using CleanArch.Domain.Interfaces;
using CleanArch.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CleanArch.Infra.Data.Repository
{
    public class CourseRepository : ICourseRepository
    {
        private readonly UniversityDbContext _context;
        public CourseRepository(UniversityDbContext context)
        {
            _context = context;
        }

        public async Task Add(Course course)
        {
            await _context.Courses.AddAsync(course);
        }

        public async Task Update(Course course)
        {
            await Task.FromResult(_context.Courses.Update(course));
        }

        public async Task<IEnumerable<Course>> GetCourses(CancellationToken cancellationToken)
        {
            return await _context.Courses.ToListAsync(cancellationToken);
        }

        public async Task<Course> GetById(int id)
        {
            return await _context.Courses.FindAsync(id);
        }
    }
}

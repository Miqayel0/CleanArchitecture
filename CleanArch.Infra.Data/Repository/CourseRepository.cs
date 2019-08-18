using CleanArch.Domain.Entities;
using CleanArch.Domain.Interfaces;
using CleanArch.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
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

        public async Task<IEnumerable<Course>> GetCourses()
        {
            return await _context.Courses.ToListAsync();
        }
    }
}

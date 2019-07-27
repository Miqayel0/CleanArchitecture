using CleanArch.Application.Dtos;
using CleanArch.Application.Interfaces;
using CleanArch.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CleanArch.Application.Services
{
    public class CourseService : ICourseService
    {
        private readonly ICourseRepository _courseRepository;

        public CourseService(ICourseRepository courseRepository)
        {
            _courseRepository = courseRepository;
        }
        public async Task<IEnumerable<CourseDto>> GetCourse()
        {
            var courses = await _courseRepository.GetCourses();

            return null;
        }
    }
}

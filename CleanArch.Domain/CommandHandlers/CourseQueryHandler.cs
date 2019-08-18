using CleanArch.Domain.Entities;
using CleanArch.Domain.Interfaces;
using CleanArch.Domain.Queries;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CleanArch.Domain.CommandHandlers
{
    public class CourseQueryHandler : IRequestHandler<CourseAllCouresesQuery, IEnumerable<Course>>
    {
        private readonly ICourseRepository _courseRepository;
        public CourseQueryHandler(ICourseRepository courseRepository)
        {
            _courseRepository = courseRepository;
        }
        public async Task<IEnumerable<Course>> Handle(CourseAllCouresesQuery request, CancellationToken cancellationToken)
        {
            var courses = await _courseRepository.GetCourses();

            return courses;
        }
    }
}

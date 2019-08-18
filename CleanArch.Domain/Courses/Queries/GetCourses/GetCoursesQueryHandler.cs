using CleanArch.Domain.Entities;
using CleanArch.Domain.Interfaces;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CleanArch.Domain.Courses.Queries.GetCourses
{
    public class GetCoursesQueryHandler : IRequestHandler<GetCouresesQuery, IEnumerable<Course>>
    {
        private readonly ICourseRepository _courseRepository;
        public GetCoursesQueryHandler(ICourseRepository courseRepository)
        {
            _courseRepository = courseRepository;
        }
        public async Task<IEnumerable<Course>> Handle(GetCouresesQuery request, CancellationToken cancellationToken)
        {
            var courses = await _courseRepository.GetCourses(cancellationToken);

            return courses;
        }
    }
}
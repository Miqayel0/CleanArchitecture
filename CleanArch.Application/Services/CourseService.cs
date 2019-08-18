using AutoMapper;
using CleanArch.Application.Dtos;
using CleanArch.Application.Interfaces;
using CleanArch.Domain.Core.Bus;
using CleanArch.Domain.Courses.Commads.CreateCourse;
using CleanArch.Domain.Courses.Queries.GetCourses;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CleanArch.Application.Services
{
    public class CourseService : ICourseService
    {
        private readonly IMediatorHandler _bus;
        private readonly IMapper _mapper;
        public CourseService(IMediatorHandler bus, IMapper mapper)
        {
            _bus = bus;
            _mapper = mapper;
        }

        public async Task Create(CourseDto courses)
        {
            var createCourseCommand = new CreateCourseCommand
            {
                Name = courses.Name,
                Description = courses.Description,
                ImageUrl = courses.ImageUrl
            };

            await _bus.Send(createCourseCommand);
        }

        public async Task<IEnumerable<CourseDto>> GetCourses()
        {
            var courses = await _bus.Send(new GetCouresesQuery());
            var coursesDto = _mapper.Map<IEnumerable<CourseDto>>(courses);

            return coursesDto;
        }
    }
}

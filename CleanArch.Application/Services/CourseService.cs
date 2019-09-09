using AutoMapper;
using CleanArch.Application.Dtos;
using CleanArch.Application.Interfaces;
using CleanArch.Domain.Core.Bus;
using CleanArch.Domain.Courses.Commads.CreateCourse;
using CleanArch.Domain.Courses.Commads.UpdateCourse;
using CleanArch.Domain.Courses.Queries.GetCourseById;
using CleanArch.Domain.Courses.Queries.GetCourses;
using CleanArch.Domain.Entities;
using CleanArch.Domain.Interfaces;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CleanArch.Application.Services
{
    public class CourseService : ICourseService
    {
        private readonly IMediatorHandler _bus;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public CourseService(IMediatorHandler bus, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _bus = bus;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
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


        public async Task Update(CourseDto courses)
        {
            var updateCourseCommand = new UpdateCourseCommand
            {
                Id = courses.Id,
                Name = courses.Name,
                Description = courses.Description,
                ImageUrl = courses.ImageUrl
            };

            await _bus.Send(updateCourseCommand);
        }


        public async Task<IEnumerable<CourseDto>> GetCourses()
        {
            var courses = await _bus.Send(new GetCouresesQuery());
            var coursesDto = _mapper.Map<IEnumerable<CourseDto>>(courses);

            return coursesDto;
        }

        public async Task<IEnumerable<CourseDto>> GetCourseById(int id)
        {
            var courses = await _bus.Send(new GetCourseByIdQuery() { Id = id});
            return _mapper.Map<IEnumerable<CourseDto>>(courses);
        }
    }
}

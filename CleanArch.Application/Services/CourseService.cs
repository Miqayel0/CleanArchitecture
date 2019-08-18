using AutoMapper;
using CleanArch.Application.Dtos;
using CleanArch.Application.Interfaces;
using CleanArch.Domain.Core.Bus;
using CleanArch.Domain.Courses.Commads.CreateCourse;
using CleanArch.Domain.Courses.Commads.UpdateCourse;
using CleanArch.Domain.Courses.Queries.GetCourses;
using CleanArch.Domain.Interfaces;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CleanArch.Application.Services
{
    public class CourseService : ICourseService
    {
        private readonly IMediatorHandler _bus;
        private readonly ICourseRepository _courseRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public CourseService(IMediatorHandler bus, IMapper mapper, ICourseRepository courseRepository, IUnitOfWork unitOfWork)
        {
            _bus = bus;
            _mapper = mapper;
            _courseRepository = courseRepository;
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
            CancellationToken cancellationToken;
            var entity = await _courseRepository.GetById(courses.Id);

            await Task.Delay(400);
            for (int i = 0; i < 100000; i++)
            {
                entity.Description = courses.Description;
                entity.Name = courses.Name;
                entity.ImageUrl = courses.ImageUrl;

                await Task.Delay(10);
                await _unitOfWork.Complete(cancellationToken);
            }

            await _unitOfWork.Complete(cancellationToken);

            //var updateCourseCommand = new UpdateCourseCommand
            //{
            //    Id = courses.Id,
            //    Name = courses.Name,
            //    Description = courses.Description,
            //    ImageUrl = courses.ImageUrl
            //};

            //await _bus.Send(updateCourseCommand);
        }


        public async Task<IEnumerable<CourseDto>> GetCourses()
        {
            CancellationToken cancellationToken;
            //var courses = await _bus.Send(new GetCouresesQuery());
            var courses = await _courseRepository.GetCourses(cancellationToken);
            var coursesDto = _mapper.Map<IEnumerable<CourseDto>>(courses);

            return coursesDto;
        }
    }
}

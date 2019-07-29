using CleanArch.Application.Dtos;
using CleanArch.Application.Interfaces;
using CleanArch.Domain.Commands;
using CleanArch.Domain.Core.Bus;
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
        private readonly IMediatorHandler _bus;

        public CourseService(ICourseRepository courseRepository, IMediatorHandler bus)
        {
            _courseRepository = courseRepository;
            _bus = bus;
        }

        public async Task Create(CouresViewModel couresViewModel)
        {
            var createCourseCommand = new CreateCourseCommand(
                    couresViewModel.Name,
                    couresViewModel.Description,
                    couresViewModel.ImageUrl
                );

            await _bus.SendCommand(createCourseCommand);
        }

        public async Task<CouresViewModel> GetCourses()
        {
            var courses = await _courseRepository.GetCourses();

            var model = new CouresViewModel
            {
                Courses = new List<CourseDto>()
            };

            return model;
        }
    }
}

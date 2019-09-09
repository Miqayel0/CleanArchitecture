using CleanArch.Domain.Entities;
using CleanArch.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CleanArch.Domain.Courses.Commads.CreateCourse
{
    public class CreateCourseCommandHandler : IRequestHandler<CreateCourseCommand, bool>
    {
        private readonly ICourseRepository _courseRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMediator _mediator;

        public CreateCourseCommandHandler(ICourseRepository courseRepository, IUnitOfWork unitOfWork, IMediator mediator)
        {
            _courseRepository = courseRepository;
            _unitOfWork = unitOfWork;
            _mediator = mediator;
        }
        public async Task<bool> Handle(CreateCourseCommand request, CancellationToken cancellationToken)
        {
            var course = new Course()
            {
                Name = request.Name,
                Description = request.Description,
                ImageUrl = request.ImageUrl
            };

            await _courseRepository.Add(course);
            await _unitOfWork.Complete(cancellationToken);
            await _mediator.Publish(new CourseCreated { CourseId = course.Id });

            return true;
        }
    }
}

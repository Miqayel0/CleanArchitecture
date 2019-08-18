using CleanArch.Domain.Entities;
using CleanArch.Domain.Exeptions;
using CleanArch.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CleanArch.Domain.Courses.Commads.UpdateCourse
{
    public class UpdateCourseCommandHandler : IRequestHandler<UpdateCourseCommand, bool>
    {
        private readonly ICourseRepository _courseRepository;
        private readonly IUnitOfWork _unitOfWork;
        public UpdateCourseCommandHandler(ICourseRepository courseRepository, IUnitOfWork unitOfWork)
        {
            _courseRepository = courseRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task<bool> Handle(UpdateCourseCommand request, CancellationToken cancellationToken)
        {
            var entity = await _courseRepository.GetById(request.Id);

            if (entity == null)
            {
                throw new NotFoundException(nameof(Course), request.Id);
            }

            await Task.Delay(4000);
            for (int i = 0; i < 100000; i++)
            {
                entity.Description = request.Description;
                entity.Name = request.Name;
                entity.ImageUrl = request.ImageUrl;

                await Task.Delay(100);
                await _unitOfWork.Complete(cancellationToken);
            }

            await _unitOfWork.Complete(cancellationToken);

            return true;
        }
    }
}

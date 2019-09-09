using CleanArch.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CleanArch.Domain.Courses.Commads.CreateCourse
{
    public class CourseCreatedHandler : INotificationHandler<CourseCreated>
    {
        private readonly INotificationSender _notification;

        public CourseCreatedHandler(INotificationSender notification)
        {
            _notification = notification;
        }
        public async Task Handle(CourseCreated notification, CancellationToken cancellationToken)
        {
            await _notification.SendAsync("Course has been Created", notification.CourseId.ToString(), "Course");
        }
    }
}

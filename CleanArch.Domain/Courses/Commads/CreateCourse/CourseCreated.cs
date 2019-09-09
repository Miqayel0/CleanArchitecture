using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArch.Domain.Courses.Commads.CreateCourse
{
    public class CourseCreated : INotification
    {
        public int CourseId { get; set; }
    }
}

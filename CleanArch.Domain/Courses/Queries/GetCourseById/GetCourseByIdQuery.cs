using CleanArch.Domain.Core.Queries;
using CleanArch.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArch.Domain.Courses.Queries.GetCourseById
{
    public class GetCourseByIdQuery : Query<Course>
    {
        public int Id { get; set; }
    }
}

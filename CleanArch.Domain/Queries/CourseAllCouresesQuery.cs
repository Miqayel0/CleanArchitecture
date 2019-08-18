using CleanArch.Domain.Core.Queries;
using CleanArch.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArch.Domain.Queries
{
    public class CourseAllCouresesQuery : Query<IEnumerable<Course>>
    {
    }
}

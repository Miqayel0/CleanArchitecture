using CleanArch.Domain.Core.Queries;
using CleanArch.Domain.Entities;
using System.Collections.Generic;

namespace CleanArch.Domain.Courses.Queries.GetCourses
{
    public class GetCouresesQuery : Query<IEnumerable<Course>>
    {
    }
}

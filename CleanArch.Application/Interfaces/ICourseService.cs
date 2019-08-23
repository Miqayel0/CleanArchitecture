using CleanArch.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CleanArch.Application.Interfaces
{
    public interface ICourseService
    {
        Task<IEnumerable<CourseDto>> GetCourses();
        Task Create(CourseDto course);
        Task Update(CourseDto requset);
        Task<IEnumerable<CourseDto>> GetCourseById(int id);
    }
}

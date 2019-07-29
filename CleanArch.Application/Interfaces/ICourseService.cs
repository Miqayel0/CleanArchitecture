using CleanArch.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CleanArch.Application.Interfaces
{
    public interface ICourseService
    {
        Task<CouresViewModel> GetCourses();
        Task Create(CouresViewModel couresViewModel);
    }
}

using CleanArch.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArch.Application
{
    public class CouresViewModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public IEnumerable<CourseDto> Courses { get; set; }
    }
}

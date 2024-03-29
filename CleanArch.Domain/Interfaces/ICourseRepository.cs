﻿using CleanArch.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CleanArch.Domain.Interfaces
{
    public interface ICourseRepository
    {
        Task<IEnumerable<Course>> GetCourses(System.Threading.CancellationToken cancellationToken);
        Task Add(Course course);
        Task Update(Course course);
        Task UpdateRange(IEnumerable<Course> courses);
        Task<Course> GetById(int id);
    }
}

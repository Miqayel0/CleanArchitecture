using CleanArch.Application.Interfaces;
using CleanArch.Application.Services;
using CleanArch.Domain.Core.Bus;
using CleanArch.Domain.Courses.Commads.CreateCourse;
using CleanArch.Domain.Courses.Commads.UpdateCourse;
using CleanArch.Domain.Courses.Queries.GetCourseById;
using CleanArch.Domain.Courses.Queries.GetCourses;
using CleanArch.Domain.Entities;
using CleanArch.Domain.Interfaces;
using CleanArch.Infra.Auth.Services;
using CleanArch.Infra.Bus;
using CleanArch.Infra.Data.Context;
using CleanArch.Infra.Data.Repository;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;

namespace CleanArch.infra.IoC
{
    public class DependencyContainer
    {
        public static void RegisterServices(IServiceCollection services)
        {
            // Domain InMemoryBus MediatR
            services.AddScoped<IMediatorHandler, InMemoryBus>();

            // Domain Handlers
            services.AddScoped<IRequestHandler<CreateCourseCommand, bool>, CreateCourseCommandHandler>();
            services.AddScoped<IRequestHandler<UpdateCourseCommand, bool>, UpdateCourseCommandHandler>();
            services.AddScoped<IRequestHandler<GetCouresesQuery, IEnumerable<Course>>, GetCoursesQueryHandler>();
            services.AddScoped<IRequestHandler<GetCourseByIdQuery, Course>, GetCourseByIdQueryHandler>();

            // Application Layer
            services.AddScoped<ICourseService, CourseService>();
            services.AddScoped<IAccountService, AccountService>();

            // Persistence Layer
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<ICourseRepository, CourseRepository>();
            services.AddScoped<IJwtFactory, JwtFactory>();
            services.AddScoped<UniversityDbContext>();
        }
    }
}

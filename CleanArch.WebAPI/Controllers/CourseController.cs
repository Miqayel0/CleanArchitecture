using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CleanArch.Application.Dtos;
using CleanArch.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CleanArch.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        private readonly ICourseService _courseService;
        public CourseController(ICourseService courseService)
        {
            _courseService = courseService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CourseDto>>> Get()
        {
            return Ok(await _courseService.GetCourses());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<CourseDto>>> Get(int id)
        {
            return Ok(await _courseService.GetCourseById(id));
        }


        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CourseDto requset)
        {
            await _courseService.Create(requset);

            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] CourseDto requset)
        {
            requset.Id = id;

            await _courseService.Update(requset);

            return Ok();

        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}

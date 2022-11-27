
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MongoDbExample.DTOs;
using MongoDbExample.Models;
using MongoDbExample.Features.Courses;


[Route("api/[controller]")]
[ApiController]
public class CourseController : ControllerBase
{
    // private readonly CourseService _courseService;
    private readonly ICoursesRepository _courseRepo;
    private readonly IMapper _mapper;
    public CourseController(ICoursesRepository service, IMapper mapper)
    {

        _courseRepo = service;
        _mapper = mapper;
    }
    [HttpGet("GetAllCourses")]
    public async Task<ActionResult<IEnumerable<Course>>> GetAll()
    {
        var course = await _courseRepo.GetAllAsync();
        return Ok(course);
    }

    [HttpPost("CreateCourses")]
    public async Task<IActionResult> Create(CourseDTO courseDto)
    {
        var course = _mapper.Map<Course>(courseDto);
        var createUser = await _courseRepo.CreateAsync(course);
        return Ok(createUser);
    }

}

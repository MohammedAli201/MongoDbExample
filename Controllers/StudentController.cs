
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
// using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MongoDbExample.DTOs;
using MongoDbExample.Features.Courses;
using MongoDbExample.Features.Students;
using MongoDbExample.Features.Users;
using MongoDbExample.Models;

// [Authorize]
[Route("api/[controller]")]
[ApiController]
public class StudentController : ControllerBase
{
    private readonly IStudentRepository _studentService;
    private readonly ICoursesRepository _courseService;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IMapper _mapper;
    public StudentController(IStudentRepository service, UserManager<ApplicationUser> userManager, IMapper mapper, ICoursesRepository courseService)
    {
        _studentService = service;
        _userManager = userManager;
        _mapper = mapper;
        _courseService = courseService;

    }



    // [Authorize]
    [HttpGet("GetAllStudents")]
    public async Task<ActionResult<IEnumerable<Student>>> GetAll()
    {
        var students = await _studentService.GetAllAsync();
        return Ok(students);
    }

    [AllowAnonymous]
    [HttpGet("{id}")]
    public async Task<ActionResult<Student>> GetById(string id)
    {
        var student = await _studentService.GetByIdAsync(id);
        if (student == null)
        {
            return NotFound();
        }
        if (student.Course.Count > 0)
        {

            var tempList = new List<Course>();
            foreach (var courseId in student.Course)
            {
                var course = await _courseService.GetByIdAsync(courseId);
                if (course != null)
                {
                    tempList.Add(course);
                }
                student.CourseList = tempList;
            }
        }
        return Ok(student);
    }
    // [AllowAnonymous]
    // [Authorize]

    [HttpPost("Create")]
    public async Task<IActionResult> Create(StudentDTO student)
    {
        var user = _mapper.Map<Student>(student);
        var createUser = await _studentService.CreateAsync(user);
        return Ok(createUser);
    }
    [HttpPut]
    public async Task<IActionResult> Update(string id, Student updatedStudent)
    {
        var queriedStudent = await _studentService.GetByIdAsync(id);
        if (queriedStudent == null)
        {
            return NotFound();
        }
        await _studentService.UpdateAsync(id, updatedStudent);
        return NoContent();
    }
    [HttpDelete]
    public async Task<IActionResult> Delete(string id)
    {
        var student = await _studentService.GetByIdAsync(id);
        if (student == null)
        {
            return NotFound();
        }
        await _studentService.DeleteAsync(id);
        return NoContent();
    }

    // [HttpPost("Register")]
    // public async Task<IActionResult> Register([FromBody] RegisterRequestDTo model)
    // {
    //     Console.WriteLine(model);

    //     // var user = _mapper.Map<ApplicationUser>(model);
    //     var user = new ApplicationUser
    //     {
    //         Email = model.Email,
    //         UserName = model.UserName,
    //         FirstName = "namfddjf",
    //         LastName = "adsd"
    //     };


    //     try
    //     {
    //         var createUser = await _userManager.CreateAsync(user, model.Password);
    //         return Ok(createUser);
    //     }
    //     catch (Exception ex)
    //     {

    //         return BadRequest(new { message = ex.Message });
    //     }


    // }
}





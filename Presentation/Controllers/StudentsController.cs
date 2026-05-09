using MediatR;
using Microsoft.AspNetCore.Mvc;
using Application.DTOs;
using Application.Commands.CreateStudent;
using Application.Commands.UpdateStudent;
using Application.Commands.DeleteStudent;
using Application.Queries.GetAllStudents;
using Application.Queries.GetStudentById;

namespace Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StudentsController : ControllerBase
{
    private readonly IMediator _mediator;

    public StudentsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    // GET: api/students
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var students = await _mediator.Send(new GetAllStudentsQuery());
        return Ok(students);
    }

    // GET: api/students/{id}
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var student = await _mediator.Send(new GetStudentByIdQuery { Id = id });
        if (student == null)
            return NotFound();
        return Ok(student);
    }

    // POST: api/students
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateStudentCommand command)
    {
        var student = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetById), new { id = student.Id }, student);
    }

    // PUT: api/students/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateStudentCommand command)
    {
        if (id != command.Id)
            return BadRequest("ID mismatch");

        var result = await _mediator.Send(command);
        if (!result)
            return NotFound();

        return NoContent();
    }

    // DELETE: api/students/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _mediator.Send(new DeleteStudentCommand { Id = id });
        if (!result)
            return NotFound();

        return NoContent();
    }
}
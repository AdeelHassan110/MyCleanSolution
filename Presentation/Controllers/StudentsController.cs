using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StudentsController : ControllerBase
{
    private readonly IStudentRepository _studentRepo;

    public StudentsController(IStudentRepository studentRepo)
    {
        _studentRepo = studentRepo;
    }

    // GET: api/students
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var students = await _studentRepo.GetAllAsync();
        var studentDtos = students.Select(s => new StudentDto
        {
            Id = s.Id,
            Name = s.Name,
            Email = s.Email,
            Age = s.Age
        });
        return Ok(studentDtos);
    }

    // GET: api/students/{id}
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var student = await _studentRepo.GetByIdAsync(id);
        if (student == null)
            return NotFound();

        var dto = new StudentDto
        {
            Id = student.Id,
            Name = student.Name,
            Email = student.Email,
            Age = student.Age
        };
        return Ok(dto);
    }

    // POST: api/students
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateStudentDto createDto)
    {
        var student = new Student
        {
            Name = createDto.Name,
            Email = createDto.Email,
            Age = createDto.Age
        };

        await _studentRepo.AddAsync(student);
        return CreatedAtAction(nameof(GetById), new { id = student.Id }, student);
    }

    // PUT: api/students/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateStudentDto updateDto)
    {
        var existing = await _studentRepo.GetByIdAsync(id);
        if (existing == null)
            return NotFound();

        existing.Name = updateDto.Name;
        existing.Email = updateDto.Email;
        existing.Age = updateDto.Age;

        await _studentRepo.UpdateAsync(existing);
        return NoContent();
    }

    // DELETE: api/students/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var existing = await _studentRepo.GetByIdAsync(id);
        if (existing == null)
            return NotFound();

        await _studentRepo.DeleteAsync(id);
        return NoContent();
    }
}
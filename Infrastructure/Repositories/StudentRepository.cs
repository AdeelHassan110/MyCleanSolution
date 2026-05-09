using Application.Interfaces;
using Domain.Entities;

namespace Infrastructure.Repositories;

public class StudentRepository : IStudentRepository
{
    private static List<Student> _students = new();
    private static int _nextId = 1;

    public Task<List<Student>> GetAllAsync()
    {
        return Task.FromResult(_students);
    }

    public Task<Student?> GetByIdAsync(int id)
    {
        var student = _students.FirstOrDefault(s => s.Id == id);
        return Task.FromResult(student);
    }

    public Task AddAsync(Student student)
    {
        student.Id = _nextId++;
        _students.Add(student);
        return Task.CompletedTask;
    }

    public Task UpdateAsync(Student student)
    {
        var existing = _students.FirstOrDefault(s => s.Id == student.Id);
        if (existing != null)
        {
            existing.Name = student.Name;
            existing.Email = student.Email;
            existing.Age = student.Age;
        }
        return Task.CompletedTask;
    }

    public Task DeleteAsync(int id)
    {
        var student = _students.FirstOrDefault(s => s.Id == id);
        if (student != null)
            _students.Remove(student);
        return Task.CompletedTask;
    }
}
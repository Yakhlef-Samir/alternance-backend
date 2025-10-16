using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Alternance.Domain.Entities;

namespace Alternance.Application.Interfaces
{
    public interface IStudentRepository
    {
        Task<Student?> GetStudentByIdAsync(Guid id);
        Task<List<Student>> GetAllStudentAsync();
        Task<Student> AddStudentAsync(Student student);
        Task UpdateStudentAsync(Student student);
        Task DeleteStudentAsync(Guid id);
    }
}
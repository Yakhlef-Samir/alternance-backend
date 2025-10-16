using Alternance.Application.Interfaces;
using Alternance.Domain.Entities;
using Alternance.Infrastructure.MongoDb;
using MongoDB.Driver;

namespace Alternance.Infrastructure.Repositories;

public class StudentRepository : MongoRepository<Student>, IStudentRepository
{
    private const string STUDENT_COLLECTION = "Student";
    private readonly IMongoCollection<Student> _collection;

    public StudentRepository(MongoDbContext context) : base(context)
    {
        _collection = context.GetCollection<Student>(STUDENT_COLLECTION);
    }

    //** Get student by ID
    public async Task<Student?> GetStudentByIdAsync(Guid id)
    {
        return await GetByIdAsync(id);
    }

    //** Get all students
    public async Task<List<Student>> GetAllStudentAsync()
    {
        IAsyncCursor<Student> cursor = await _collection.FindAsync(_ => true);
        return await cursor.ToListAsync();
    }

    //** Add student
    public async Task<Student> AddStudentAsync(Student student)
    {
        return await AddAsync(student);
    }

    //** Update student
    public async Task UpdateStudentAsync(Student student)
    {
        await UpdateAsync(student);
    }

    //** Delete student
    public async Task DeleteStudentAsync(Guid id)
    {
        await DeleteAsync(id);
    }
}

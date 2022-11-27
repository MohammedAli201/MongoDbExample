using MongoDbExample.Models;
using MongoDbExample.DTOs;

namespace MongoDbExample.Features.Students
{
    public interface IStudentRepository
    {
        //   Task<Student> Authenticate(string username, string password);
        bool Exist(string email);
        Task<IEnumerable<Student>> GetAllAsync();
        Task<Student> GetByIdAsync(string id);
        Task<Student> CreateAsync(Student student);
        Task UpdateAsync(string id, Student student);
        Task DeleteAsync(string id);
    }
}
using MongoDbExample.Models;
using MongoDbExample.DTOs;

namespace MongoDbExample.Features.Students
{
    public interface IStudentRepository
    {
        //   Task<Student> Authenticate(string username, string password);
        bool Exist(string email);
        Task<IEnumerable<Student>> GetAllAsync();
        Task<Student> GetById(Guid id);
        Task<Student> Create(StudentDTO student);
        void Update(Student student, string oldPassword, string newPassword);
        void Delete(string id);
    }
}
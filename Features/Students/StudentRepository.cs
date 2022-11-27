using MongoDbExample.DTOs;
using MongoDbExample.Models;

namespace MongoDbExample.Features.Students
{
    public class StudentRepository : IStudentRepository
    {
        Task<Student> IStudentRepository.Create(StudentDTO student)
        {
            throw new NotImplementedException();
        }

        void IStudentRepository.Delete(string id)
        {
            throw new NotImplementedException();
        }

        bool IStudentRepository.Exist(string email)
        {
            throw new NotImplementedException();
        }

        Task<IEnumerable<Student>> IStudentRepository.GetAllAsync()
        {
            throw new NotImplementedException();
        }

        Task<Student> IStudentRepository.GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        void IStudentRepository.Update(Student student, string oldPassword, string newPassword)
        {
            throw new NotImplementedException();
        }
    }
}
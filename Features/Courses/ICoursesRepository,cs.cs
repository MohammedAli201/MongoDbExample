using MongoDbExample.Models;
using MongoDbExample.Utilize;

namespace MongoDbExample.Features.Courses
{
    public interface ICoursesRepository
    {
        bool Exist(string email);
        Task<IEnumerable<Course>> GetAllAsync();
        Task<Course> GetByIdAsync(string id);
        Task<PageResults> CreateAsync(Course courses);
        void Update(Course courses, string oldPassword, string newPassword);
        Task<PageResults> Delete(string id);
    }
}
using MongoDbExample.Models;

namespace MongoDbExample.Features.Users
{
    public interface IUserRepository
    {
        Task<ApplicationUser> Authenticate(string username, string password);
        bool Exist(string email);
        Task<IEnumerable<ApplicationUser>> GetAllAsync();
        Task<ApplicationUser> GetById(Guid id);
        Task<ApplicationUser> Create(ApplicationUser userApp, string password);
        void Update(ApplicationUser user, string oldPassword, string newPassword);
        void Delete(string id);
    }
}
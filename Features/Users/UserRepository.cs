using Microsoft.AspNetCore.Identity;
using MongoDB.Driver;
using MongoDbExample.Extensions;
using MongoDbExample.Models;

namespace MongoDbExample.Features.Users
{
    public class UserRepository : IUserRepository
    {
        private readonly IMongoCollection<ApplicationUser> _users;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        public UserRepository(ISchoolDatabaseSettings settings, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> userSigningManager)
        {
            _userManager = userManager;
            _signInManager = userSigningManager;
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _users = database.GetCollection<ApplicationUser>(settings.UserCollectionName);
        }

        public async Task<ApplicationUser> Authenticate(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                return null;
            var user = await _userManager.FindByEmailAsync(username);
            if (user == null)
                return null;

            // authentication successful
            // var user = await _userManager.FindByEmailAsync(model.Email);
            var passwordUser = await _userManager.CheckPasswordAsync(user, password);
            // var result = await _signInManager.PasswordSignInAsync(username, password, false, false);
            // Console.WriteLine(result);
            if (!passwordUser)
            {
                Console.WriteLine(passwordUser);
                return null;
            }
            return user;
        }

        public async Task<ApplicationUser> Create(ApplicationUser userApp, string password)
        {
            userApp.UserName = userApp.UserName.ToLower();
            //Validation 
            if (string.IsNullOrWhiteSpace(password))
            {
                throw new AppException("Password is required");
            }

            var userExist = await _userManager.FindByEmailAsync(userApp.Email);
            if (userExist != null)
            {
                throw new AppException("Email \"" + userApp.Email + "\" is already taken");
            }

            await _userManager.CreateAsync(userApp, password);
            // await _users.InsertOneAsync(userApp);

            return userApp;

        }

        public void Delete(string id)
        {
            throw new NotImplementedException();
        }

        public bool Exist(string email)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<ApplicationUser>> GetAllAsync()
        {
            return await _users.Find(s => true).ToListAsync();
        }

        public async Task<ApplicationUser> GetById(Guid id)
        {
            return await _users.Find<ApplicationUser>(s => s.Id == id).FirstOrDefaultAsync();

        }

        public void Update(ApplicationUser user, string oldPassword, string newPassword)
        {
            throw new NotImplementedException();
        }
    }
}
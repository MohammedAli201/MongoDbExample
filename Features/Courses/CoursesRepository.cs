using MongoDB.Driver;
using MongoDbExample.Models;
using MongoDbExample.Utilize;

namespace MongoDbExample.Features.Courses
{
    public class CoursesRepository : ICoursesRepository
    {

        private readonly IMongoCollection<Course> _courses;

        public CoursesRepository(ISchoolDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _courses = database.GetCollection<Course>(settings.CoursesCollectionName);
        }
        public async Task<PageResults> CreateAsync(Course courses)
        {
            // check if Course is exist already
            var exist = await _courses.FindAsync(c => c.Name == courses.Name);
            if (exist != null)
            {
                return new PageResults
                {
                    Message = "The course is already exist",
                    Failed = true
                };
            }
            try
            {
                await _courses.InsertOneAsync(courses);

            }
            catch (System.Exception ex)
            {
                return new PageResults
                {

                    Message = ex.Message,

                    Failed = true
                };
            }
            return new PageResults
            {

                Message = "The course is succesfully create ",

                Sucecesed = true
            };
        }

        public async Task<PageResults> Delete(string id)
        {
            // CHECK FIRST IF THE USER EXIST 


            try
            {

                await _courses.DeleteOneAsync(c => c.Id == id);

            }
            catch (System.Exception ex)
            {
                return new PageResults
                {
                    Message = ex.Message,

                    Failed = true
                };
            }
            return new PageResults
            {
                Message = "The operation is done",

                Sucecesed = true
            };
        }

        public bool Exist(string email)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Course>> GetAllAsync()
        {
            return await _courses.Find<Course>(c => true).ToListAsync();
        }

        public async Task<Course> GetByIdAsync(string id)
        {
            return await _courses.Find<Course>(c => c.Id == id).FirstOrDefaultAsync();
        }

        public void Update(Course courses, string oldPassword, string newPassword)
        {
            throw new NotImplementedException();
        }
    }

}
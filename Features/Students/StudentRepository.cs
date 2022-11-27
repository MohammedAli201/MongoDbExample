using MongoDB.Driver;
using MongoDbExample.DTOs;
using MongoDbExample.Models;

namespace MongoDbExample.Features.Students
{
    public class StudentRepository : IStudentRepository
    {

        private readonly IMongoCollection<Student> _students;

        public StudentRepository(ISchoolDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _students = database.GetCollection<Student>(settings.StudentsCollectionName);
        }

        public async Task<Student> CreateAsync(Student student)
        {
            await _students.InsertOneAsync(student);
            return student;
        }

        public async Task DeleteAsync(string id)
        {
            await _students.DeleteOneAsync(s => s.Id == id);
        }

        public bool Exist(string email)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Student>> GetAllAsync()
        {
            return await _students.Find(s => true).ToListAsync();
        }

        public async Task<Student> GetByIdAsync(string id)
        {
            return await _students.Find<Student>(s => s.Id == id).FirstOrDefaultAsync();

        }

        public async Task UpdateAsync(string id, Student student)
        {
            await _students.ReplaceOneAsync(s => s.Id == id, student);

        }
    }
}
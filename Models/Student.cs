using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MongoDbExample.Models
{
    public class Student
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = string.Empty;

        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;

        public string Major { get; set; } = string.Empty;

        [BsonRepresentation(BsonType.ObjectId)]
        public List<string>? Course { get; set; }
        [BsonIgnore]
        public List<Course>? CourseList { get; set; }

    }
}
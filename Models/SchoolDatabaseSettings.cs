namespace MongoDbExample.Models
{
    public class SchoolDatabaseSettings : ISchoolDatabaseSettings
    {
        public string StudentsCollectionName { get; set; }
        public string CoursesCollectionName { get; set; }
        public string UserCollectionName { get; set; }
        public string? ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }

    public interface ISchoolDatabaseSettings
    {

        string StudentsCollectionName { get; set; }
        string CoursesCollectionName { get; set; }
        string UserCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }


    }
}
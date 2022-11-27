namespace MongoDbExample.DTOs
{
    public class StudentDTO
    {

        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;

        public string Major { get; set; } = string.Empty;
        public List<string>? Course { get; set; }
    }
}
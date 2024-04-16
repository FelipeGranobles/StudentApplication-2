    namespace StudentApplication.Models
    {
        public class Class
        {
            public int ClassId { get; set; }
            public string? ClassName { get; set; }
            public bool ActiveClass { get; set; } = true;
            public Subject? Subject { get; set; }
        }
    }

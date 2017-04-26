using System.Data.Entity;

namespace AegeanThesis.Models
{
    public class Lessons
    {
        public int ID { get; set; }
        public string ThesisId { get; set; } //foreign key from ThesisForm.ID
        public string Structed_Programming { get; set; }
        public string Object_Oriented_Programming { get; set; }
    }
    public class LessonsBContext : DbContext
    {
        public DbSet<Lessons> lessons { get; set; }
    }
}
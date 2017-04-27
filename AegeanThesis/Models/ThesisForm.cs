using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Web.Mvc;

namespace AegeanThesis.Models
{
    public class ThesisForm
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string Supervisor { get; set; } //Taken from - connected with: User model attribute Name

        [Display(Name = "Number of Students")]
        [Range(1,3)]
        public string NumStudents { get; set; }

        public string Purpose { get; set; }
        public string Description { get; set; }

        [Display(Name = "Prerequested Lessons")]
        public string PrereqLessons { get; set; }

        public string[] LessonsList { get; set; }
        public IEnumerable<SelectListItem> Items { get; set; }

        [Display(Name = "Prerequested Knowledge")]
        public string PrereqKnowledge { get; set; }

        [Display(Name = "Students Info")]
        public string StudentInfo { get; set; }

        [Display(Name = "Announcement Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public string AnnouncDate { get; set; }

        [Display(Name = "Adoption Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public string AdoptionDate { get; set; }

        [Display(Name = "Finish Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public string FinishDate { get; set; }

        [Range(5, 10)]
        public float? Grade { get; set; }

        public bool Assigned { get; set; }
    }
    public class ThesisFormBContext : DbContext
    {
        public DbSet<ThesisForm> Thesises { get; set; }
    }
}
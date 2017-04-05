using System.ComponentModel.DataAnnotations;

namespace AegeanThesis.Models
{
    public class ThesisForm
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string Supervisor { get; set; }

        [Range(1,3)]
        public string NumStudents { get; set; }

        public string Purpose { get; set; }
        public string Description { get; set; }

        public string [] PrereqLessons { get; set; }

        public string PrereqKnowledge { get; set; }

        //Στοιχεία Φοιτητών - Foregin key?

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
        public float Grade { get; set; }
    }
}
using System.Collections.Generic;
using System.Web.Mvc;

namespace AegeanThesis.Models
{
    public class LessonsViewModel
    {
        public string[] LessonsList { get; set; }
        public IEnumerable<SelectListItem> Items { get; set; }
    }
}
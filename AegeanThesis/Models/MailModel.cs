using System.ComponentModel.DataAnnotations;

namespace AegeanThesis.Models
{
    public class MailModel
    {       
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Subject { get; set; }        
        public string Notes { get; set; }

        public int? ThesisId { get; set; }
    }
}
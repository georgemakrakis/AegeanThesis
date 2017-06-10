namespace AegeanThesis.Models
{
    public class ThesisProgress
    {
        public int ID { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string Name { get; set; }
        public double ProgressPercentage { get; set; }
        public string Dependencies { get; set; }
    }
}
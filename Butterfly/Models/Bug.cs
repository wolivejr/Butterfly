using System;

namespace Butterfly.Models
{
    public class Bug
    {
        public int ID { get; set; }
        public string Status { get; set; }
        public string Description { get; set; }
        public string Priority { get; set; }
        public string Category { get; set; }
        public string Submitter { get; set; }
        public string SubmissionDate { get; set; }
        public string SubmissionTime { get; set; }
        public string Solution { get; set; }
        public string SolutionUser { get; set; }
        public string SolutionDate { get; set; }
        public string SolutionTime { get; set; }

        public Bug()
        {
            Status = "Open";
        }

    }
}

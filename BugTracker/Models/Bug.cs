using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BugTracker.Models
{
    public class Bug
    {
        public int ID { get; set; }
        public string Status { get; set; }
        public string Description { get; set; }
        public string Priority { get; set; }
        public string Category { get; set; }

        public Bug()
        {
            Status = "Open";
        }
    }
}

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
        public string Username { get; set; }
        public string Date { get; set; }
        public string Time { get; set; }

        public Bug()
        {
            Status = "Open";
            Date = DateTime.Now.ToShortDateString();
            Time = DateTime.Now.ToShortTimeString();
        }
    }
}

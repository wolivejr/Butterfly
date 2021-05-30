﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BugTracker.Models
{
    public class User
    {
        public int ID { get; set; }
        public string Username { get; set; }
        public string Group { get; set; }
        public string Role { get; set; }

        public User()
        {
            Group = "Unaffliated";
            Role = "User";
        }
    }
}

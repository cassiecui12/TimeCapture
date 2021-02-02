using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TimeCapture.Models
{
    public class TimeRecording
    {
        public TimeRecording() { }

        public int Id { get; set; }
        public Lawyer Lawyer { get; set; }
        public Client Client { get; set; }
        public Matter Matter { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Description { get; set; }
        public Category Category { get; set; }
    }
}
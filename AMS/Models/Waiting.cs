using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AMS.Models
{
    public class Waiting
    {
        public int WaitingId { get; set; }
        public int UserId { get; set; }
        public String Name { get; set; }
        public int DoctorId { get; set; }
        public DateTime TakenAt { get; set; }
        public int Serial { get; set; }
        public String Date { get; set; }
    }
}
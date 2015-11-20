using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AMS.Models
{
    public class Appointment
    {
        public int AppointmentId { get; set; }
        public int PatientId { get; set; }
        public int DoctorId { get; set; }
        public DateTime TakenAt { get; set; }
        public String VisitingTime { get; set; }
        public String Date { get; set; }
        public int Serial { get; set; }
        public String Uid { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AMS.Models
{
    public class Schedule
    {
        public int ScheduleId { get; set; }
        public int DoctorId { get; set; }
        public String Dates { get; set; }
        public int FromHour { get; set; }
        public int FromMinute { get; set; }
        public int ToHour { get; set; }
        public int ToMinute { get; set; }
        public int PatientNumber { get; set; }
        public int VisitingFee { get; set; }
        public int RegistrationFee { get; set; }
    }
}
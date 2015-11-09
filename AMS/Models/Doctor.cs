using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AMS.Models
{
    public class Doctor
    {
        [Key]
        public int DoctorId { get; set; }
        public int UserId { get; set; }
        public String FirstName { get; set; }
        public String LastName { get; set; }
        public String Specialization { get; set; }
        public int Age { get; set; }
        public String Gender { get; set; }
        public String MobileNumber { get; set; }
        public String ImageUrl { get; set; }
    }
}
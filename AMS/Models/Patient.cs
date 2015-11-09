using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AMS.Models
{
    public class Patient
    {
        [Key]
        public int PatientId { get; set; }
        public int UserId { get; set; }
        public String Name { get; set; }
        public int Age { get; set; }
        public String Gender { get; set; }
        public String Location { get; set; }
        public String MobileNumber { get; set; }
    }
}
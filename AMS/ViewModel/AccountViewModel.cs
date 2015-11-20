using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace AMS.ViewModel
{
    public class LoginModel
    {
        [Required]
        [EmailAddress]
        public String Email { get; set; }
        [Required]
        public String Password { get; set; }
        public bool RememberMe { get; set; }
    }

    public class RegisterModel
    {
        [Required]
        public String FirstName { get; set; }
        [Required]
        public String LastName { get; set; }
        [Required]
        [EmailAddress]
        public String Email { get; set; }
        [Required]
        public String Password { get; set; }
        [Required]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{5})$", ErrorMessage = "Entered phone format is not valid.")]
        public String MobileNumber { get; set; }
        [Required]
        [Compare("Password")]
        public String ConfirmPassword { get; set; }
        [Required]
        public String Role { get; set; }
    }
    public class ProfileModel
    {
        [Required]
        public String FirstName { get; set; }
        
        [Required]
        public String LastName { get; set; }
        [Required]
        public String Gender { get; set; }
        [Required]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{5})$", ErrorMessage = "Entered phone format is not valid.")]
        public String MobileNumber { get; set; }

        public int Age { get; set; }
        [Required]
        public String Location { get; set; }
        public String About { get; set; }
        public string ImageUrl { get; set; }
    }

    public class DoctorModel
    {
        [Required]
        public String FirstName { get; set; }
        [Required]
        public String LastName { get; set; }
        [Required]
        public String Specialization { get; set; }
        [Required]
        public int Age { get; set; }
        [Required]
        public String Gender { get; set; }
        public String HouseNo { get; set; }
        public String RoadNo { get; set; }
        [Required]
        public String Thana { get; set; }
        [Required]
        public String Zilla { get; set; }
        [Required]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{5})$", ErrorMessage = "Entered mobile number is not valid.")]
        public String MobileNumber { get; set; }
        
        public String ImageUrl { get; set; }
    }

    public class EditAccountModel
    {
        public String Email { get; set; }
        [Required]
        public String CurrentPassword { get; set; }
        [Required]
        public String Password { get; set; }
        [Required]
        [Compare("Password")]
        public String ConfirmPassword { get; set; }
    }
    public class ScheduleModel
    {
        [Required]
        public String Dates { get; set; }
        [Required]
        public int FromHour { get; set; }
        [Required]
        public int FromMinute { get; set; }
        [Required]
        public int ToHour { get; set; }
        [Required]
        public int ToMinute { get; set; }
        [Required]
        public int PatientNumber { get; set; }
    }

    public class AppointmentModel
    {
        public int PatientId { get; set; }
        public String PatientName { get; set; }
        public String VisitingTime { get; set; }
        public String Date { get; set; }
        public int Serial { get; set; }
        public String Uid { get; set; }
    }
}
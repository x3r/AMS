using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AMS.Models
{
    public class Address
    {
        [Key]
        public int AddressId { get; set; }
        public int UserId { get; set; }
        public String HouseNo { get; set; }
        public String RoadNo { get; set; }
        public String Thana { get; set; }
        public String Zilla { get; set; }
    }
}
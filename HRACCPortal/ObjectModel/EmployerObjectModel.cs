using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HRACCPortal.ObjectModel
{
    public class EmployerObjectModel
    {
            public string IndianTimeNow = (TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"))).ToString();
            public DateTime todaydate = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("India Standard Time")).Date;

            //employees

            public int EmployerIdPK { get; set; }
            [Required(ErrorMessage = "Please enter employee name.")]
            public string EmployerName { get; set; }
            [Required(ErrorMessage = "Please enter phone number.")]
            public string EmployerContactPhone { get; set; }
            [Required(ErrorMessage = "Please enter email id.")]
            public string EmployerContactEmail { get; set; }
            public string EmployerContactAddress1 { get; set; }
            public string EmployerContactAddress2 { get; set; }
            public string EmployerContactCity { get; set; }
            public string EmployerContactState { get; set; }
            public string EmployerContactZip { get; set; }
            public string DateAdded { get; set; }
            public string DateUpdated { get; set; }
            public string AddedBy { get; set; }
            public string UpdatedBy { get; set; }

        }
    }
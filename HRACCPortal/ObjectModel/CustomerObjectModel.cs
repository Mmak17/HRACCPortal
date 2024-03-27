using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HRACCPortal.ObjectModel
{
    public class CustomerObjectModel
    {
        public string IndianTimeNow = (TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"))).ToString();
        public DateTime todaydate = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("India Standard Time")).Date;

        //customer
        public int CustomerIdPK { get; set; }
        [Required(ErrorMessage = "Please enter customer name.")]
        public string CustomerName { get; set; }
        [Required(ErrorMessage = "Please enter phone number.")]
        public string CustomerContactPhone { get; set; }
        [Required(ErrorMessage = "Please enter email id.")]
        public string CustomerContactEmail { get; set; }
        public string CustomerContactAddress1 { get; set; }
        public string CustomerContactAddress2 { get; set; }
        public string CustomerContactCity { get; set; }
        public string CustomerContactState { get; set; }
        public string CustomerContactZip { get; set; }
        public string CustomerTerm { get; set; }
        public string DateAdded { get; set; }
        public string DateUpdated { get; set; }
        public string AddedBy { get; set; }
        public string UpdatedBy { get; set; }

    }
}
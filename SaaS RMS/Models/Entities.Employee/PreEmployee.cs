using SaaS_RMS.Models.Entities.Restuarant;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SaaS_RMS.Models.Entities.Employee
{
    public class PreEmployee
    {
        [Required]
        [DisplayName("Title")]
        public string Title { get; set; }
        [Required]
        public string Firstname { get; set; }

        public string Middlename { get; set; }
        [Required]
        public string Lastname { get; set; }
        [DisplayName("Place Of Birth")]
        [Required]
        public string PlaceOfBirth { get; set; }
        [DisplayName("Date Of Birth")]
        [Required]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy HH:mm}")]
        public DateTime DateOfBirth { get; set; }

        [DisplayName("Primary Address")]
        [Required]
        public string PrimaryAddress { get; set; }
        [Required]
        public string Gender { get; set; }
        [DisplayName("Mobile(Home)")]
        public string HomePhoneNumber { get; set; }

        [DisplayName("Mobile(Work)")]
        public string WorkPhoneNumber { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [DisplayName("Department")]
        [Required]
        public int DepartmentId { get; set; }
        [ForeignKey("DepartmentId")]
        public Department Department { get; set; }
    }
}

using System;
using System.ComponentModel.DataAnnotations;

namespace HoxWiChallage.Models.ViewModel
{
    public class ForeignViewModel
    {
        [Display(Name = "First Name")]
        public string FistName { get; set; }

        [Display(Name = "Sure Name")]
        public string SureName { get; set; }

        [Display(Name = "Born Date")]
        [DataType(DataType.Date)]
        public DateTime BornDate { get; set; }

        [Display(Name = "Nationality")]
        public string Nationality { get; set; }

        [Display(Name = "Arrived Date")]
        [DataType(DataType.Date)]
        public DateTime Arrived { get; set; }

        [Display(Name = "Civil Status")]
        [DataType(DataType.Date)]
        public bool CivilStatus { get; set; }

        [Display(Name = "Kind of Visa")]
        public string KindOfVisa { get; set; }
    }
}
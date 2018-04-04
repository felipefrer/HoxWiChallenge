using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HoxWiChallenge.Web.Models.ViewModel
{
    public class ForeignViewModel
    {
        public string Id { get; set; }

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

        [Display(Name = "Kind of Visa")]
        public string KindOfVisa { get; set; }
    }
}
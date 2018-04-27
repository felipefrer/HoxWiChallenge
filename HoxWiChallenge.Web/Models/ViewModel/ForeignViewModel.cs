using System;
using System.ComponentModel.DataAnnotations;

namespace HoxWiChallenge.Web.Models.ViewModel
{
    public class ForeignViewModel
    {
        #region Properties

        public string Id { get; set; }

        [Display(Name = "First Name")]
        public string FistName { get; set; }

        [Display(Name = "Surname")]
        public string Surname { get; set; }

        [Display(Name = "Birthday")]
        [DataType(DataType.Date)]
        public DateTime BirthdayDate { get; set; }

        [Display(Name = "Nationality")]
        public string Nationality { get; set; }

        [Display(Name = "Arrival in Ireland")]
        [DataType(DataType.Date)]
        public DateTime Arrival { get; set; }

        [Display(Name = "Current Visa")]
        public string Visa { get; set; }

        #endregion
    }
}
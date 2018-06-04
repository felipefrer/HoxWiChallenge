using FluentValidation.Attributes;
using HoxWiChallenge.Web.Models.ViewModel.Validation;
using System;
using System.ComponentModel.DataAnnotations;

namespace HoxWiChallenge.Web.Models.ViewModel
{
    [Validator(typeof(ForeignViewModelValidator))]
    public class ForeignViewModel
    {
        #region Properties

        public string Id { get; set; }

        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Surname")]
        public string Surname { get; set; }

        [Display(Name = "Name")]
        public string FullName { get { return FirstName + " " + Surname; } }

        [Display(Name = "Gender")]
        public string Gender { get; set; }

        [Display(Name = "Date of Birth")]
        public DateTime BirthdayDate { get; set; }

        [Display(Name = "Nationality")]
        public string Nationality { get; set; }

        [Display(Name = "Arrival in Ireland")]
        public DateTime Arrival { get; set; }

        [Display(Name = "Current Visa")]
        public string Visa { get; set; }

        public string VisaName { get { return "Stamp " + Visa; } }

        #endregion
    }
}
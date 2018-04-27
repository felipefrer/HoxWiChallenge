using HoxWiChallenge.Web.Models.HoxWi;
using System;

namespace HoxWiChallenge.Web.Models
{
    public class Foreign : HoxWiModel
    {
        #region Properties

        public string FistName { get; set; }
        public string Surname { get; set; }
        public DateTime BirthdayDate { get; set; }
        public string Nationality { get; set; }
        public DateTime Arrival { get; set; }
        public string Visa { get; set; }

        #endregion     
    }
}
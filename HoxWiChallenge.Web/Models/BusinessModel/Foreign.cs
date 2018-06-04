using HoxWiChallenge.Web.Models.HoxWi;
using System;
using System.Collections.Generic;

namespace HoxWiChallenge.Web.Models
{
    public class Foreign : HoxWiModel<Foreign>
    {
        #region Properties

        public string FirstName { get; set; }
        public string Surname { get; set; }
        public Gender Gender { get; set; }
        public DateTime BirthdayDate { get; set; }
        public string Nationality { get; set; }
        public DateTime Arrival { get; set; }
        public string Visa { get; set; }

        #endregion
    }
}
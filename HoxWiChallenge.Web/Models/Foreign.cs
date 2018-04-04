using HoxWiChallenge.Web.Models.HoxWi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HoxWiChallenge.Web.Models
{
    public class Foreign : HoxWiModel
    {
        public string FistName { get; set; }
        public string SureName { get; set; }
        public DateTime BornDate { get; set; }
        public string Nationality { get; set; }
        public DateTime Arrived { get; set; }
        public string KindOfVisa { get; set; }
    }
}
using HoxWiChallage.Models.HoxWi;
using System;

namespace HoxWiChallage.Models
{
    public class Foreign : HoxWiModel
    {
        public string FistName { get; set; }
        public string SureName { get; set; }
        public DateTime BornDate { get; set; }
        public string Nationality { get; set; }
        public DateTime Arrived { get; set; }
        public bool CivilStatus { get; set; }
        public string KindOfVisa { get; set; }
    }
}
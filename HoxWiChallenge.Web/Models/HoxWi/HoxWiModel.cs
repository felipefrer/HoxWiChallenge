using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HoxWiChallenge.Web.Models.HoxWi
{
    public class HoxWiModel : IHoxWiModel
    {
        public string _id { get; set; }
        public DateTime hCreationDate { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HoxWiChallenge.Web.Models.HoxWi
{
    interface IHoxWiModel
    {
        string _id { get; set; }
        DateTime hCreationDate { get; set; }
    }
}

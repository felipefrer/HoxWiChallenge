using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HoxWiChallage.Models.HoxWi
{
    public interface IHoxWiModel
    {
        Guid Hid { get; set; }
        DateTime HCreationDate { get; set; }
    }
}
using System;
using System.Collections.Generic;

namespace HoxWiChallenge.Web.Models.HoxWi
{
    public interface IHoxWiModel<T> where T : class
    {
        #region Properties

        string _id { get; set; }
        DateTime hCreationDate { get; set; }
        DateTime hLastUpdate { get; set; }

        #endregion
    }
}

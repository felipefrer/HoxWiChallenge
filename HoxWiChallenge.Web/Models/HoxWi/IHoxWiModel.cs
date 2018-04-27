using System;

namespace HoxWiChallenge.Web.Models.HoxWi
{
    interface IHoxWiModel
    {
        #region Properties

        string _id { get; set; }
        DateTime hCreationDate { get; set; }

        #endregion
    }
}

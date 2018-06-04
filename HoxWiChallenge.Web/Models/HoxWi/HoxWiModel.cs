using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace HoxWiChallenge.Web.Models.HoxWi
{
    public abstract class HoxWiModel<T> : IHoxWiModel<T> where T : class
    {
        #region Properties

        public string _id { get; set; }
        public DateTime hCreationDate { get; set; }
        public DateTime hLastUpdate { get; set; }

        #endregion
    }
}
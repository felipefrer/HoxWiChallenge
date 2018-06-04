using HoxWiChallenge.Web.Models;
using System;
using System.Collections.Generic;

namespace HoxWiChallenge.Web.Business.Interfaces
{
    public interface IForeignBusiness : IBusiness<Foreign>
    {
        #region Methods

        List<Foreign> GetAllForeign(string searchPhrase, int skip, int rowCount, string sort);
        Foreign GetForeignById(string id);
        Tuple<bool, string> InsertNewForeign(Foreign foreign);
        Tuple<bool, string> UpdateForeign(Foreign foreign);

        #endregion
    }
}

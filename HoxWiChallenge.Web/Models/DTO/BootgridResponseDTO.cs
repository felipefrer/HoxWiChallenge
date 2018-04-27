using System.Collections.Generic;

namespace HoxWiChallenge.Web.Models.DTO
{
    public class BootgridResponseDTO<T> where T : class
    {
        #region Properties

        public int current;
        public int rowCount;
        public List<T> rows;
        public int total;

        #endregion
    }
}
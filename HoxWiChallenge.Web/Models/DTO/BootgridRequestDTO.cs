using System.Collections.Specialized;
using System.Linq;

namespace SmartHourRegister.Web.DTO
{
    public class BootgridRequestDTO
    {
        #region Contructors

        public BootgridRequestDTO(NameValueCollection formRequest)
        {
            var orderKey = formRequest.AllKeys.Where(k => k.StartsWith("sort")).First();
            var startIndex = orderKey.IndexOf("[") + 1;
            var endIndex = orderKey.IndexOf("]");
            var sortProperty = orderKey.Substring(startIndex, (endIndex - startIndex));

            var id = formRequest.AllKeys.Where(k => k.Equals("id")).FirstOrDefault();

            int.TryParse(id, out Id);
            Current = int.Parse(formRequest["current"]);
            RowCount = int.Parse(formRequest["rowCount"]);
            SearchPhrase = formRequest["searchPhrase"].ToString();
            Sort = $"{sortProperty} {formRequest[orderKey]}";
        }

        #endregion

        #region Properties

        public int Id;
        public int Current;
        public int RowCount;
        public string Sort;
        public string SearchPhrase;
        public int Skip { get { return (Current - 1) * RowCount; } }

        #endregion
    }
}
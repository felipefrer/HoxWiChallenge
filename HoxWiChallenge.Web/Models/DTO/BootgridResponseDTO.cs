using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HoxWiChallenge.Web.Models.DTO
{
    public class BootgridResponseDTO<T> where T : class
    {
        public int current;
        public int rowCount;
        public List<T> rows;
        public int total;
    }
}
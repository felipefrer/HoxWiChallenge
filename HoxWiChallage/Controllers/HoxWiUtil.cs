using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HoxWiChallage.Controllers
{
    public class HoxWiUtil<T> where T : class
    {
        public static IEnumerable<T> HoxWiResultConvert(object [] result)
        {
            return ((IEnumerable)result).Cast<T>();
        }
    }
}
using HoxWiChallenge.Web.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HoxWiChallenge.Web.Util
{
    public static class ApplicationParser<T> where T : class
    {
        public static List<T> ParseToList(dynamic[] data)
        {
            var jsonResult = JsonConvert.SerializeObject(data);
            var dataParse = JsonConvert.DeserializeObject<List<T>>(jsonResult);

            return dataParse;
        }

        public static T Parse(dynamic[] data)
        {
            var jsonResult = JsonConvert.SerializeObject(data);
            var dataParse = JsonConvert.DeserializeObject<List<T>>(jsonResult);

            return dataParse.FirstOrDefault();
        }
    }
}
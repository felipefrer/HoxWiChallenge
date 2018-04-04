using HoxWi.Db;
using HoxWiChallage.Models;
using HoxWiChallage.Models.ViewModel;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HoxWiChallage.Controllers
{
    public class ForeignController : Controller
    {
        #region Constructors

        public ForeignController(IClient hoxWiDbClient)
        {
            _hoxWiDbClient = new Client(Mode.Dynamic, StorageType.Mysql);
        }

        #endregion

        #region Properties

        private readonly IClient _hoxWiDbClient;

        #endregion

        #region Methods

        // GET: Foreign
        public ActionResult Index()
        {
            var searchResult = _hoxWiDbClient.Search(new SearchRequest("Foreing", "JFKa42416116939415ea88709c88d9010eb3a74da2ff3b74ab5b9355529082be282"));

            return View();
        }


        #region Create

        public ActionResult Insert()
        {
             return View();
        }

        #endregion

        [HttpPost]
        public ActionResult Insert(FormCollection foreignForm)
        {
            var workplaceInsert = new InsertRequest("Foreign", ToDictionary(foreignForm));

            var hoxWiResult = _hoxWiDbClient.Add(workplaceInsert);

            return View(hoxWiResult);
        }

        #endregion

        #region Util

        public static IDictionary ToDictionary(FormCollection col)
        {
            var dict = new Dictionary<string, string>();

            foreach (string key in col.Keys)
            {
                dict.Add(key, col[key]);
            }

            return dict;
        }

        #endregion
    }
}
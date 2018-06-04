using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HoxWiChallenge.Web.Controllers
{
    public class HoxWiChallengeController : Controller
    {
        #region Methods

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string GetModelStateErros()
        {
            var lstErros = ModelState.Select(e => e.Value.Errors).Where(x => x.Count > 0).ToList();

            var erros = string.Empty;

            lstErros.ForEach(e => erros += e.FirstOrDefault()?.ErrorMessage + "<br/>");

            return erros;
        }

        #endregion
    }
}
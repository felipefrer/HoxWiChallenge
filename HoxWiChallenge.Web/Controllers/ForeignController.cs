using AutoMapper;
using HoxWi.Db;
using HoxWiChallenge.Web.Models;
using HoxWiChallenge.Web.Models.DTO;
using HoxWiChallenge.Web.Models.ViewModel;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;

namespace HoxWiChallenge.Web.Controllers
{
    public class ForeignController : Controller
    {
        #region Constructors

        public ForeignController(IClient client)
        {
            _hoxWiClient = client;
        }

        #endregion

        #region Properties

        private readonly IClient _hoxWiClient;

        #endregion

        #region Methods

        public JsonResult GetForeigns()
        {
            var searchForeignHoxWi = new SearchRequest("Foreign", WebConfigurationManager.AppSettings["HoxDbApiSecret"]);

            var hoxWiSearchResult = _hoxWiClient.Search(searchForeignHoxWi);

            var jsonResult = JsonConvert.SerializeObject(hoxWiSearchResult.Results);
            var foreign = JsonConvert.DeserializeObject<List<Foreign>>(jsonResult);

            var bootgridResponseDto = new BootgridResponseDTO<ForeignViewModel>
            {
                current = 1,
                rowCount = 5,
                rows = Mapper.Map<List<Foreign>, List<ForeignViewModel>>(foreign),
                total = 3
            };

            return Json(bootgridResponseDto);
        }

        public PartialViewResult ForeignForm()
        {
            return PartialView("_ForeignForm");
        }

        public ActionResult Index()
        {
            var searchForeignHoxWi = new SearchRequest("Foreign", WebConfigurationManager.AppSettings["HoxDbApiSecret"]);

            var hoxWiSearchResult = _hoxWiClient.Search(searchForeignHoxWi);

            var jsonResult = JsonConvert.SerializeObject(hoxWiSearchResult.Results);
            var foreign = JsonConvert.DeserializeObject<List<Foreign>>(jsonResult);

            return View(Mapper.Map<List<Foreign>, List<ForeignViewModel>>(foreign));
        }

        [HttpPost]
        public JsonResult Create(ForeignViewModel foreignViewModel)
        {
            if (!ModelState.IsValid)
            {
                return Json(new BasicResponse { Success = false, Error = GetModelStateErros() });
            }

            var foreign = Mapper.Map<ForeignViewModel, Foreign>(foreignViewModel);

            var insertForeignHoxWi = new InsertRequest("Foreign", foreign);

            var hoxWiResponse = _hoxWiClient.Add(insertForeignHoxWi);

            hoxWiResponse.Message = hoxWiResponse.Success ? "Foreign has been included!" : "Oops, something wrong happened!";

            return Json(hoxWiResponse);
        }

        [HttpPost]
        public JsonResult Update(ForeignViewModel foreignViewModel)
        {
            if (!ModelState.IsValid)
            {
                return Json(new BasicResponse { Success = false, Error = GetModelStateErros() });
            }

            var foreign = Mapper.Map<ForeignViewModel, Foreign>(foreignViewModel);

            var updateForeignHoxWi = new UpdateRequest("Foreign", foreign);


            var hoxWiResponse = _hoxWiClient.Update(updateForeignHoxWi);

            return Json(hoxWiResponse);
        }

        [HttpPost]
        public JsonResult Delete(string hidForeign)
        {
            var deleteSearchRequest = new SearchRequest("Foreign", new { _id = hidForeign });

            var hoxWiResponse = _hoxWiClient.Delete(deleteSearchRequest);

            hoxWiResponse.Message = hoxWiResponse.Success ? "Foreign has been deleted!" : "Oops, something wrong happened!";

            return Json(hoxWiResponse);
        }

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
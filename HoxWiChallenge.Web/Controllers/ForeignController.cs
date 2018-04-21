using AutoMapper;
using HoxWi.Db;
using HoxWiChallenge.Web.Models;
using HoxWiChallenge.Web.Models.DTO;
using HoxWiChallenge.Web.Models.ViewModel;
using HoxWiChallenge.Web.Util;
using Newtonsoft.Json;
using SmartHourRegister.Web.DTO;
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

        public JsonResult GetForeigns(BootgridRequestDTO bootgridRequestDTO)
        {
            var searchForeignHoxWi = new SearchRequest("Foreign", WebConfigurationManager.AppSettings["HoxDbApiSecret"], bootgridRequestDTO.RowCount, "_id");

            var hoxWiSearchResult = _hoxWiClient.Search(searchForeignHoxWi);

            var lstForeign = ApplicationParser<Foreign>.ParseToList(hoxWiSearchResult.Results);

            var lstForeignViewModel = Mapper.Map<List<Foreign>, List<ForeignViewModel>>(lstForeign);

            var bootgridResponseDto = new BootgridResponseDTO<ForeignViewModel>
            {
                current = bootgridRequestDTO.Current,
                rowCount = bootgridRequestDTO.RowCount,
                rows = lstForeignViewModel,
                total = lstForeignViewModel.Count()
            };

            return Json(bootgridResponseDto);
        }

        public PartialViewResult ForeignForm(string foreignId)
        {
            ForeignViewModel foreignViewModel = null;

            if (!string.IsNullOrWhiteSpace(foreignId))
            {
                var searchForeignHoxWi = new SearchRequest("Foreign", WebConfigurationManager.AppSettings["HoxDbApiSecret"], new { _id = foreignId });

                var hoxWiSearchResult = _hoxWiClient.Search(searchForeignHoxWi);

                if (hoxWiSearchResult.Results.Count() > 0)
                {
                    var foreign = ApplicationParser<Foreign>.Parse(hoxWiSearchResult.Results);
                    foreignViewModel = Mapper.Map<Foreign, ForeignViewModel>(foreign);
                }
            }           
            
            return PartialView("_ForeignForm", foreignViewModel);
        }

        public ActionResult Index()
        {
            return View();
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
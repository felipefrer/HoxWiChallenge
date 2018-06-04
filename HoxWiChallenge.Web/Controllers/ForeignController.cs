using AutoMapper;
using HoxWi.Db;
using HoxWiChallenge.Web.Business.Interfaces;
using HoxWiChallenge.Web.Models;
using HoxWiChallenge.Web.Models.DTO;
using HoxWiChallenge.Web.Models.HoxWi;
using HoxWiChallenge.Web.Models.ViewModel;
using SmartHourRegister.Web.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Configuration;
using System.Web.Mvc;

namespace HoxWiChallenge.Web.Controllers
{
    public class ForeignController : HoxWiChallengeController
    {
        #region Constructors

        public ForeignController(IForeignBusiness foreignBusiness)
        {
            _foreignBusiness = foreignBusiness;
        }

        #endregion

        #region Properties

        private readonly IForeignBusiness _foreignBusiness;

        #endregion

        #region Methods

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bootgridRequestDTO"></param>
        /// <returns></returns>
        public JsonResult GetForeigns(BootgridRequestDTO bootgridRequestDTO)
        {
            var lstForeign = _foreignBusiness.GetAllForeign(bootgridRequestDTO.SearchPhrase, bootgridRequestDTO.Skip, bootgridRequestDTO.RowCount, bootgridRequestDTO.Sort);

            // Mapping business model to view model.
            var lstForeignViewModel = Mapper.Map<List<Foreign>, List<ForeignViewModel>>(lstForeign);

            // Building bootgridresponse from view model.
            var bootgridResponseDto = new BootgridResponseDTO<ForeignViewModel>
            {
                current = bootgridRequestDTO.Current,
                rowCount = bootgridRequestDTO.RowCount,
                rows = lstForeignViewModel,
                total = lstForeignViewModel.Count()
            };

            return Json(bootgridResponseDto);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="foreignId"></param>
        /// <returns></returns>
        public PartialViewResult ForeignForm(string foreignId)
        {            
            return PartialView("_ForeignForm", null);
        }

        public JsonResult GetForeignById(string foreignId)
        {
            try
            {
                var foreign = _foreignBusiness.GetForeignById(foreignId);

                var foreignViewModel = Mapper.Map<Foreign, ForeignViewModel>(foreign);

                return Json(new AppResponseDTO { Success = false, Message = "Foreign data!", Data = foreignViewModel });
            }
            catch (Exception)
            {
                return Json(new AppResponseDTO { Success = false, Message = "Oops, something wrong happened!" });
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="foreignViewModel"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult Create(ForeignViewModel foreignViewModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return Json(new AppResponseDTO { Success = false, Message = GetModelStateErros() });
                }

                var foreign = Mapper.Map<ForeignViewModel, Foreign>(foreignViewModel);

                var businessResult = _foreignBusiness.InsertNewForeign(foreign);

                return Json(new AppResponseDTO { Success = businessResult.Item1, Message = businessResult.Item2 });
            }
            catch (Exception)
            {
                return Json(new AppResponseDTO { Success = false, Message = "Oops, something wrong happened!" });
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="foreignViewModel"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult Update(ForeignViewModel foreignViewModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return Json(new AppResponseDTO { Success = false, Message = GetModelStateErros() });
                }

                var foreign = Mapper.Map<ForeignViewModel, Foreign>(foreignViewModel);

                var businessResult = _foreignBusiness.UpdateForeign(foreign);

                return Json(new AppResponseDTO { Success = businessResult.Item1, Message = businessResult.Item2 });
            }
            catch (Exception)
            {
                return Json(new AppResponseDTO() { Success = false, Message = "Oops, something wrong happened!" });
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="hidForeign"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Delete(string hidForeign)
        {
            try
            {
                _foreignBusiness.Remove(hidForeign);

                return Json(new AppResponseDTO() { Success = true, Message = "Foreign has been deleted!" });
            }
            catch (Exception)
            {
                return Json(new AppResponseDTO() { Success = true, Message = "Oops, something wrong happened!" });
            }            
        }      

        #endregion
    }
}
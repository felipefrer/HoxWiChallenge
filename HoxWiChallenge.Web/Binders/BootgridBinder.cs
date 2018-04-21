using SmartHourRegister.Web.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HoxWiChallenge.Web.Binders
{
    public class BootgridBinder : IModelBinder
    {
        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            var bootGridRequestDTO = new BootgridRequestDTO(controllerContext.HttpContext.Request.Form);

            return bootGridRequestDTO;
        }
    }
}
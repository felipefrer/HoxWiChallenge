using HoxWiChallenge.Web.AutoMapper;
using HoxWiChallenge.Web.Binders;
using SmartHourRegister.Web.DTO;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace HoxWiChallenge.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            ModelBinders.Binders.Add(typeof(BootgridRequestDTO), new BootgridBinder());
            AutoMapperWebConfig.Configure();
        }
    }
}

using HoxWi.Db;
using HoxWiChallenge.Web;
using HoxWiChallenge.Web.Business;
using HoxWiChallenge.Web.Business.Interfaces;
using Microsoft.Web.Infrastructure.DynamicModuleHelper;
using Ninject;
using Ninject.Web.Common;
using Ninject.Web.Common.WebHost;
using System;
using System.Web;
using System.Web.Configuration;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(NinjectWebCommon), "Stop")]

namespace HoxWiChallenge.Web
{
    public static class NinjectWebCommon
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        public static void Start()
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel);
        }

        public static void Stop()
        {
            bootstrapper.ShutDown();
        }

        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
            kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();

            RegisterServices(kernel);
            return kernel;
        }

        private static void RegisterServices(IKernel kernel)
        {
            kernel.Bind<IClient>().ToMethod(ctx => new Client(Mode.Dynamic, StorageType.MongoDB));

            kernel.Bind(typeof(IBusiness<>)).To(typeof(BusinessBase<>));
            //kernel.Bind(typeof(IForeignBusiness)).To(typeof(ForeignBusiness));
            kernel.Bind<IForeignBusiness>().To<ForeignBusiness>().WithConstructorArgument("secretKey", WebConfigurationManager.AppSettings["HoxDbApiSecret"]);
        }
    }
}
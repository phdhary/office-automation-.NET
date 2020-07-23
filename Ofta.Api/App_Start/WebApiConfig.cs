using Ofta.Lib.BL;
using Ofta.Lib.Dal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Dependencies;
using Unity;

namespace Ofta.Api
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            //cors settubg
            var cors = new EnableCorsAttribute("*", "*", "*");
            config.EnableCors(cors);

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
            config.Formatters.Remove(config.Formatters.XmlFormatter);

            var container = RegisterDependency();
            config.DependencyResolver = new UnityResolver(container);
        }
        private static UnityContainer RegisterDependency()
        {
            var container = new UnityContainer();
            container.RegisterType<IKotaBL, KotaBL>();
            container.RegisterType<ILaporanDinasBL, LaporanDinasBL>();
            container.RegisterType<IParamNoBL, ParamNoBL>();
            container.RegisterType<ISuratDinasBL, SuratDinasBL>();
            container.RegisterType<IJabatanBL, JabatanBL>();
            container.RegisterType<IJenisArsipBL, JenisArsipBL>();
            container.RegisterType<IJenisBiayaBL, JenisBiayaBL>();
            container.RegisterType<IJenisCutiBL, JenisCutiBL>();
            container.RegisterType<IJenisKontrakBL, JenisKontrakBL>();
            container.RegisterType<IJenisSuratDinasBL, JenisSuratDinasBL>();
            container.RegisterType<IPegBL, PegBL>();
            container.RegisterType<IRSBL, RSBL>();
            container.RegisterType<IApprovalTypeBL, ApprovalTypeBL>();

            container.RegisterType<IJabatanDal, JabatanDal>();
            container.RegisterType<IJenisArsipDal, JenisArsipDal>();
            container.RegisterType<IJenisBiayaDal, JenisBiayaDal>();
            container.RegisterType<IJenisCutiDal, JenisCutiDal>();
            container.RegisterType<IJenisKontrakDal, JenisKontrakDal>();
            container.RegisterType<IJenisSuratDinasDal, JenisSuratDinasDal>();
            container.RegisterType<IKotaDal, KotaDal>();
            container.RegisterType<ILaporanDinasDal, LaporanDinasDal>();
            container.RegisterType<IParamNoDal, ParamNoDal>();
            container.RegisterType<IPegDal, PegDal>();
            container.RegisterType<IRSDal, RSDal>();
            container.RegisterType<ISuratDinasDal, SuratDinasDal>();
            container.RegisterType<ITransportDal, TransportDal>();
            container.RegisterType<IApprovalTypeDal, ApprovalTypeDal>();

            return container;
        }

    }

    public class UnityResolver : IDependencyResolver
    {
        protected IUnityContainer _container;

        public UnityResolver(IUnityContainer container)
        {
            _container = container;
        }

        public IDependencyScope BeginScope()
        {
            var child = _container.CreateChildContainer();
            return new UnityResolver(child);
        }

        public object GetService(Type serviceType)
        {
            try
            {
                return _container.Resolve(serviceType);
            }
            catch (ResolutionFailedException)
            {
                return null;
            }
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            try
            {
                return _container.ResolveAll(serviceType);
            }
            catch (ResolutionFailedException)
            {
                return new List<object>();
            }
        }

        public void Dispose()
        {
            Dispose(true);
        }

        protected virtual void Dispose(bool disposing)
        {
            _container.Dispose();
        }
    }

}

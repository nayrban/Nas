using Autofac;
using Autofac.Integration.WebApi;
using NasData;
using NasData.Infraestructure;
using NasData.Respository;
using NasService;
using Owin;
using System.Linq;
using System.Reflection;
using System.Web.Http;

namespace NasServiceAPI.Config
{
    public class AutofacConfig
    {
        public static void Init(IAppBuilder app, HttpConfiguration httpConfig)
        {
            var builder = new ContainerBuilder();

            // Register your Web API controllers.
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            builder.RegisterType<UnitOfWork<NasModelContext>>().As<IUnitOfWork<NasModelContext>>().InstancePerRequest();
            builder.RegisterType<DbFactory<NasModelContext>>().As<IDbFactory<NasModelContext>>().InstancePerRequest();            

            builder.RegisterAssemblyTypes(typeof(MinistryRepository).Assembly)
               .Where(t => t.Name.EndsWith("Repository"))
               .AsImplementedInterfaces().InstancePerRequest();

            builder.RegisterAssemblyTypes(typeof(MinistryCodeRepository).Assembly)
               .Where(t => t.Name.EndsWith("Repository"))
               .AsImplementedInterfaces().InstancePerRequest();

            // Services     
            builder.RegisterAssemblyTypes(typeof(MinistryService).Assembly)
            .Where(t => t.Name.EndsWith("Service"))
            .AsImplementedInterfaces().InstancePerRequest();

            // builder.Register(c => new MemberService()).As<IMemberService>().InstancePerRequest();

            // OPTIONAL: Register the Autofac filter provider.
            builder.RegisterWebApiFilterProvider(httpConfig);



            // Set the dependency resolver to be Autofac.
            var container = builder.Build();
            httpConfig.DependencyResolver = new AutofacWebApiDependencyResolver(container);
            app.UseAutofacMiddleware(container);
            //app.UseAutofacLifetimeScopeInjector(container);       
            app.UseAutofacWebApi(httpConfig);
        }
    }
}
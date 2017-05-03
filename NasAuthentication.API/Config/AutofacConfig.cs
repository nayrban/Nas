using Autofac;
using Autofac.Integration.WebApi;
using NasData;
using NasData.Infraestructure;
using NasData.Respository;
using NasData.Respository.Auth;
using NasService;
using Owin;
using System.Reflection;
using System.Web.Http;

namespace NasAuthentication.Config
{
    public class AutofacConfig
    {
        public static void Init(IAppBuilder app, HttpConfiguration httpConfig)
        {
            var builder = new ContainerBuilder();                        

            // Register your Web API controllers.
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            builder.RegisterType<UnitOfWork<AuthContext>>().As<IUnitOfWork<AuthContext>>().InstancePerRequest();
            builder.RegisterType<UnitOfWork<NasModelContext>>().As<IUnitOfWork<NasModelContext>>().InstancePerRequest();
            builder.RegisterType<DbFactory<AuthContext>>().As<IDbFactory<AuthContext>>().InstancePerRequest();
            builder.RegisterType<DbFactory<NasModelContext>>().As<IDbFactory<NasModelContext>>().InstancePerRequest();

            builder.RegisterAssemblyTypes(typeof(AuthClientRepository).Assembly)
               .Where(t => t.Name.EndsWith("Repository"))
               .AsImplementedInterfaces().InstancePerRequest();

            builder.RegisterAssemblyTypes(typeof(AuthRefreshTokenRepository).Assembly)
               .Where(t => t.Name.EndsWith("Repository"))
               .AsImplementedInterfaces().InstancePerRequest();

            builder.RegisterAssemblyTypes(typeof(ApplicationUserRepository).Assembly)
               .Where(t => t.Name.EndsWith("Repository"))
               .AsImplementedInterfaces().InstancePerRequest();

            builder.RegisterAssemblyTypes(typeof(MemberRepository).Assembly)
              .Where(t => t.Name.EndsWith("Repository"))
              .AsImplementedInterfaces().InstancePerRequest();

            builder.RegisterAssemblyTypes(typeof(DeviceRepository).Assembly)
              .Where(t => t.Name.EndsWith("Repository"))
              .AsImplementedInterfaces().InstancePerRequest();

            // Services
            builder.RegisterAssemblyTypes(typeof(AuthClientService).Assembly)
               .Where(t => t.Name.EndsWith("Service"))
               .AsImplementedInterfaces().InstancePerRequest();

            builder.RegisterAssemblyTypes(typeof(MemberService).Assembly)
               .Where(t => t.Name.EndsWith("Service"))
               .AsImplementedInterfaces().InstancePerRequest();

            builder.RegisterAssemblyTypes(typeof(DeviceService).Assembly)
               .Where(t => t.Name.EndsWith("Service"))
               .AsImplementedInterfaces().InstancePerRequest();

            builder.RegisterAssemblyTypes(typeof(ApplicationUserService).Assembly)
               .Where(t => t.Name.EndsWith("Service"))
               .AsImplementedInterfaces().InstancePerRequest();
            
            builder.RegisterAssemblyTypes(typeof(AuthRefreshTokenService).Assembly)
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
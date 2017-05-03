using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;
using NasData;
using NasServiceAPI;
using NasServiceAPI.Config;
using Owin;
using System.Data.Entity;
using System.Web.Http;

[assembly: OwinStartup(typeof(Startup))]

namespace NasServiceAPI
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {           
            // Code that runs on application startup
            //AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);

            HttpConfiguration httpConfig = new HttpConfiguration();


            AutoMapperConfig.Init();

            AutofacConfig.Init(app, httpConfig);


            ConfigureOAuth(app);

            WebApiConfig.Register(httpConfig);
            app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);
            app.UseWebApi(httpConfig);

            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<NasModelContext>());


        }

        private void ConfigureOAuth(IAppBuilder app)
        {
            //Token Consumption
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions
            {
            });
        }
    }
}
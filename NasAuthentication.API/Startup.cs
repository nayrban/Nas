using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;
using NasAuthentication.API.Providers;
using NasAuthentication.Config;
using NasData;

using Owin;
using System;
using System.Data.Entity;
using System.Web.Http;

[assembly: OwinStartup(typeof(NasAuthentication.API.Startup))]
namespace NasAuthentication.API
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {

            HttpConfiguration config = new HttpConfiguration();
            AutofacConfig.Init(app, config);
            AutoMapperConfig.Init();

            ConfigureOAuth(app);
                     
            WebApiConfig.Register(config);
            app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);
            app.UseWebApi(config);

            //Database.SetInitializer(new DropCreateDatabaseIfModelChanges<AuthContext>());
            //Database.SetInitializer(new DropCreateDatabaseAlways<NasModelContext>());
            //  Database.SetInitializer<NasModelContext>(null);
        }

        public void ConfigureOAuth(IAppBuilder app)
        {
            OAuthAuthorizationServerOptions OAuthServerOptions = new OAuthAuthorizationServerOptions()
            {
                AllowInsecureHttp = true,
                TokenEndpointPath = new PathString("/api/account/token"),
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(1),                
                Provider = new SimpleAuthorizationServerProvider(),
                RefreshTokenProvider = new SimpleRefreshTokenProvider()
            };
            


            // Token Generation
            app.UseOAuthAuthorizationServer(OAuthServerOptions);
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());


        }
    }
}